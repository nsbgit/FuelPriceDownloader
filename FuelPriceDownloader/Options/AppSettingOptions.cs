using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPriceDownloader.Options
{
    public class AppSettingOptions
    {
        public const string AppSettingsKey = "AppSettings"; 
        public int TaskExecutionDelaySeconds { get; set; }
        public int DaysCount { get; set; }
        public string ApiUrl { get; set; }
    }
}
