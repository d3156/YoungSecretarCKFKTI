using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace YoungSecretarCKFKTI.Modules
{

    static class Links
    {
        private static List<string> LinksArray;
        public static string Link { 
            set 
            { 
                if (LinksArray == null)
                {
                    LinksArray = new List<string>();
                    if (File.Exists("Links.txt")) LinksArray.AddRange(File.ReadAllLines("Links.txt"));
                }
                LinksArray.Add(value);
                if (File.Exists("Links.txt")) File.Delete("Links.txt");
                File.WriteAllLines("Links.txt", LinksArray);
            } 
        }

        public static List<string> GetLinks
        {
            get
            {
                if (LinksArray == null)
                {
                    LinksArray = new List<string>();
                    if (File.Exists("Links.txt")) LinksArray.AddRange(File.ReadAllLines("Links.txt"));
                }
                return LinksArray;
            }
        }
    }


    public class Commands : ModuleBase<SocketCommandContext>            
    {
        [Command("удачного дня")]
        public async Task GoodDay()=> await ReplyAsync("И тебе удачного дня, товарищ!");
        [Command("добавьте ссылку в список")]
        public async Task AddLink(string Link = null)
        {
            if (Link == null) { await ReplyAsync("Выражайтесь корректнее, пожалуйста"); return; }
            Links.Link = Link;
            await ReplyAsync("Добавлено, товарищи!");
        }

        [Command("предоставьте список ссылок")]
        public async Task ShowLink()
        {
            string message = "Вот выписка из архива:\n";
            foreach (var item in Links.GetLinks) message += item + "\n";
            await ReplyAsync(message);
        }

        [Command("спасибо")] public async Task Thancks() => await ReplyAsync("Я просто делаю свою работу, товарищ!");
        [Command("помощь")] public async Task Help() => await ReplyAsync("удачного дня - пожелать удачного дня" +
            "\nдобавьте ссылку в список - добавление интернет-ссылки и иной информации в партийный архив" +
            "\nпредоставьте список ссылок - выписка из архива" +
            "\nспасибо - поблагодарить секретаря за помощь" +
            "\nнайти в архиве - поиск записи в архиве");
        [Command("найти в архиве")]
        public async Task FindLink(string Link = null)
        {
            if (Link == null) { await ReplyAsync("Выражайтесь корректнее, пожалуйста"); return; }
            var arhive = Links.GetLinks;
            var finded = arhive.FindAll(x => x.Contains(Link));
            string message = "Вот что удалось найти в архиве:\n";
            foreach (var item in finded) message += item + "\n";
            await ReplyAsync(message);
        }
    }
}
