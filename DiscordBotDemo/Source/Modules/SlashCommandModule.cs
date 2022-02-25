using Discord.Interactions;
using System;
using System.Threading.Tasks;

namespace aberrantGeek.DiscordBot.Modules
{
    public class SlashCommandModule : InteractionModuleBase<SocketInteractionContext>
    {
        /// <summary>
        ///     Will be called before execution. Here you can populate several entities you may want to
        ///     retrieve before executing a command. 
        ///     
        ///         I.E. database objects
        /// </summary>
        /// <param name="command"></param>
        public override void BeforeExecute(ICommandInfo command)
        {
            throw new NotImplementedException();
        }

        public override void AfterExecute(ICommandInfo command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to add a user to the database using their UID.
        /// </summary>
        /// <returns>Returns true </returns>
        [SlashCommand("register", "Registers a user with the game service and responds with the result.")]
        [DefaultPermission(true)]
        public async Task RegisterUserAsync()
        {
            await RespondAsync(text: $"Attempting to register {Context.User}..");
        }
    }
}