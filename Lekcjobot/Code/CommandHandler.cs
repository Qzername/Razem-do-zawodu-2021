using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Victoria;

namespace Code
{
    public class CommandHandler
    {
        private CommandService _Service;
        private DiscordSocketClient _Client;
        IServiceProvider provider;
        public LavaNode lavanode;

        public CommandHandler(DiscordSocketClient Client)
        {
            _Client = Client;
            var services = new ServiceCollection()
                .AddLavaNode(x => {
                    x.Port = 2333;
                    x.Authorization = "youshallnotpass";
                    x.SelfDeaf = false;
                })
                .AddSingleton(_Client);

            provider = services.BuildServiceProvider();

            lavanode = provider.GetRequiredService<LavaNode>();
            lavanode.ConnectAsync();
            lavanode.OnTrackEnded += Lavanode_OnTrackEnded;

            _Service = new CommandService();
            _Service.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
            _Client.MessageReceived += HandleCommandAsync; 
            _Client.Ready += Ready;

            _Client.SetGameAsync("Lekcjobot to bot który przypomina nam o lekcji | Wersja: 1.0v | AUT: uZer#9084");
        }

        private Task Lavanode_OnTrackEnded(Victoria.EventArgs.TrackEndedEventArgs arg)
        {
            lavanode.LeaveAsync(arg.Player.VoiceChannel);
            return Task.CompletedTask;
        }

        async Task Ready()
        {
            try
            {
                Console.WriteLine("Connecting to LavaLink...");
                int i = 0;
                while(!lavanode.IsConnected)
                {
                    await lavanode.ConnectAsync();
                    Console.WriteLine("Attempt: " + ++i);
                }
                Console.WriteLine("Connected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Source + " " + ex.Message);
            }
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

        public void PlaySound(Discord.IGuild guild)
        {
            var vc = guild.GetVoiceChannelAsync(785785408117669898).Result;
            lavanode.JoinAsync(vc);
            var player = lavanode.GetPlayer(guild);
            player.PlayAsync(lavanode.SearchAsync(@"https://www.youtube.com/watch?v=qIQqODMAqfk").Result.Tracks[0]);
        }
    }
}
