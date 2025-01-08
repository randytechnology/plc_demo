using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace plc_demo.Utils
{
    /// <summary>
    /// 文件操作相关类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool CheckFileExist(string relativePath)
        {
            // 获取应用程序的根目录
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 将相对路径合并为绝对路径
            string fullPath = System.IO.Path.Combine(appDirectory, relativePath);

            // 检查文件是否存在
            return File.Exists(fullPath);
        }
    }
}
