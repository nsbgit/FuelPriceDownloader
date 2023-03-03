using FuelPriceDownloader.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPriceDownloader.Jobs
{
    [DisallowConcurrentExecution]
    public class DownloadFuelPricesJob : IJob
    {
        private readonly IFuelPriceDownloaderService _fuelPriceDownloaderService;

        public DownloadFuelPricesJob(IFuelPriceDownloaderService fuelPriceDownloaderService)
        {
            _fuelPriceDownloaderService = fuelPriceDownloaderService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _fuelPriceDownloaderService.DownloadFuelPricesAsync();
        }
    }
}
