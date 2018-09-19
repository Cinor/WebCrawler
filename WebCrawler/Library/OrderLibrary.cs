using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCrawler.Services;
using WebCrawler.Models;
using WebCrawler.Models.ViewModels;
using PagedList;
using System.Threading.Tasks;

namespace WebCrawler.Library
{
    public class OrderLibrary
    {
        DBService dbServer = new DBService();

        /// <summary>
        /// 依據頁碼決定下載的新聞數量
        /// </summary>
        /// <param name="page">頁碼</param>
        public void Downloadpage(int page)
        {

            NewsMain newsMain = new NewsMain();
            newsMain.DownloadNews(10);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<News> GetAllNews()
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
        public News GetNewsByID(Guid ID)
        {
            try
            {
                return dbServer.SelectNewsById(ID);
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        /// <summary>
        /// 依據類型、關鍵字等條件進行Selcect
        /// </summary>
        /// <param name="Types">類型</param>
        /// <param name="Keyword">關鍵字</param>
        /// <returns></returns>
        public NewsViews GetNewsDatasByCondition(string Types, string Keyword, int currentPage)
        {

            var DatasList = GetAllNews();
            var result = DatasList.OrderByDescending(c => c.Time)
                        .Where(c => string.IsNullOrEmpty(Types) ? true : c.Types == Types)
                        .Where(c => string.IsNullOrEmpty(Keyword) ? true : (string.IsNullOrEmpty(c.Content) ? true : c.Content.Contains(Keyword)))
                        .ToList();


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
        /// 寫入資料庫
        /// </summary>
        /// <param name="newsDatas"></param>
        public void SaveNewsData(List<News> newsDatas)
        {

            foreach (var item in newsDatas)
            {
                dbServer.InsertNewsData(item);
            }

        }
        
        /// <summary>
        /// 存入政治人物名單
        /// </summary>
        /// <param name="politician"></param>
        public void CreatePolitician(Politician politician)
        {
            dbServer.InsertPolitician(politician);
        }

        /// <summary>
        /// 取出所有政治人物
        /// </summary>
        /// <returns></returns>
        public List<Politician> GetAllPolitician()
        {
            try
            {
                var result = dbServer.SelectAllPolitician().ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}