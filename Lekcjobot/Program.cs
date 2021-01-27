using Code;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Lekcjobot.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Lekcjobot
{
    class Program
    {
        private readonly DiscordSocketClient _client;
        private CommandHandler comm;
        private readonly CommandService service;
        public Timer lessonTimer;

        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            _client = new DiscordSocketClient();
            service = new CommandService();
            comm = new CommandHandler(_client);
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;

            lessonTimer = new Timer();
            lessonTimer.Interval = 5000; //900000 = 15 min, 60000 = 1 min
            lessonTimer.Elapsed += EpisodeTimerElapsed;
            lessonTimer.Enabled = true;
        }

        public async Task MainAsync()
        {
            await _client.LoginAsync(TokenType.Bot, "ODAzOTY5NTc4NzUyOTMzOTA4.YBFhBA.rpl1HUCpeEaORNh3TYO2H1XKMrc");
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");
            return Task.CompletedTask;
        }

        void EpisodeTimerElapsed(object sender, ElapsedEventArgs e)
        {
            List<SocketGuild> servers = _client.Guilds.ToAsyncEnumerable().ToEnumerable().ToList();

            Console.WriteLine("s");

            lessonTimer.Interval += 5000;

            foreach (SocketGuild guild in servers)
            {
                if (RAMDatabase.Servers.Any(x => x.Key == guild.Id))
                {
                    var channel = guild.GetTextChannel(RAMDatabase.Servers[guild.Id]);
                    channel.SendMessageAsync("seksik " + lessonTimer.Interval);
                }
            }
        }
    }
}
