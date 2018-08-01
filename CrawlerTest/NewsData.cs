using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class NewsData
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        public String Types { get; set; }
        /// <summary>
        /// 網址
        /// </summary>
        public String Links { get; set; }
        /// <summary>
        /// 標題
        /// </summary>
        public String Head { get; set; }
        /// <summary>
        /// 內文
        /// </summary>
        public String Content { get; set; }

    }
}
