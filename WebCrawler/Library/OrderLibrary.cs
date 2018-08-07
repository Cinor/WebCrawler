﻿using System;
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
        
        public void Download()
        {
            NewsMain newsMain = new NewsMain();
            newsMain.Page = 10;
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
                var result = dbServer.SelectNewsData().ToList();

                return result;
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
        public NewsViews getNewsDatasByCondition(string Types, string Keyword , int currentPage)
        {

            var DatasList = getOrderDatas();
            var result = DatasList.OrderBy(c => c.Time)
                        .Where(c => string.IsNullOrEmpty(Types) ? true : c.Types == Types)                        
                        .Where(c => string.IsNullOrEmpty(Keyword) ? true : c.Content.Contains(Keyword)).ToList();
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
        /// 根據"關鍵字"搜尋內文
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public NewsViews getOrdersDatasbyContent(String content)
        {
            try
            {
                NewsViews newsList = new NewsViews()
                {
                    //NewsList = dbServer.SelectNewsData().Where(b => b.Content == content).ToList()
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
        public void saveOrderDatads(List<Models.ViewModel.News> newsDatas)
        {
            foreach (var item in newsDatas)
            {
                dbServer.InsertNewsData(item);
            }
        }

    }
}