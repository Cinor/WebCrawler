﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Models;

namespace WebCrawler.ViewModel
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
