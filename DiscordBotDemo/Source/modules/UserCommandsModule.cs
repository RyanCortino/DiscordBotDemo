using Discord;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aberrantGeek.VanDamnedBot.Modules
{
    public class UserCommandsModule : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly VanDamnedBot _vanDamme;

        public UserCommandsModule(VanDamnedBot vanDamnedBot)
        {
            _vanDamme = vanDamnedBot;
        }

        [UserCommand("Insult")]
        public async Task InsultUser(IUser user)
            => await RespondAsync($"Hey {user.Mention}! {Context.User.Username} says {_vanDamme.NextInsult}");

    }
}