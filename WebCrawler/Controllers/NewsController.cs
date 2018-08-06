using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCrawler.ViewModel;
using WebCrawler.Library;

namespace WebCrawler.Controllers
{
    public class NewsController : Controller
    {
        OrderLibrary orderLibrary = new OrderLibrary();

        // GET: News
        public ActionResult ViewNews()
        {
            //orderLibrary.Download();

            var news = orderLibrary.getOrderDatas();

            return View(news);
        }

        [HttpGet]
        public ActionResult ViewNews(string Types, string Keyword, int page)
        {
            int currentPage = page < 1 ? 1 : page;
            var news = orderLibrary.getNewsDatasByCondition(Types, Keyword, currentPage);

            return View(news);
        }



    }
}