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
        
        public void Download()
        {
            NewsMain newsMain = new NewsMain();
            newsMain.Page = 10;
            newsMain.DownloadNews();
        }


        public News getOrderDatas()
        {
            
            try
            {

                News newsList = new News()
                {
                    NewsList = dbServer.SelectNewsData().ToList()
                };

                return newsList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newsDatas"></param>
        public void saveOrderDatads(News newsDatas)
        {
            foreach (var item in newsDatas.NewsList)
            {
                dbServer.InsertNewsData(item);
            }
        }

    }
}