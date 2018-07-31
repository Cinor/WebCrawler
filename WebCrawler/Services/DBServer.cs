using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using WebCrawler.Models;

namespace WebCrawler.Server
{
    public class DBServer
    {

        public void insertNewsData(List<NewsData> NewList)
        {

            using (News_DatabaseEntities _nDB = new News_DatabaseEntities())
            {
                foreach (var item in NewList)
                {

                }
            }
            
        }



    }
}