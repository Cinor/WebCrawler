using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using WebCrawler.Models;

namespace WebCrawler.Services
{
    public class DBService
    {
        /// <summary>
        /// 至資料庫取出所有NewsData資料
        /// </summary>
        /// <returns></returns>
        public List<NewsData> SelectNewsData()
        {
            List<NewsData> results = null;
            using (News_DatabaseEntities _nDB = new News_DatabaseEntities())
            {
                try
                {
                    results = (from DB in _nDB.NewsDataDB
                              select new NewsData
                              {                                  
                                  Time = DB.Time,
                                  Types = DB.Types,
                                  Head = DB.Head,
                                  Links = DB.Links,
                                  Content = DB.Content
                              }).ToList();


                    //results = _nDB.NewsDataDB.Select( row => row ).ToList();

                    return results;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        
        /// <summary>
        /// NewsData存入至資料庫中
        /// </summary>
        /// <param name="NewList"></param>
        public void InsertNewsData(List<NewsData> NewList)
        {

            using (News_DatabaseEntities _nDB = new News_DatabaseEntities())
            {
                try
                {
                    foreach (var item in NewList)
                    {
                        NewsDataDB newsDataDB = new NewsDataDB
                        {
                            Id = Guid.NewGuid(),
                            Time = item.Time,
                            Types = item.Types,
                            Head = item.Head,
                            Links = item.Links,
                            Content = item.Content
                        };
                        _nDB.NewsDataDB.Add(newsDataDB);
                        _nDB.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
 
            }
            
        }

        public void UpdataNewsData()
        {

        }

    }
}