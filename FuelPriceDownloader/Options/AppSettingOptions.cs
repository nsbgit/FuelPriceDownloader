namespace FuelPriceDownloader.Options
{
    /// <summary>
    /// Represents the application settings options for the FuelPriceDownloader application.
    /// </summary>
    public class AppSettingOptions
    {
        /// <summary>
        /// The key for the app settings section in the configuration file.
        /// </summary>
        public const string AppSettingsKey = "AppSettings";

        /// <summary>
        /// The number of seconds to delay the execution of the FuelPriceDownloader task.
        /// </summary>
        public int TaskExecutionDelaySeconds { get; set; }

        /// <summary>
        /// The number of days to keep fuel price records in the database.
        /// </summary>
        public int DaysCount { get; set; }

        /// <summary>
        /// The URL of the API that provides the fuel price data.
        /// </summary>
        public string ApiUrl { get; set; }
    }
}
