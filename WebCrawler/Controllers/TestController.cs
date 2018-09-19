using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebCrawler.Library;
using WebCrawler.Models;

namespace WebCrawler.Controllers
{
    public class TestController : AsyncController
    {
        OrderLibrary orderLibrary = new OrderLibrary();


        public void TestAsync()
        {
            Task.Run(() => orderLibrary.Downloadpage(10)).FailFastOnException();

        }

        public ActionResult TestCompleted()
        {

            return Redirect("/News/NewsView/");
        }


        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


    }
}