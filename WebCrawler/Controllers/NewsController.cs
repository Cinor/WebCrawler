using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCrawler.Models.ViewModels;
using WebCrawler.Library;
using System.Threading.Tasks;
using WebCrawler.Models;

namespace WebCrawler.Controllers
{
    public class NewsController : AsyncController
    {
        OrderLibrary orderLibrary = new OrderLibrary();

        /// <summary>
        /// 下載蘋果日報新聞
        /// </summary>
        public  void DownloadNewsAsync()
        {

            Task.Run(() => orderLibrary.Downloadpage(10)).FailFastOnException();                        
        }

        /// <summary>
        /// 下載完畢就跳頁
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadNewsCompleted()
        {

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