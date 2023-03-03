using FuelPriceDownloader.Models;
using FuelPriceDownloader.Options;
using FuelPriceDownloader.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Configuration;
using Quartz;
using FuelPriceDownloader.Jobs;

public class Program
{
    private static void Main(string[] args)
    {

        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                //.WriteTo.Console()
                .CreateLogger();

        try
        {
            Log.Logger.Information("Starting host...");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Setup configuration options
                    services.Configure<AppSettingOptions>(context.Configuration.GetSection(AppSettingOptions.AppSettingsKey));

                    // Setup EntityFramework
                    services.AddDbContext<FuelPriceDbContext>(options =>
                    {
                        options.UseSqlServer(context.Configuration.GetConnectionString("FuelPriceDb"));
                    });

                    // DI Services
                    services.AddTransient<IPrintService, PrintService>();

                    // Set up Quartz.Net Scheduler
                    services.AddQuartz(q =>
                    {
                        // Setup DI in Quartz
                        q.UseMicrosoftDependencyInjectionJobFactory();

                        var jobKey = new JobKey("SampleJob");
                        //var jobKey = new JobKey("DownloadFuelPricesJob");

                        // Add Job
                        q.AddJob<SampleJob>(options => options.WithIdentity(jobKey));

                        // Add trigger
                        q.AddTrigger(options => options
                            .ForJob(jobKey)
                            .WithIdentity($"{jobKey}-trigger")
                            .WithCronSchedule("0/3 * * * * ?")
                        );
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                })
                .UseSerilog()
                .Build();

            //var svc = ActivatorUtilities.CreateInstance<PrintService>(host.Services);
            //svc.PrintCount();

            using var scope = host.Services.CreateScope();

            host.Run();

            Log.Information("Shutting down host...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "There was a problem starting the service");
            return;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .AddEnvironmentVariables();
    }
}