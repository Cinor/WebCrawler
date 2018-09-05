using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCrawler.Models.ViewModel;
using WebCrawler.Library;

namespace WebCrawler.Controllers
{
    public class NewsController : AsyncController
    {
        OrderLibrary orderLibrary = new OrderLibrary();

        /// <summary>
        /// 非同步進入點
        /// </summary>
        public void DownloadNewsAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            OrderLibrary orderAsync = new OrderLibrary();
            orderAsync.Download(10);

            AsyncManager.OutstandingOperations.Decrement();
        }

        /// <summary>
        /// 非同步結束點
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadNewsCompleted()
        {

            return Redirect("/News/NewsView/");
        }


        //    public ActionResult DownloadNews()
        //{
        //    AsyncManager.OutstandingOperations.Increment();
        //    OrderLibrary orderAsync = new OrderLibrary();
            
        //    orderAsync.Download(5);

        //    return Redirect("/News/NewsView/");
        //}

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