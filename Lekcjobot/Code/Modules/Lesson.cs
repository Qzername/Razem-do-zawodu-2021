using Discord;
using Discord.Commands;
using Lekcjobot.Code.Models;
using Lekcjobot.Code.VulcanAPIIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekcjobot.Code.Modules
{
    public class LessonModule : ModuleBase<SocketCommandContext>
    {
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
            await Nowlesson();
        }

        [Command("lessonnow")]
        public async Task Nowlesson()
        {
            List<Lesson> lessons = new List<Lesson>(VulcanAPI.createLessons(VulcanAPI.runScript("./Code/VulcanAPI/getLessons.py", string.Format("{0} {1} {2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))));
            RAMDatabase.RemoveBlacklistedLessons(ref lessons);

            var datenow = DateTime.Now;

            int lessonid = -1;

            for (int i = 0; i < lessons.Count; i++)
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

        Embed MakeEmbed(Lesson[] lessons, string title)
        {
            var embedbuilder = new EmbedBuilder();

            embedbuilder.Title = title;

            embedbuilder.Description = string.Empty;

            foreach (Lesson l in lessons)
            {
                embedbuilder.Description += string.Format("{0} - Od {1}:{2} do {3}:{4}\n", l.lesson, l.dateFrom.Hour, l.dateFrom.Minute, l.dateTo.Hour, l.dateTo.Minute);
            }

            return embedbuilder.Build();
        }

        void SendEmbed(Embed embed) => Context.Channel.SendMessageAsync(embed: embed);
    }
}
