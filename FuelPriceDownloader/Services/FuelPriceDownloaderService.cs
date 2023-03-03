using FuelPriceDownloader.Models;
using FuelPriceDownloader.Models.FuelPriceResponse;
using FuelPriceDownloader.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FuelPriceDownloader.Services
{
    public class FuelPriceDownloaderService : IFuelPriceDownloaderService
    {
        private readonly AppSettingOptions _options;
        private readonly FuelPriceDbContext _dbContext;
        private readonly HttpClient _client;
        private readonly ILogger<FuelPriceDownloaderService> _logger;

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

        public async Task DownloadFuelPricesAsync()
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
                .Where(fp => fp != null)
                .ToList();

            if (fuelPrices != null && fuelPrices.Any())
            {
                // Filter out fuel prices that are older than the specified number of days
                var cutoffDate = DateTime.UtcNow.AddDays(-_options.DaysCount);
                fuelPrices = fuelPrices.Where(fp => fp.RecordDate >= cutoffDate).ToList();

                // Filter out any duplicate fuel prices
                var recordsToSave = fuelPrices
                    .GroupBy(fp => fp.RecordDate)
                    .Select(g => g.First())
                    .ToList();

                // Get the dates of the records that already exist in the database
                var existingDates = await _dbContext.FuelPrices
                    //.Where(fp => recordsToSave.Any(r => r.RecordDate == fp.RecordDate))
                    .Select(fp => fp.RecordDate)
                    .ToListAsync();

                // Add new records to the database
                var newRecords = recordsToSave
                    .Where(r => !existingDates.Contains(r.RecordDate))
                    .ToList();

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
