using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading;
using WebCrawler.Models.ViewModels;
using WebCrawler.Library;

namespace WebCrawler.Models
{
    public class NewsMain
    {
        OrderLibrary orderLibrary = new OrderLibrary();

        /// <summary>
        /// 依據頁面數量一頁一頁抓取新聞
        /// </summary>
        public void DownloadNews(int page)
        {            
            int p = 0;
            while (p <= page)
            {
                DownloadNewsData(p);
                p++;
            }
        }

        /// <summary>
        /// 抓取新聞列表、各個新聞類別
        /// </summary>
        /// <param name="page"></param>
        public void DownloadNewsData(int page)
        {
            string link;

            //蘋果日報網址
            link = "https://tw.appledaily.com/new/realtime/";

            link = link + page.ToString();
            
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(link);

                //新聞資料 (網址 + 時間 + 類型 )
                var nodeData = doc.DocumentNode.SelectNodes("//div[@class='abdominis rlby clearmen']/ul[1]/li");

                NewsViews newsList = new NewsViews();

                List<News> newsData = new List<News>();

                foreach (var nsd in nodeData)
                {
                    News news = new News
                    {
                        //建立key值
                        Id = Guid.NewGuid(),

                        //抓取類型
                        Types = nsd.SelectSingleNode("./a/h2").InnerText
                    };

                    //抓取網址
                    var newlinks = nsd.SelectSingleNode("./a").Attributes["href"].Value;
                    news.Links = newlinks;

                    //抓取內文
                    var DNC = DownloadNewsContent(newlinks);
                    news.Content = DNC["新聞內文"];

                    //新聞時間 -> 年/月/日 + 時:分
                    news.Time = Convert.ToDateTime(DNC["內文時間"].Substring(5));

                    //抓取標題
                    //news.Head = nsd.SelectSingleNode("./a/h1").InnerText;
                    news.Head = DNC["內文標題"];

                    newsData.Add(news);

                    Thread.Sleep(1);
                }

                orderLibrary.SaveNewsData(newsData);

            }
            catch (System.IndexOutOfRangeException e)
            {
                System.Console.WriteLine(e.Message);
                throw new System.ArgumentOutOfRangeException("陣列項目錯誤：", e);
            }


        }

        /// <summary>
        /// 抓取 內文標題、內文時間、內文內容
        /// </summary>
        /// <param name="Link"></param>
        /// <returns></returns>
        private Dictionary<string, string> DownloadNewsContent(String Link)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(Link);
                Dictionary<string, string> DNContent = new Dictionary<string, string>();
                                
                //內文標題
                var nodeContentHead = doc.DocumentNode.SelectSingleNode("//article[@class='ndArticle_leftColumn']/hgroup/h1").InnerText;
                DNContent.Add("內文標題", nodeContentHead);
                
                //內文時間 年/月/日 時:分
                var nodeContentTime = doc.DocumentNode.SelectSingleNode("//article[@class='ndArticle_leftColumn']/hgroup/div[@class='ndArticle_creat']").InnerText;
                DNContent.Add("內文時間", nodeContentTime);

                //內文內容
                var nodeContentData = doc.DocumentNode.SelectSingleNode("//article[@class='ndArticle_content clearmen']/div/p").InnerText;
                DNContent.Add("新聞內文", nodeContentData);

                return DNContent;
            }
            catch (Exception)
            {
                Console.WriteLine("網址："　+ Link + "可能有無法抓取的因素，請檢查看看");
                throw;
            }

        }

    }
}
