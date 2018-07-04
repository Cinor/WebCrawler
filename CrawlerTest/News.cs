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

namespace CrawlerTest
{
    class News
    {


        public void GetNewsTody()
        {
            string link;

            link = "https://tw.appledaily.com/new/realtime";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(link);

            //項目名稱
            var nodeHead = doc.DocumentNode.SelectNodes("//div[@class='abdominis rlby clearmen']/h1[1]");
            //新聞標題資料
            var nodeData = doc.DocumentNode.SelectNodes("//div[@class='abdominis rlby clearmen']/ul[1]/li");

            NewsList newsList = new NewsList();

            foreach (var nsh in nodeHead)
            {
                newsList.Days = nsh.InnerText;

                List<NewsData> newsData = new List<NewsData>();
                foreach (var nsd in nodeData)
                {
                    NewsData data = new NewsData();
                    var Data = Regex.Split(nsd.InnerText.Replace(" ", "").Replace("\r\n\r\n", ""), "\r\n");
                    //抓取時間:時分
                    data.Time = Data[0];
                    //抓取類型
                    data.Types = Data[1];
                    //抓取網址
                    data.Link = nsd.SelectSingleNode("./a").Attributes["href"].Value;
                    //抓取標題
                    data.Head = Data[2];

                    newsData.Add(data);
                }
                newsList.GetList = newsData;

            }

        }

    }
}
