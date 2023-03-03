namespace FuelPriceDownloader.Models.FuelPriceResponse
{
    /// <summary>
    /// Represents the response from the fuel price API.
    /// </summary>
    public class FuelPriceResponse
    {
        /// <summary>
        /// The request information associated with the fuel price response.
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// An array of fuel price series containing the fuel price data.
        /// </summary>
        public Series[] Series { get; set; }
    }
}
