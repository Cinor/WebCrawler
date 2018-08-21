using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCrawler.Models.ViewModel;
using WebCrawler.Library;

namespace WebCrawler.Controllers
{
    public class NewsController : Controller
    {
        OrderLibrary orderLibrary = new OrderLibrary();

        public ActionResult DownloadNews()
        {
            orderLibrary.Download(5);

            return Redirect("/News/NewsView/");
        }

        [HttpGet]
        public ActionResult NewsView(string Types_list, string Keyword, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var news = orderLibrary.getNewsDatasByCondition(Types_list, Keyword, currentPage);

            return View(news);
        }


        public ActionResult Details(Guid ID)
        {
            var news = orderLibrary.getNewsByID(ID);
            return View(news);
        }

    }
}