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

        // By setting the [DefaultPermission()] to false, you can disable the command by default.
        [SlashCommand("ping", "Pings the bot and returns its latency.")]
        [DefaultPermission(true)]
        public async Task PingPongUserAsync()
            => await RespondAsync(text: $":ping_pong: It took me {Context.Client.Latency}ms to respond to you!"
                                  , ephemeral: true);

        // [Summary] lets you customize the name and the description of a parameter
        [SlashCommand("echo", "Repeat the input")]
        public async Task Echo(string echo, [Summary(description: "mention the user")] bool mention = false)
            => await RespondAsync(echo + (mention ? Context.User.Mention : string.Empty));

        [Group("test_group", "This is a command group")]
        public class GroupExample : InteractionModuleBase<SocketInteractionContext>
        {
            [SlashCommand("choose", "enums create choices")]
            public async Task ChoiceExampleAsync(ExampleEnum input)
                => await RespondAsync($"You choose {input}");
        }

        // User Commands can only have one parameter, which must be a type of SocketUser
        [UserCommand("SayHello")]
        public async Task SayHello(IUser user)
            => await RespondAsync($"{user.Mention}, hello!");

        [UserCommand("Examine")]
        public async Task ExamineUserAsync(IUser user)
            => await RespondAsync(text: $":wave: {Context.User.Username} examines you, <@{user.Id}>!");
    }

    public enum ExampleEnum {
        choice1,
        choice2,
        choice3
    }
}