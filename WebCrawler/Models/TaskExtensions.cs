using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebCrawler.Models
{
    /// <summary>
    /// Task 擴充方法
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Task 拋出例外時立刻結束程式。
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static Task FailFastOnException(this Task task)
        {
            task.ContinueWith(c => Environment.FailFast("Task faulted", c.Exception),
                TaskContinuationOptions.OnlyOnFaulted); // 例外發生時才執行。
            return task;
        }

        /// <summary>
        /// Task 拋出例外時只會印出例外訊息。
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static Task IgnoreExceptions(this Task task)
        {
            task.ContinueWith(c => Console.WriteLine(c.Exception),
                TaskContinuationOptions.OnlyOnFaulted); // 例外發生時才執行。
            return task;
        }
    }
}