using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class DemoDiscordBot : IDiscordBot
{
    private readonly ILogger<DemoDiscordBot> _log;
    private readonly IConfiguration _config;

    private DiscordSocketClient _client;

    public DemoDiscordBot(ILogger<DemoDiscordBot> log, IConfiguration config)
    {
        _log = log;
        _config = config;
    }

    public void Run()
        => Main().GetAwaiter().GetResult();

    private async Task Main()
    {
        await Initialize();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }
    private async Task Initialize()
    {
        _log.LogInformation("Discord Service Initializing.");

        _client = new DiscordSocketClient();
        _client.Log += Log;

        // Retreive our token value from our User Environment Variables.
        var token = _config.GetValue<string>("DiscordToken");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        _client.MessageUpdated += MessageUpdated;
        _client.Ready += () =>
        {
            _log.LogInformation("Discord connected with Token: {0}", token.ToString());
            return Task.CompletedTask;
        };

        _log.LogInformation("Discord Service Initialized.");
    }

    private Task Log(LogMessage logMessage)
    {
        _log.LogInformation(logMessage.ToString());

        return Task.CompletedTask;
    }

    private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
    {
        var message =
            await before.GetOrDownloadAsync();

        _log.LogInformation($"{message} -> {after}");
    }
}