using Code;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Lekcjobot.Code;
using Lekcjobot.Code.Models;
using Lekcjobot.Code.VulcanAPIIO;
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
        List<Lesson> lessons;
        DateTime checklessontime;

        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            checklessontime = DateTime.Now;
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

           // checklessontime = new DateTime(checklessontime.Year, checklessontime.Month, checklessontime.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            CheckForLessons(ref lessons);
            RAMDatabase.RemoveBlacklistedLessons(ref lessons);

            var lessonsInFuture = lessons.FindAll(x => x.dateFrom.Ticks - DateTime.Now.Ticks > 0);
            var lessonNow = lessons.Find(x => x.dateFrom.Ticks < DateTime.Now.Ticks && x.dateTo.Ticks > DateTime.Now.Ticks);

            TimeSpan ts = lessonsInFuture[0].dateFrom - DateTime.Now;
            lessonTimer.Interval =  ts.TotalMilliseconds + 1000;

            if (lessonNow is null)
                return;

            foreach (SocketGuild guild in servers)
            {
                if (RAMDatabase.Servers.Any(x => x.Key == guild.Id))
                {
                    var channel = guild.GetTextChannel(RAMDatabase.Servers[guild.Id]);
                    channel.SendMessageAsync("@informatyk LEKCJA KURWY: " + lessonNow.lesson);
                }
            }
        }

        void CheckForLessons(ref List<Lesson> lessons)
        {
            lessons = new List<Lesson>(VulcanAPI.createLessons(VulcanAPI.runScript("./Code/VulcanAPI/getLessons.py", string.Format("{0} {1} {2}",checklessontime.Year, checklessontime.Month, checklessontime.Day))));
            Console.WriteLine(lessons[0].lesson);

            while (lessons.FindAll(x => x.dateFrom < checklessontime).Count == 0)
            {
                checklessontime = new DateTime(checklessontime.Year, checklessontime.Month, checklessontime.Day+1, 0, 0, 0);
                lessons = new List<Lesson>(VulcanAPI.createLessons(VulcanAPI.runScript("./Code/VulcanAPI/getLessons.py", string.Format("{0} {1} {2}", checklessontime.Year, checklessontime.Month, checklessontime.Day))));
            }
        }
    }
}
