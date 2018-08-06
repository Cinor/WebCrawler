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
        /// <param name="InsertNewData"></param>
        public void InsertNewsData(NewsData InsertNewData)
        {

            using (News_DatabaseEntities _nDB = new News_DatabaseEntities())
            {

                NewsDataDB newsDataDB = new NewsDataDB
                {
                    Id = Guid.NewGuid(),
                    Time = InsertNewData.Time,
                    Types = InsertNewData.Types,
                    Head = InsertNewData.Head,
                    Links = InsertNewData.Links,
                    Content = InsertNewData.Content
                };

                try
                {
                    _nDB.NewsDataDB.Add(newsDataDB);
                    _nDB.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        /// <summary>
        /// 依據NewsData ID 更新資料庫資料
        /// </summary>
        /// <param name="updatanewsData"></param>
        public void UpdataNewsData(NewsData updatanewsData)
        {
            using (News_DatabaseEntities _nDB = new News_DatabaseEntities())
            {

                if (_nDB.NewsDataDB.Any(d => d.Id == updatanewsData.ID))
                {
                    var DB_News = _nDB.NewsDataDB.Where(d => d.Id == updatanewsData.ID).Single();

                    DB_News.Id = updatanewsData.ID;
                    DB_News.Time = updatanewsData.Time;
                    DB_News.Types = updatanewsData.Types;
                    DB_News.Head = updatanewsData.Head;
                    DB_News.Links = updatanewsData.Links;
                    DB_News.Content = updatanewsData.Content;


                    try
                    {
                        _nDB.SaveChanges();

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


            }
        }

    }
}