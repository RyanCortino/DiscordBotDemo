using Discord;
using Discord.Interactions;
using System.Threading.Tasks;

namespace aberrantGeek.VanDamnedBot.Modules
{
    public class GeneralModule : InteractionModuleBase<SocketInteractionContext>
    {
        // By setting the [DefaultPermission()] to false, you can disable the command by default.
        [SlashCommand("ping", "Pings the bot and returns its latency.")]
        [DefaultPermission(true)]
        public async Task PingPongUserAsync()
            => await RespondAsync(text: $":ping_pong: It took me {Context.Client.Latency}ms to respond to you!"
                                  , ephemeral: true);
    }
}