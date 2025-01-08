using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace plc_demo.Utils
{
    /// <summary>
    /// 文本帮助类
    /// </summary>
    public static class TextHelper
    {
        //常见姓氏有这80个
        public static char[] OftenLastName = new char[80] {
            '李','王','张','刘','陈','杨','黄','赵','吴','周',
            '徐','孙','马','朱','胡','郭','何','高','林','罗',
            '郑','梁','谢','宋','唐','许','韩','冯','邓','曹',
            '彭','曾','萧','田','董','袁','潘','于','蒋','蔡',
            '余','杜','叶','程','苏','魏','吕','丁','任','沈',
            '姚','卢','姜','崔','钟','谭','陆','汪','范','金',
            '石','廖','贾','夏','韦','傅','方','白','邹','孟',
            '熊','秦','邱','江','尹','薛','阎','段','雷','侯'
        };

        /// <summary>
        /// 随机获取一个中文名
        /// </summary>
        /// <returns></returns>
        public static string RandomChineseName()
        {
            //基本汉字在unicode编码中位于19968 - 40869
            int ChineseWordsNumber = 40869 - 19968 + 1;
            int FirstChineseWord = 19968;

            string randName = "";
            //使用随机数在中文随机选取文字
            Random rd = new Random();
            //产生随机数 代表 单姓或复姓 下标(共有444+60=504种姓氏)
            int rdnumber = rd.Next(80);
            //判断 单姓还是复姓
            randName += OftenLastName[rdnumber];
            //添加第一个字
            rdnumber = rd.Next(ChineseWordsNumber) + FirstChineseWord;
            randName += (char)rdnumber;
            //添加第二个字
            rdnumber = rd.Next(ChineseWordsNumber) + FirstChineseWord;
            randName += (char)rdnumber;
            //显示名字
            return randName;
        }

    }

    
}
