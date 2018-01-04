using Domain;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RssReader.Console
{
    public class ConsoleInterface
    {
        private const string MAINMENU =
            "1: Add RSS source\n" +
            "2: Load sources from database\n" +
            "3: Show sources\n" +
            "4: Load all news\n" +
            "5: Show all news\n" +
            "6: Statistics\n" +
            "0: Exit";

        private readonly IRssService _rssService;

        public ConsoleInterface(IRssService rssService)
        {
            _rssService = rssService;
        }

        public void Show()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            while (true)
            {
                System.Console.WriteLine(MAINMENU);
                System.Console.Write(">");
                var s = System.Console.ReadLine();
                s = s.Trim();
                Int32.TryParse(s, out int comand);
                switch (comand)
                {
                    case 1:
                        AddRssSourceMenu();
                        break;
                    case 2:
                        var sources = _rssService.LoadSourcesFromDb().Result;
                        System.Console.WriteLine($"Uploaded {sources.Count} sources:");
                        foreach (var source in sources)
                        {

                        }
                        break;
                    case 3:
                        ShowSources(RssService.SourcesCache);
                        break;
                    case 4:
                        int newsCount = 0;
                        try
                        {
                            newsCount = _rssService.ReadNewsFromAllSources().Result.Count;
                        }
                        catch (Exception ex)
                        {
                            ShowError(ex.Message);
                        }
                        System.Console.WriteLine($"Uploaded {newsCount} news");
                        break;
                    case 5:
                        var news = _rssService.News.ToListAsync().Result;
                        ShowManyNews(news);
                        break;
                    case 6:
                        var countOfNews = _rssService.News.Count().Result;
                        System.Console.WriteLine($"Count of news in database:{countOfNews}");
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        continue;
                }
            }
        }

        private void ShowManyNews(List<News> news)
        {
            foreach (var n in news)
            {
                ShowNews(n);
            }
        }

        private static void ShowNews(News n)
        {
            System.Console.WriteLine(
                $"Id:{n.Id}\n" +
                $"Title:{n.Title}\n" +
                $"Url:{n.Url}\n" +
                $"Description:{n.Description}\n" +
                $"Date:{n.Date}\n");
        }

        private static void ShowSources(List<RssSource> sources)
        {
            foreach (var source in sources)
            {
                ShowSource(source);
            }
        }

        private static void ShowSource(RssSource source)
        {
            System.Console.WriteLine(
                $"Id:{source.Id}\n" +
                $"Name:{source.Name}\n" +
                $"Url:{source.Url}\n");
        }

        private void AddRssSourceMenu()
        {
            var source = new RssSource();
            System.Console.WriteLine("Add RSS source:");
            System.Console.Write("Name:");
            source.Name = System.Console.ReadLine().Trim();
            System.Console.Write("Url:");
            source.Url = System.Console.ReadLine().Trim();
            System.Console.WriteLine("Loading");
            var result = _rssService.AddSourceAsync(source).Result;
            if (!result.Succeeded)
            {
                ShowError(result.Errors.First());
            }
            else
            {
                System.Console.WriteLine("Completed");
            }
        }

        private static void ShowError(string error)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Error:" + error);
            System.Console.ResetColor();
        }
    }
}
