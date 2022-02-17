using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace aberrantGeek.DiscordBot
{
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
                    services.AddSingleton<DiscordSocketClient>();
                    services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
                    services.AddSingleton<CommandHandler>();
                    services.AddSingleton<DemoDiscordBot>();
                })
                .UseSerilog()
                .Build();

            ICustomService commandHandler = host.Services.GetRequiredService<CommandHandler>();
            commandHandler.Run();

            ICustomService discordBot = host.Services.GetRequiredService<DemoDiscordBot>();
            discordBot.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Productions"}.json", optional: true)
                .AddEnvironmentVariables();
        }

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}