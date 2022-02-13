using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class DemoDiscordBot : IDiscordBot
{
    private readonly ILogger<DemoDiscordBot> _log;
    private readonly IConfiguration _config;

    // Represents a WebSocket-based Discord client.
    private DiscordSocketClient _client;

    public DemoDiscordBot(ILogger<DemoDiscordBot> log, IConfiguration config)
    {
        _log = log;
        _config = config;
    }

    public void Run()
        => MainAsync().GetAwaiter().GetResult();

    private async Task MainAsync()
    {
        await InitializeClientAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

    private async Task InitializeClientAsync()
    {
        _client = new DiscordSocketClient();

        // Retreive our token value from our User Environment Variables.
        var token = _config.GetValue<string>("DiscordToken");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;

        _log.LogInformation("Discord Service Initialized.");
    }

    private Task LogAsync(LogMessage logMessage)
    {
        _log.LogInformation(logMessage.ToString());

        return Task.CompletedTask;
    }
    private Task ReadyAsync()
    {
        _log.LogInformation($"{_client.CurrentUser} is connected!");

        return Task.CompletedTask;
    }
}