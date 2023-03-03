﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

public class Program
{
    private static void Main(string[] args)
    {

        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

        Log.Logger.Information("Application Starting");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
            })
            .UseSerilog()
            .Build();
    }
    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .AddEnvironmentVariables();
    }
}