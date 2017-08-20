using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Blog.Libraries.Core.Helper
{

    /// <summary>
    /// xml帮助类
    /// </summary>
    public class XmlHelper
    {

        /// <summary>
        /// xml编码
        /// </summary>
        public static string XmlEncode(string str)
        {
            if (str == null)
                return null;
            str = Regex.Replace(str, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "", RegexOptions.Compiled);
            return XmlEncodeAsIs(str);
        }

        /// <summary>
        /// XML按原样编码
        /// </summary>
        public static string XmlEncodeAsIs(string str)
        {
            if (str == null)
                return null;
            using (var sw = new StringWriter())
            using (var xwr = new XmlTextWriter(sw))
            {
                xwr.WriteString(str);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 编码特效
        /// </summary>
        public static string XmlEncodeAttribute(string str)
        {
            if (str == null)
                return null;
            str = Regex.Replace(str, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "", RegexOptions.Compiled);
            return XmlEncodeAttributeAsIs(str);
        }

        /// <summary>
        /// 根据原样编码特效
        /// </summary>
        public static string XmlEncodeAttributeAsIs(string str)
        {
            return XmlEncodeAsIs(str).Replace("\"", "&quot;");
        }

        /// <summary>
        /// 解码一个特性
        /// </summary>
        public static string XmlDecode(string str)
        {
            var sb = new StringBuilder(str);
            return sb.Replace("&quot;", "\"").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").ToString();
        }

        /// <summary>
        /// 序列化日期时间
        /// </summary>
        public static string SerializeDateTime(DateTime dateTime)
        {
            var xmlS = new XmlSerializer(typeof(DateTime));
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                xmlS.Serialize(sw, dateTime);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 反序列化日期时间
        /// </summary>
        public static DateTime DeserializeDateTime(string dateTime)
        {
            var xmlS = new XmlSerializer(typeof(DateTime));
            using (var sr = new StringReader(dateTime))
            {
                object test = xmlS.Deserialize(sr);
                return (DateTime)test;
            }
        }

    }

}
