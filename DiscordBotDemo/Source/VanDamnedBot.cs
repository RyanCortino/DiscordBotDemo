using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace aberrantGeek.VanDamnedBot
{
    public class VanDamnedBot : BaseService
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly List<Insult> _insults;

        public string NextInsult
            => _insults[0].value.ToString();

        public VanDamnedBot(DiscordSocketClient client, InteractionService commands, ILogger<VanDamnedBot> logger, IConfiguration config)
            : base(logger, config)
        {            
            _client = client;
            _commands = commands;

            _insults = new List<Insult>();
        }

        public override void Run()
            => MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            await DeserializeInsultsAsync();
            await InitializeDiscordBotAsync();

            // Block this task until the program is closed.
            await Task.Delay(Timeout.Infinite);
        }

        private async Task DeserializeInsultsAsync()
        {
            using FileStream openStream 
                = File.OpenRead($"{Directory.GetCurrentDirectory()}/Source/data/{_config.GetValue<string>("JsonFileName")}");

            var insult = await JsonSerializer.DeserializeAsync<Insult>(openStream);

            _insults.Clear();
            _insults.Add(insult);

            _logger.LogInformation($"VanDamnedBot.DeserializeInsults resulted in {_insults.Count} entries being loaded.");
        }

        private async Task InitializeDiscordBotAsync()
        {
            // Retreive our token value from our User Environment Variables.
            var token = _config.GetValue<string>("VanDamnedToken");

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