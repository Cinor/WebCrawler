using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCrawler.Services;
using WebCrawler.Models;
using WebCrawler.ViewModel;

namespace WebCrawler.Library
{
    public class OrderLibrary
    {
        DBService dbServer = new DBService();


        public NewsList getOrderDatas()
        {
            
            try
            {

                NewsList newsList = new NewsList()
                {
                    GetList = dbServer.SelectNewsData().ToList()
                };

                return newsList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}