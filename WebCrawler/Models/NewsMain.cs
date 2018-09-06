using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading;
using WebCrawler.Models.ViewModel;
using WebCrawler.Library;
using PagedList;
using System.Diagnostics;

namespace WebCrawler.Models
{
    public class NewsMain
    {
        OrderLibrary orderLibrary = new OrderLibrary();

        public int Page { get; set; }

        public void DownloadNews()
        {
            Stopwatch stopwatch = new Stopwatch();
            int p = 0;
            while (p <= Page)
            {
                stopwatch.Start();

                DownloadNewsData(p);

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);

                p++;
            }
        }

        private void DownloadNewsData(int page)
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
                    News news = new News();

                    //建立key值
                    news.Id = Guid.NewGuid();

                    //抓取類型
                    news.Types = nsd.SelectSingleNode("./a/h2").InnerText;

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

                orderLibrary.saveNewsData(newsData);

            }
            catch (System.IndexOutOfRangeException e)
            {
                System.Console.WriteLine(e.Message);
                throw new System.ArgumentOutOfRangeException("index parameter is out of range.", e);
            }


        }

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

                throw;
            }

        }

    }
}
