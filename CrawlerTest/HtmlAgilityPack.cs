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
    class HtmlAgilityPack
    {


        public void GetNewsTody()
        {
            string link, XPath;

            link = "https://tw.appledaily.com/new/realtime";
            //XPath = "/html[1]/body[1]/div[3]/div[1]/div[5]/div[2]";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(link);

            //項目名稱
            var nodeHead = doc.DocumentNode.SelectNodes("//div[@class='abdominis rlby clearmen']/h1[1]");
            //新聞標題資料
            var nodeData = doc.DocumentNode.SelectNodes("//div[@class='abdominis rlby clearmen']/ul[1]/li");

            foreach (var item in nodeData)
            {
                var Data = Regex.Split(item.InnerText.Replace(" ", "").Replace("\r\n\r\n", ""),"\r\n");
               
                Console.WriteLine(Data);
            }

            Dictionary<string, string> BSData = new Dictionary<string, string>();

            var numbersAndWords = nodeHead.Zip(nodeData, (w, n) => new { Word = w, Number = n });

            foreach (var nw in numbersAndWords)
            {
                BSData.Add(nw.Word.InnerText, nw.Number.InnerText);

            }
            //// 指定來源網頁
            //WebClient url = new WebClient();
            //// 將網頁來源資料暫存到記憶體內
            //MemoryStream ms = new MemoryStream(url.DownloadData(link));

            //// 使用 UTF8 編碼讀入 HTML 
            //HtmlDocument doc = new HtmlDocument();
            //doc.Load(ms, Encoding.UTF8);

            //// 裝載第一層查詢結果 
            //HtmlDocument hdc = new HtmlDocument();

            //// XPath 來解讀它
            //hdc.LoadHtml(doc.DocumentNode.SelectSingleNode(XPath).InnerHtml);

            //HtmlNodeCollection htnode = hdc.DocumentNode.SelectNodes(@"//div[@class='abdominis rlby clearmen']/div/a");

            //foreach (HtmlNode currNode in htnode)
            //{
            //    string currLink = currNode.SelectSingleNode(".").Attributes["href"].Value;
            //    Console.Write(currLink + "<br/>");
            //}
        }

    }
}
