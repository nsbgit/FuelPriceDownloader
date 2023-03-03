namespace FuelPriceDownloader.Models.FuelPriceResponse
{
    public class FuelPriceResponse
    {
        public Request Request { get; set; }
        public Series[] Series { get; set; }
    }
}
