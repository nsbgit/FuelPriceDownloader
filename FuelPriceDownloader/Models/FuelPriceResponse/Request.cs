namespace FuelPriceDownloader.Models.FuelPriceResponse
{
    /// <summary>
    /// Represents the request information associated with a fuel price response.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// The command used to retrieve the fuel price data.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The series ID used to identify the fuel price data.
        /// </summary>
        public string Series_id { get; set; }
    }
}
