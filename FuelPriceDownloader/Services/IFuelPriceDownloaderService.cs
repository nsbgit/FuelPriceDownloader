namespace FuelPriceDownloader.Services
{
    public interface IFuelPriceDownloaderService
    {
        Task DownloadFuelPricesAsync();
    }
}