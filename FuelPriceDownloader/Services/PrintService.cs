using FuelPriceDownloader.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPriceDownloader.Services
{
    public class PrintService : IPrintService
    {
        private readonly ILogger<PrintService> _logger;
        private readonly FuelPriceDbContext _dbContext;

        public PrintService(ILogger<PrintService> logger, FuelPriceDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task PrintCount()
        {
            try
            {
                _logger.LogInformation($"Started at: {DateTime.Now}");
                //Task.Delay(2000);
                var lst = await _dbContext.FuelPrices.ToListAsync();
                _logger.LogInformation($"Thre are {lst.Count} record(s)");
                _logger.LogInformation($"Ended at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }
        }
    }
}
