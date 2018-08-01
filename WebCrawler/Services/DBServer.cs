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
                    results = _nDB.NewsDataDB.Select( row => row ).ToList();
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
                        _nDB.NewsDataDB.Add(item);
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