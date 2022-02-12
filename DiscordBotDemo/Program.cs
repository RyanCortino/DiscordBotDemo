using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Setting up configuartion for Serilog
        ConfigurationBuilder builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        Log.Logger.Information("Application starting..");

        // Setting up Dependency Injections, Configuration, and Logging
        IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Services go here
            })
            .UseSerilog()
            .Build();
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Productions"}.json", optional: true)
            .AddEnvironmentVariables();
    }
}
