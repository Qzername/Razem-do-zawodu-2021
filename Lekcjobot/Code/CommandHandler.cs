using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class CommandHandler
    {
        private CommandService _Service;
        private DiscordSocketClient _Client;
        IServiceProvider provider;

        public CommandHandler(DiscordSocketClient Client)
        {
            _Client = Client;
            var services = new ServiceCollection()
                .AddSingleton(_Client);

            provider = services.BuildServiceProvider();

            _Service = new CommandService();
            _Service.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
            _Client.MessageReceived += HandleCommandAsync;

            _Client.SetGameAsync("Lekcjobot to bot który przypomina nam o lekcji | Wersja: 1.0v | AUT: uZer#9084");
        }

        private async Task HandleCommandAsync(SocketMessage M)
        {
            var Message = M as SocketUserMessage;
            if (Message == null || Message.Author.IsBot) return;

            var context = new SocketCommandContext(_Client, Message);

            int argPos = 0;

            if (Message.HasStringPrefix("lb ", ref argPos) || Message.HasMentionPrefix(_Client.CurrentUser, ref argPos))
            {
                var result = await _Service.ExecuteAsync(context, argPos, provider);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    await context.Channel.SendMessageAsync("Something went wrong: " + result.ErrorReason);
            }
        }
    }
}
