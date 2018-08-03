using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Models
{
    public class NewsData
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public System.Guid ID { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [DisplayName("時間")]
        public DateTime? Time { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        [DisplayName("類型")]
        public String Types { get; set; }
        /// <summary>
        /// 網址
        /// </summary>
        [DisplayName("網址")]
        public String Links { get; set; }
        /// <summary>
        /// 標題
        /// </summary>
        [DisplayName("文章標題")]
        public String Head { get; set; }
        /// <summary>
        /// 內文
        /// </summary>
        [DisplayName("文章內文")]
        public String Content { get; set; }

    }
}
