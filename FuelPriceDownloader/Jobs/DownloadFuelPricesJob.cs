using FuelPriceDownloader.Services;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Diagnostics;

namespace FuelPriceDownloader.Jobs
{
    [DisallowConcurrentExecution]
    public class DownloadFuelPricesJob : IJob
    {
        private readonly IFuelPriceDownloaderService _fuelPriceDownloaderService;
        private readonly ILogger<DownloadFuelPricesJob> _logger;

        public DownloadFuelPricesJob(IFuelPriceDownloaderService fuelPriceDownloaderService, ILogger<DownloadFuelPricesJob> logger)
        {
            _fuelPriceDownloaderService = fuelPriceDownloaderService;
            _logger = logger;
        }

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
