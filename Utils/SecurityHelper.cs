using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace plc_demo.Utils
{
    /// <summary>
    /// 安全操作类
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// 返回32位MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5(string input)
        {
            if (string.IsNullOrEmpty(input)) { return ""; }
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // 转换成32位的十六进制字符串
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 返回16位MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5Short(string input)
        {
            string md5Full = GetMD5(input);
            return md5Full.Substring(8, 16);  // 截取32位MD5中的中间16位
        }
    }
}
