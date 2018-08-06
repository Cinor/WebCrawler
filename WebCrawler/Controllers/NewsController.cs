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
            news.Days = DateTime.Now.ToString();

            return View(news);
        }

        //[HttpPost]
        //public ActionResult ViewNews()
        //{

        //    var news = orderLibrary.getOrderDatas();
        //    news.Days = DateTime.Now.ToString();

        //    return View(news);
        //}



    }
}