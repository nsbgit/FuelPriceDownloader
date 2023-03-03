namespace FuelPriceDownloader.Services
{
    /// <summary>
    /// Defines the interface for a service that downloads fuel prices from an API and stores them in a database.
    /// </summary>
    public interface IFuelPriceDownloaderService
    {
        /// <summary>
        /// Downloads the fuel prices from the API and stores them in the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous download and storage of the fuel prices.</returns>
        Task DownloadFuelPricesAsync();
    }
}
