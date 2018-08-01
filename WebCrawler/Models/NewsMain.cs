﻿using System;
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

namespace WebCrawler.Models
{
    class NewsMain
    {
        public int Page { get; set; }
        private List<NewsList> NewsDataList = new List<NewsList>();
        
        public void GetNews()
        {
            int p = 0;
            while ( p <= Page)
            {
                GetNewsLink(p);
                p++;
            }
        }
        
        private void GetNewsLink(int page)
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

                NewsList newsList = new NewsList();

                foreach (var nsh in nodeHead)
                {
                    newsList.Days = nsh.InnerText;

                    List<NewsData> newsData = new List<NewsData>();
                    foreach (var nsd in nodeData)
                    {
                        NewsData data = new NewsData();
                        var Data = Regex.Split(nsd.InnerText.Replace(" ", "").Replace("\r\n\r\n", ""), "\r\n");
                        //新聞時間 年/月/日 時:分
                        data.Time = Convert.ToDateTime((newsList.Days + " " + Data[0]).ToString());
                       // data.Time = (newsList.Days + " " + Data[0]).ToString();
                        //抓取類型
                        data.Types = Data[1];
                        //抓取網址
                        var newlinks = nsd.SelectSingleNode("./a").Attributes["href"].Value;
                        data.Links = newlinks;
                        //抓取內文
                        data.Content = GetNewsContent(newlinks);
                        //抓取標題
                        data.Head = Data[2];

                        newsData.Add(data);

                        Thread.Sleep(1);
                    }
                    newsList.GetList = newsData;
                    NewsDataList.Add(newsList);
                }
                

            }
            catch (Exception)
            {

                throw;
            }
                

        }

        private String GetNewsContent(String Link)
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