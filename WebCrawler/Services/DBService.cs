using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using WebCrawler.Models;
using WebCrawler.Models.ViewModel;

namespace WebCrawler.Services
{
    public class DBService
    {
        /// <summary>
        /// 至資料庫取出所有NewsData資料
        /// </summary>
        /// <returns></returns>
        public List<News> SelectAllNews()
        {
            List<News> results = null;
            using (News_DatabaseEntities _nDB = new News_DatabaseEntities())
            {
                try
                {
                    results = (from DB in _nDB.NewsDataDB
                               select new News
                               {
                                   Id = DB.Id,
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
        /// 依據ID進行Select
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public News SelectNewsById(Guid ID)
        {
            using (News_DatabaseEntities _nDB = new News_DatabaseEntities())
            {
                if (_nDB.NewsDataDB.Any(b => b.Id == ID))
                {
                    var result = _nDB.NewsDataDB.Where(b => b.Id == ID).Single();

                    News news = new News()
                    {
                        Id = result.Id,
                        Time = result.Time,
                        Types = result.Types,
                        Head = result.Head,
                        Links = result.Links,
                        Content = result.Content
                    };

                    return news;
                }
                else
                {
                    return null;
                }


            }

        }

        /// <summary>
        /// NewsData存入至資料庫中
        /// </summary>
        /// <param name="InsertNewData"></param>
        public void InsertNewsData(News InsertNewData)
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
        /// 依據NewsData的ID進行更新資料作業
        /// </summary>
        /// <param name="updatanews"></param>
        public void UpdataNewsData(News updatanews)
        {
            using (News_DatabaseEntities _nDB = new News_DatabaseEntities())
            {

                if (_nDB.NewsDataDB.Any(d => d.Id == updatanews.Id))
                {
                    var DB_News = _nDB.NewsDataDB.Where(d => d.Id == updatanews.Id).Single();

                    DB_News.Id = updatanews.Id;
                    DB_News.Time = updatanews.Time;
                    DB_News.Types = updatanews.Types;
                    DB_News.Head = updatanews.Head;
                    DB_News.Links = updatanews.Links;
                    DB_News.Content = updatanews.Content;


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