using Discord;
using Discord.Interactions;
using System.Threading.Tasks;

namespace aberrantGeek.DiscordBot.Modules
{
    public class GeneralModule : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly CommandHandler _handler;

        public GeneralModule(CommandHandler handler)
        {
            _handler = handler;
        }

        // When declaring SlashCommands, we need to provide a name and a description, both following the Discord guidelines
        [SlashCommand("ping", "Receive a pong")]
        [DefaultPermission(true)]
        public async Task Ping()
        {
            await RespondAsync("pong");        
        }

        // [Summary] lets you customize the name and the description of a parameter
        [SlashCommand("echo", "Repeat the input")]
        public async Task Echo(string echo, [Summary(description: "mention the user")]bool mention = false)
        {
            await RespondAsync(echo + (mention ? Context.User.Mention : string.Empty));
        }

        // User Commands can only have one parameter, which must be a type of SocketUser
        [UserCommand("SayHello")]
        public async Task SayHello(IUser user)
        {
            await RespondAsync($"{user.Mention}, hello!");
        }
    }
}