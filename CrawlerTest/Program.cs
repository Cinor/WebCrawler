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
            NewsMain a = new NewsMain();
            a.Page = 10;
            a.GetNews();

        }
    }
}
