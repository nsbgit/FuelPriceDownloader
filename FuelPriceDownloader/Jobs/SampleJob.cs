using FuelPriceDownloader.Services;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPriceDownloader.Jobs
{
    [DisallowConcurrentExecution]
    public class SampleJob : IJob
    {
        private readonly IPrintService _printService;
        private readonly ILogger<SampleJob> _logger;

        public SampleJob(IPrintService printService, ILogger<SampleJob> logger)
        {
            _printService = printService;
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Started Job at {DateTime.Now}");
            await _printService.PrintCount();
            _logger.LogInformation($"Ended Job at {DateTime.Now}");
        }
    }
}
