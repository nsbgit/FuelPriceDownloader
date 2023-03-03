using FuelPriceDownloader.Services;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Diagnostics;

namespace FuelPriceDownloader.Jobs
{
    /// <summary>
    /// Represents a job that downloads fuel prices using a FuelPriceDownloaderService.
    /// </summary>
    [DisallowConcurrentExecution]
    public class DownloadFuelPricesJob : IJob
    {
        private readonly IFuelPriceDownloaderService _fuelPriceDownloaderService;
        private readonly ILogger<DownloadFuelPricesJob> _logger;

        /// <summary>
        /// Initializes a new instance of the DownloadFuelPricesJob class with the specified dependencies.
        /// </summary>
        /// <param name="fuelPriceDownloaderService">The service used to download fuel prices.</param>
        /// <param name="logger">The logger used to log information about the job's execution.</param>
        public DownloadFuelPricesJob(IFuelPriceDownloaderService fuelPriceDownloaderService, ILogger<DownloadFuelPricesJob> logger)
        {
            _fuelPriceDownloaderService = fuelPriceDownloaderService;
            _logger = logger;
        }

        /// <summary>
        /// Executes the job, downloading fuel prices and logging information about the job's execution.
        /// </summary>
        /// <param name="context">The execution context of the job.</param>
        /// <returns>A task that represents the asynchronous job execution.</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            var methodName = "FuelPriceDownloader.Jobs.DownloadFuelPricesJob.Execute";
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                await _fuelPriceDownloaderService.DownloadFuelPricesAsync();

                stopwatch.Stop();
                _logger.LogDebug($"Method '{methodName}' took {stopwatch.ElapsedMilliseconds} ms to execute.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in method '{methodName}'. Elapsed time: {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
    }
}
