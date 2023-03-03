namespace FuelPriceDownloader.Models.FuelPriceResponse
{
    /// <summary>
    /// Represents a series of fuel price data returned by the fuel price API.
    /// </summary>
    public class Series
    {
        /// <summary>
        /// The series ID associated with the fuel price data.
        /// </summary>
        public string series_id { get; set; }

        /// <summary>
        /// The name of the fuel price data series.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The units of measurement for the fuel price data.
        /// </summary>
        public string units { get; set; }

        /// <summary>
        /// The frequency of the fuel price data updates.
        /// </summary>
        public string f { get; set; }

        /// <summary>
        /// The short name of the units of measurement for the fuel price data.
        /// </summary>
        public string unitsshort { get; set; }

        /// <summary>
        /// The description of the fuel price data series.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// The copyright information for the fuel price data series.
        /// </summary>
        public string copyright { get; set; }

        /// <summary>
        /// The source of the fuel price data series.
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// The ISO 3166 code associated with the fuel price data.
        /// </summary>
        public string iso3166 { get; set; }

        /// <summary>
        /// The geographic region associated with the fuel price data.
        /// </summary>
        public string geography { get; set; }

        /// <summary>
        /// The start date of the fuel price data series.
        /// </summary>
        public string start { get; set; }

        /// <summary>
        /// The end date of the fuel price data series.
        /// </summary>
        public string end { get; set; }

        /// <summary>
        /// The date and time when the fuel price data series was last updated.
        /// </summary>
        public DateTime updated { get; set; }

        /// <summary>
        /// An array of fuel price data records.
        /// </summary>
        public object[][] data { get; set; }
    }
}
