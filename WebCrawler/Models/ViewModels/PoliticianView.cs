using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCrawler.Models.ViewModels
{
    public class PoliticianView
    {
        /// <summary>
        /// 利害關係人名單
        /// </summary>
        public List<Politician> stakeholdersList { get; set; }


    }

    public class Politician
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public System.Guid Id { get; set; }

        /// <summary>
        /// 利害關係人姓名
        /// </summary>
        [DisplayName("姓名")]
        public String Name { get; set; }

        /// <summary>
        /// 出現次數
        /// </summary>
        [DisplayName("出現次數")]
        public int Amount { get; set; }
    }
}