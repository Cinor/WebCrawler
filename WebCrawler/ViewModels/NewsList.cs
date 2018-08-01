using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Models
{
    public class NewsList
    {
        /// <summary>
        /// 日期
        /// </summary>
        public String Days { get; set; }
        /// <summary>
        /// NewDataList
        /// </summary>
        public List<NewsData> GetList { get; set; }

    }
}
