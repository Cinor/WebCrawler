using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCrawler.Services;
using WebCrawler.Models;
using WebCrawler.Models.ViewModel;
using PagedList;

namespace WebCrawler.Library
{
    public class OrderLibrary
    {
        DBService dbServer = new DBService();
        
        /// <summary>
        /// 依據頁碼決定下載的新聞數量
        /// </summary>
        /// <param name="page">頁碼</param>
        public void Download(int page)
        {
            NewsMain newsMain = new NewsMain();
            newsMain.Page = page;
            newsMain.DownloadNews();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Models.ViewModel.News> getOrderDatas()
        {            
            try
            {
                var result = dbServer.SelectAllNews().ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public News getNewsByID(Guid ID)
        {
            return dbServer.SelectNewsById(ID);
        }


        /// <summary>
        /// 依據類型、關鍵字等條件進行Selcect
        /// </summary>
        /// <param name="Types">類型</param>
        /// <param name="Keyword">關鍵字</param>
        /// <returns></returns>
        public NewsViews getNewsDatasByCondition(string Types, string Keyword , int currentPage)
        {

            var DatasList = getOrderDatas();
            var result = DatasList.OrderBy(c => c.Time)
                        .Where(c => string.IsNullOrEmpty(Types) ? true : c.Types == Types)                        
                        .Where(c => string.IsNullOrEmpty(Keyword) ? true : c.Content.Contains(Keyword))
                        .OrderByDescending( c => c.Time ).ToList();
            try
            {
                
                NewsViews newsList = new NewsViews()
                {                    
                    NewsList = result.ToPagedList(currentPage, 20),
                    Types_list = DatasList.Where(x => !String.IsNullOrWhiteSpace(x.Types)).GroupBy(x => x.Types).Select(x => new System.Web.Mvc.SelectListItem { Text = x.Key, Value = x.Key }).ToList()
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
        public void saveNewsData(List<Models.ViewModel.News> newsDatas)
        {
            foreach (var item in newsDatas)
            {
                News_DatabaseEntities _nDB = new News_DatabaseEntities();

                if (! _nDB.NewsDataDB.Any(b => b.Links == item.Links))
                {
                    dbServer.InsertNewsData(item);
                }
                
            }
        }

    }
}