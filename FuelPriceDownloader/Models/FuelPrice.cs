namespace FuelPriceDownloader.Models
{
    /// <summary>
    /// Represents a fuel price record.
    /// </summary>
    public partial class FuelPrice
    {
        /// <summary>
        /// The date and time when the fuel price record was created or updated.
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// The price of the fuel on the record date.
        /// </summary>
        public double Price { get; set; }
    }
}
