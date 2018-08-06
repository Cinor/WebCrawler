using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebCrawler.Models;

namespace WebCrawler.ViewModel
{
    public class News
    {
        /// <summary>
        /// NewsDataList
        /// </summary>
        public IPagedList<NewsData> NewsList { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public String Days { get; set; }

        /// <summary>
        /// 類別
        /// </summary>
        public List<SelectListItem> Types_list { get; set; }



    }
}
