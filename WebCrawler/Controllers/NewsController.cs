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

        // GET: News
        public ActionResult NewsView()
        {
            //orderLibrary.Download();

            var news = orderLibrary.getOrderDatas();

            return View(news);
        }

        [HttpGet]
        public ActionResult NewsView(string Types, string Keyword, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var news = orderLibrary.getNewsDatasByCondition(Types, Keyword, currentPage);

            return View(news);
        }



    }
}