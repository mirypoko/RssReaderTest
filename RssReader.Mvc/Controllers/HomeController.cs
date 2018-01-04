using Domain;
using RssReader.Mvc.ViewModels;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RssReader.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRssService _rssService;

        public HomeController(IRssService rssService)
        {
            _rssService = rssService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Sources = await _rssService.RssSources.ToListAsync();
            return View();
        }

        public async Task<ActionResult> LoadNews(RssFilterViewModel model)
        {
            var result = new List<News>();
            if (ModelState.IsValid)
            {
                if (model.Source == "all")
                {
                    var sources = await _rssService.RssSources.ToListAsync();
                    result = await _rssService.News.ToListAsync();
                    foreach (var item in result)
                    {
                        item.RssSource = sources.Find(s => s.Id == item.RssSourceId);
                    }
                }
                else
                {
                    if (!Int64.TryParse(model.Source, out long sourceId))
                    {
                        return new HttpStatusCodeResult(400);
                    }
                    var source = await _rssService.RssSources.FirstAsync(s => s.Id == sourceId);
                    if (source == null)
                    {
                        return new HttpStatusCodeResult(400);
                    }
                    result = await _rssService.News.SearchAsync(n => n.RssSourceId == sourceId);
                    foreach (var item in result)
                    {
                        item.RssSource = source;
                    }
                }

                if (model.Sort == 0)
                {
                    result = result.OrderBy(r => r.Date).ToList();
                }
                else
                {
                    result = result.OrderBy(r => r.RssSourceId).ToList();
                }

                return PartialView(result);
            }

            return new HttpStatusCodeResult(400);
        }
    }
}