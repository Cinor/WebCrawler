using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebCrawler.Library;
using WebCrawler.Models.ViewModels;

namespace WebCrawler.Controllers
{
    public class PoliticianController : AsyncController
    {
        OrderLibrary _orderLibrary = new OrderLibrary();

        public void StatisticsAsync()
        {
            Task.Run(() => _orderLibrary.Statistics());

        }

        public ActionResult StatisticsCompleted()
        {
            //TempData["message"] = "統計完成，請重新整理頁面。";

            return Redirect("/Politician/PoliticianView/");
        }
        
        [HttpGet]
        // GET: Politician
        public ActionResult PoliticianView()
        {
            var politician = _orderLibrary.GetAllPolitician();
            return View("PoliticianView", politician);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Politician politician)
        {
            //設定0
            politician.Amount = 0;

            _orderLibrary.SavePolitician(politician);

            return RedirectToAction("PoliticianView");
        }

        
        public ActionResult Updeta()
        {
            return View();
        }


        public ActionResult Details(String Name)
        {
            //轉址至NewsController/NewsView
            return RedirectToAction("NewsView", "News", new { @Keyword = Name });

        }


        public ActionResult Delete(Guid ID)
        {
            _orderLibrary.DeletePolitician(ID);

            return RedirectToAction("PoliticianView");
        }
    }
}