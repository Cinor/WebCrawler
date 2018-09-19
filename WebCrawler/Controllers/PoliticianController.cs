using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCrawler.Library;
using WebCrawler.Models.ViewModels;

namespace WebCrawler.Controllers
{
    public class PoliticianController : Controller
    {
        OrderLibrary orderLibrary = new OrderLibrary();

        [HttpGet]
        // GET: Politician
        public ActionResult PoliticianView()
        {
            var politician = orderLibrary.GetAllPolitician();
            return View();
        }

        public ActionResult Create(Politician politician)
        {
            orderLibrary.CreatePolitician(politician);

            return View("Create");
        }

        
        public ActionResult Updeta()
        {
            return View();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return View();
        }
    }
}