using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace aberrantGeek.DiscordBot
{
    public class DemoDiscordBot : BaseService
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;

        public DemoDiscordBot(DiscordSocketClient client, InteractionService commands, ILogger<DemoDiscordBot> logger, IConfiguration config)
            : base(logger, config)
        {            
            _client = client;
            _commands = commands;
        }

        public override void Run()
            => MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {            
            await InitializeDiscordBotAsync();

            // Block this task until the program is closed.
            await Task.Delay(Timeout.Infinite);
        }

        private async Task InitializeDiscordBotAsync()
        {
            // Retreive our token value from our User Environment Variables.
            var token = _config.GetValue<string>("DiscordToken");

            _commands.Log += LogAsync;
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.Ready += async () =>
            {
                if (Program.IsDebug())
                    // ID of the test guild can be provided from the Config
                    await _commands.RegisterCommandsToGuildAsync(_config.GetValue<ulong>("testGuildID"), true);
                else
                    await _commands.RegisterCommandsGloballyAsync(true);
            };

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }

        private Task LogAsync(LogMessage logMessage)
        {
            _logger.LogInformation(logMessage.ToString());

            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            _logger.LogInformation($"{_client.CurrentUser} is connected!");

            return Task.CompletedTask;
        }
    }
}