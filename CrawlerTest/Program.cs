using CrawlerTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Program
    {
        static void Main(string[] args)
        {
            //NewsMain a = new NewsMain();
            //a.Page = 10;
            //a.GetNews();

            //測試抓取內文
            //var Link = "https://tw.news.appledaily.com/international/realtime/20180906/1424770/ ";
            //a.GetNewsContent(Link);


            FreeNews free = new FreeNews();
            free.Page = 10;
            free.DownloadNews();

        }
    }
}
