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


namespace WebCrawler.Models
{
    public class NewsMain
    {
        OrderLibrary orderLibrary = new OrderLibrary();

        public int Page { get; set; }
        
        public void DownloadNews()
        {
            int p = 0;
            while ( p <= Page)
            {
                DownloadNewsData(p);
                p++;
            }
        }
        
        private void DownloadNewsData(int page)
        {
            string link;

            link = "https://tw.appledaily.com/new/realtime/";

            link = link + page.ToString();

            
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(link);

                //項目名稱
                var nodeHead = doc.DocumentNode.SelectNodes("//div[@class='abdominis rlby clearmen']/h1[1]");
                //新聞標題資料
                var nodeData = doc.DocumentNode.SelectNodes("//div[@class='abdominis rlby clearmen']/ul[1]/li");

                NewsViews newsList = new NewsViews();

                List<News> newsData = new List<News>();

                foreach (var nsh in nodeHead)
                {
                    newsList.Days = nsh.InnerText;
                                        
                    foreach (var nsd in nodeData)
                    {
                        News news = new News();
                        var Data = Regex.Split(nsd.InnerText.Replace(" ", "").Replace("\r\n\r\n", ""), "\r\n");
                        //新聞時間 年/月/日 時:分
                        news.Time = Convert.ToDateTime((newsList.Days + " " + Data[0]).ToString());
                       // data.Time = (newsList.Days + " " + Data[0]).ToString();
                        //抓取類型
                        news.Types = Data[1];
                        //抓取網址
                        var newlinks = nsd.SelectSingleNode("./a").Attributes["href"].Value;
                        news.Links = newlinks;
                        //抓取內文
                        news.Content = DownloadNewsContent(newlinks);
                        //抓取標題
                        news.Head = Data[2];

                        newsData.Add(news);

                        Thread.Sleep(1);
                    }
                    //newsList.NewsList = newsData;
                }

                orderLibrary.saveOrderDatads(newsData);

            }
            catch (Exception)
            {

                throw;
            }
                

        }

        private String DownloadNewsContent(String Link)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(Link);

                //新聞標題
                var nodeContentHead = doc.DocumentNode.SelectNodes("//article[@class='ndArticle_leftColumn']/hgroup/h1");
                //新聞內文
                var nodeContentData = doc.DocumentNode.SelectSingleNode("//article[@class='ndArticle_content clearmen']/div/p");

                String nodeContent = nodeContentData.InnerText;
                
                return nodeContent;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
