using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using System.Timers;
using Discord.WebSocket;
using Lekcjobot.Code.VulcanAPIIO;
using Lekcjobot.Code.Models;

namespace Lekcjobot.Code
{
    public static class RAMDatabase
    {
        public static List<string> Blacklist;
        public static Dictionary<ulong, ulong> Servers;

        static RAMDatabase()
        {
            //default
            Blacklist = new List<string>();
            Blacklist.Add("p_Sprzedaz towarów");
            Blacklist.Add("Podstawy handlu");

            Servers = new Dictionary<ulong, ulong>();
            Servers.Add(743551390324097054, 776751040887783427);
        }

        public static void RemoveBlacklistedLessons(ref List<Lesson> lessons)
        {
            for (int i = lessons.Count - 1; i > -1; i--)
                foreach (string s in Blacklist)
                    if (lessons[i].lesson == s )
                        lessons.RemoveAt(i);
            for (int i = lessons.Count - 1; i > 0; i--)
                if (lessons[i - 1].lesson == lessons[i].lesson)
                    lessons.RemoveAt(i);
        }
    }
}

namespace Lekcjobot.Code.Modules
{

    public class Base : ModuleBase<SocketCommandContext>
    {
        [Command("addtoblacklist")]
        public async Task atbl([Remainder] string lessonname)
        {
            RAMDatabase.Blacklist.Add(lessonname);
        }

        [Command("addtoservers")]
        public async Task ats([Remainder] string channelid)
        {
            RAMDatabase.Servers.Add(Context.Guild.Id,Convert.ToUInt64(channelid));
        }

        [Command("ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("Pong! Latency: " + Context.Client.Latency);
        }

        [Command("lessons")]
        public async Task Lessons()
        {
            List<Lesson> lessons = new List<Lesson>(VulcanAPI.createLessons(VulcanAPI.runScript("./Code/VulcanAPI/getLessons.py", string.Format("{0} {1} {2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))));
            RAMDatabase.RemoveBlacklistedLessons(ref lessons);
            SendEmbed(MakeEmbed(lessons.ToArray(), "Lekcje cepie"));
        }

        [Command("jaka kurwa lekcja")] 
        public async Task fun()
        {
            await ReplyAsync("grzeczniej kurwa (użyj `lb jaka lekcja moj ulubiony bocie` aby sie dowiedziec)");
        }
        
        [Command("jaka lekcja moj ulubiony bocie")]
        public async Task Nowlesson()
        {
            List<Lesson> lessons = new List<Lesson>(VulcanAPI.createLessons(VulcanAPI.runScript("./Code/VulcanAPI/getLessons.py", string.Format("{0} {1} {2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))));
            RAMDatabase.RemoveBlacklistedLessons(ref lessons);

            var datenow = DateTime.Now;

            int lessonid = -1;

            for(int i = 0; i<lessons.Count;i++)
                if (datenow.Ticks > lessons[i].dateFrom.Ticks && datenow.Ticks < lessons[i].dateTo.Ticks)
                    lessonid = i;
                    
            var closestTime = lessons.OrderBy(t => Math.Abs((t.dateFrom - datenow).Ticks)).First();

            if (lessonid == -1)
            {
                if (datenow.Hour >= 15)
                {
                    await ReplyAsync("NA DZISIAJ NIE MA LEKCJI, WYPIERDALAJ");
                    return;
                }

                await ReplyAsync(string.Format("JEST PRZERWA DEBILU \nNAJBLIŻSZA LEKCJA: {0} \nZACZYNA SIĘ O GODZINIE: {1}", closestTime.lesson, closestTime.dateFrom.ToString()));
            }
            else
                await ReplyAsync(string.Format("JEST LEKCJA HUNCFOCIE\nTERAZ LEKCJA: {0} \nKOŃCZY SIĘ O: {1}", closestTime.lesson, closestTime.dateTo.ToString()));
        }

        [Command("zamknij ryj")]
        public async Task fun2()
        {
            await ReplyAsync("zamknąć to se starego możesz");
        }

        Embed MakeEmbed(Lesson[] lessons, string title)
        {
            var embedbuilder = new EmbedBuilder();

            embedbuilder.Title = title;

            embedbuilder.Description = string.Empty;

            foreach(Lesson l in lessons)
            {
                embedbuilder.Description += string.Format("{0} - Od {1}:{2} do {3}:{4}\n", l.lesson, l.dateFrom.Hour, l.dateFrom.Minute, l.dateTo.Hour, l.dateTo.Minute);
            }

            return embedbuilder.Build();
        }

        void SendEmbed(Embed embed) => Context.Channel.SendMessageAsync(embed: embed);
    }
}
