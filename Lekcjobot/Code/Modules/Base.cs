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
                if (lessons[i - 1].dateFrom == lessons[i].dateFrom)
                    lessons.RemoveAt(i);
        }
    }
}

namespace Lekcjobot.Code.Modules
{

    public class Base : ModuleBase<SocketCommandContext>
    {
        [Command("testmarks")]
        public async Task testmarks()
        {
            await ReplyAsync(VulcanAPI.runScript("./Code/VulcanAPI/getMarks.py", "cert"));
        }

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

        [Command("zamknij ryj")]
        public async Task fun2()
        {
            await ReplyAsync("zamknąć to se starego możesz");
        }
    }
}
