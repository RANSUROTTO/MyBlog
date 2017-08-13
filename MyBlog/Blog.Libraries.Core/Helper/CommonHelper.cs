using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

namespace Blog.Libraries.Core.Helper
{

    /// <summary>
    /// 公共助手
    /// </summary>
    public class CommonHelper
    {

        /// <summary>
        /// 确保用户电子邮件或投掷
        /// </summary>
        /// <param name="email">电子邮箱</param>
        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            var output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new Exception("Email is not valid.");
            }

            return output;
        }

        /// <summary>
        /// 验证字符串是否是有效的电子邮件格式
        /// </summary>
        /// <param name="email">电子邮箱</param>
        /// <returns>为有效的电子邮箱返回true,否则返回false</returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.Match(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase | RegexOptions.Compiled).Success;
            return result;
        }

        /// <summary>
        /// 验证字符串是否是有效的IP地址
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>为有效的IP地址返回true,否则返回false</returns>
        public static bool IsValidIpAddress(string ipAddress)
        {
            IPAddress ip;
            return IPAddress.TryParse(ipAddress, out ip);
        }

        /// <summary>
        /// 生成随机数字代码
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var sb = new StringBuilder();

            for (var i = 0; i < length; i++)
                sb.Append(random.Next(10).ToString());

            return sb.ToString();
        }

        /// <summary>
        /// 返回指定范围内的随机整数
        /// </summary>
        /// <param name="min">最小值[包含]</param>
        /// <param name="max">最大值[不包含]</param>
        /// <returns>结果</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// 确保字符串不超过允许的最大长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="postfix">如果原始字符串缩短,则添加到最后的字符串</param>
        /// <returns>
        /// 如果输入字符串小于最大长度,则直接返回
        /// 否则将截断字符串返回
        /// 如果postfix不为空,则截取(最大长度-postfix的长度),再将postfix添加到截取后的字符串末尾
        /// </returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var pLen = postfix?.Length ?? 0;

                var result = str.Substring(0, maxLength - pLen);
                if (!string.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }

            return str;
        }

        /// <summary>
        /// 确保字符串中只包含数值(将非数值[0~9]的字符清除)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EnsureNumericOnly(string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : new string(str.Where(p => char.IsDigit(p)).ToArray());
        }

        /// <summary>
        /// 确保字符串不为null
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>结果</returns>
        public static string EnsureNotNull(string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// 指示指定的字符串组是否存在空值或者空字符串
        /// </summary>
        /// <param name="stringsToValidate">要验证的字符串数组</param>
        /// <returns>结果</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            return stringsToValidate.Any(p => string.IsNullOrEmpty(p));
        }

        /// <summary>
        /// 比较两个数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="a1">数组1</param>
        /// <param name="a2">数组2</param>
        /// <returns>结果</returns>
        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }

            return true;
        }

        private static AspNetHostingPermissionLevel? _trustLevel;
        /// <summary>
        /// 查找正在运行的应用程序的信任级别
        /// </summary>
        /// <returns>信用等级</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                //默认指示不授予任何权限
                _trustLevel = AspNetHostingPermissionLevel.None;

                //确认最高权限
                foreach (AspNetHostingPermissionLevel trustLevel in new[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break; //我们确定了最高的权限
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            return _trustLevel.Value;
        }

        /// <summary>
        /// 为对象的属性赋值
        /// </summary>
        /// <param name="instance">需要赋值属性值的对象</param>
        /// <param name="propertyName">需要赋值的属性的名称</param>
        /// <param name="value">需要赋予的值</param>
        public static void SetProperty(object instance, string propertyName, object value)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            Type instanceType = instance.GetType();
            PropertyInfo pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new Exception(string.Format("No property '{0}' found on the instance of type '{1}'.", propertyName, instanceType));
            if (!pi.CanWrite)
                throw new Exception(string.Format("The property '{0}' on the instance of type '{1}' does not have a setter.", propertyName, instanceType));
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="destinationType">目标类型</param>
        /// <returns>转换后的值</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="value">值</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>转换后的值</returns>
        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="destinationType">目标类型</param>
        /// <param name="culture">Culture</param>
        /// <returns>转换后的值</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                var destinationConverter = TypeDescriptor.GetConverter(destinationType);
                if (destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);

                var sourceConverter = TypeDescriptor.GetConverter(sourceType);
                if (sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);

                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);

                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// 获取时间年差异
        /// </summary>        
        public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            //source: http://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c
            int age = endDate.Year - startDate.Year;
            if (startDate > endDate.AddYears(-age))
                age--;
            return age;
        }

        /// <summary>
        /// 映射到物理磁盘路径的虚拟路径.
        /// </summary>
        /// <param name="path">映射路径. E.g. "~/bin"</param>
        /// <returns>物理路径. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //托管
                return HostingEnvironment.MapPath(path);
            }

            //没有托管 例如,运行在单元测试
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

    }

}
