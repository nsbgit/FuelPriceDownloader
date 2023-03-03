using FuelPriceDownloader.Models;
using FuelPriceDownloader.Models.FuelPriceResponse;
using FuelPriceDownloader.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;

namespace FuelPriceDownloader.Services
{
    /// <summary>
    /// A service that downloads fuel prices from an API and stores them in a database.
    /// </summary>
    public class FuelPriceDownloaderService : IFuelPriceDownloaderService
    {
        private readonly AppSettingOptions _options;
        private readonly FuelPriceDbContext _dbContext;
        private readonly HttpClient _client;
        private readonly ILogger<FuelPriceDownloaderService> _logger;

        /// <summary>
        /// Initializes a new instance of the FuelPriceDownloaderService class with the specified dependencies.
        /// </summary>
        /// <param name="options">The application settings options.</param>
        /// <param name="dbContext">The database context used to store the fuel prices.</param>
        /// <param name="client">The HTTP client used to download the fuel prices.</param>
        /// <param name="logger">The logger used to log information about the service's execution.</param>
        public FuelPriceDownloaderService(
            IOptions<AppSettingOptions> options,
            FuelPriceDbContext dbContext,
            HttpClient client,
            ILogger<FuelPriceDownloaderService> logger)
        {
            _options = options.Value;
            _dbContext = dbContext;
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Downloads the fuel prices from the API and stores them in the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous download and storage of the fuel prices.</returns>
        public async Task DownloadFuelPricesAsync()
        {
            try
            {
                // Download the fuel prices from the API
                var response = await _client.GetAsync(_options.ApiUrl);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                // Parse the fuel prices from the JSON response
                var fuelPriceResponse = JsonConvert.DeserializeObject<FuelPriceResponse>(json);
                var fuelPrices = fuelPriceResponse?.Series?
                    .SelectMany(s => s.data)
                    .Select(d => CreateFuelPrice(d))
                    .ToList();

                if (fuelPrices != null && fuelPrices.Any())
                {
                    // Filter out fuel prices that are older than the specified number of days
                    var cutoffDate = DateTime.UtcNow.AddDays(-_options.DaysCount);

                    // Add new records to the database
                    var existingDates = await _dbContext.FuelPrices
                        .Select(fp => fp.RecordDate)
                        .ToListAsync();

                    var newRecords = new List<FuelPrice>();
                    foreach (var fp in fuelPrices)
                    {
                        if (fp != null && fp.RecordDate >= cutoffDate && !existingDates.Contains(fp.RecordDate))
                        {
                            newRecords.Add(fp);
                        }
                    }

                    if (newRecords.Count > 0)
                    {
                        _dbContext.FuelPrices.AddRange(newRecords);
                        await _dbContext.SaveChangesAsync();
                        _logger.LogInformation($"Added {newRecords.Count} new fuel price records to the database.");
                    }
                    else
                    {
                        _logger.LogInformation("No new fuel price records to add to the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while downloading fuel prices.");
            }
        }


        /// <summary>
        /// Creates a FuelPrice object from an array of data.
        /// </summary>
        /// <param name="d">An array of data representing a fuel price record.</param>
        /// <returns>A FuelPrice object created from the data, or null if an error occurs.</returns>
        private FuelPrice CreateFuelPrice(object[] d)
        {
            DateTime recordDate;
            double price = 0;
            try
            {
                recordDate = DateTime.ParseExact(d[0] as string, "yyyyMMdd", CultureInfo.InvariantCulture);
                price = Convert.ToDouble(d[1]);
            }
            catch (FormatException ex)
            {
                string errorMessage = $"Error creating FuelPrice object. Data: [{string.Join(", ", d)}]. Exception: {ex.Message}";
                _logger.LogError(ex, errorMessage);
                return null;
            }

            return new FuelPrice
            {
                RecordDate = recordDate,
                Price = price
            };
        }
    }
}
