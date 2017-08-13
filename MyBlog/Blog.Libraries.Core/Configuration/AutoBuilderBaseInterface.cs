using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Blog.Libraries.Core.Helper;

namespace Blog.Libraries.Core.Configuration
{

    /// <summary>
    /// 自动构建基础基类
    /// 允许通过字符串来初始化派生类使用特性<seealso cref="AllowAutoBuilderPropertyAttribute"/>的属性的值
    /// </summary>
    public abstract class AutoBuilderBaseInterface
    {

        #region Constructor

        protected AutoBuilderBaseInterface() { }

        /// <summary>
        /// 通过字符串数组来初始化该类型的派生类属性的值
        /// </summary>
        protected AutoBuilderBaseInterface(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                args.ToList().ForEach(p =>
                {
                    if (string.IsNullOrEmpty(p)) return;
                    try
                    {
                        string key = p.Split('=')[0];
                        PropertyInfo property = GetType().GetProperty(key);

                        var attribute = property.GetCustomAttribute<AllowAutoBuilderPropertyAttribute>();

                        if (attribute != null
                            && (attribute.Target & AutoBuilderAttributeTargets.AllowRead) != 0)
                        {
                            dynamic value = Convert.ChangeType(p.Split('=')[1], property.PropertyType);
                            property.SetValue(this, value);
                        }

                    }
                    catch
                    {
                        // ignored
                        // 抛出异常是一个耗性能的操作,不建议在循环内捕获异常
                        // 该处循环内使用try-catch是因为当该属性赋值失败时,不会影响剩下属性的赋值操作
                    }
                });
            }
        }

        /// <summary>
        /// 通过字符串来初始化该类型的派生类属性的值
        /// </summary>
        protected AutoBuilderBaseInterface(string args) : this(args.Split(';')) { }

        #endregion

    }

    /// <summary>
    /// 自动构建基础基类扩展
    /// <seealso cref="AutoBuilderBaseInterface"/>
    /// </summary>
    public static class AutoBuilderBaseInterfaceExtensions
    {

        #region Extensions

        /// <summary>
        /// 写出构建类初始化属性字符串
        /// </summary>
        public static string GetSettings<T>(this T t) where T : AutoBuilderBaseInterface
        {
            StringBuilder sb = new StringBuilder();

            var properties = t.GetType().GetProperties()
                .Where(p =>
                {
                    var attribute = p.GetCustomAttribute<AllowAutoBuilderPropertyAttribute>();
                    return attribute != null
                        && (attribute.Target & AutoBuilderAttributeTargets.AllowWrite) != 0;
                }).ToList();

            for (int i = 0; i < properties.Count; i++)
            {
                string key = properties[i].Name;
                string value = properties[i].GetValue(t)?.ToString();
                string temp = string.Format("{0}={1}", key, value);
                if (i > 0) temp = ";" + temp;
                sb.Append(temp);
            }

            return sb.ToString();
        }

        #endregion

    }

    /// <summary>
    /// 允许一个属性的所有者 继承类<seealso cref="AutoBuilderBaseInterface"/> 使用字符串初始化值
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowAutoBuilderPropertyAttribute : Attribute
    {

        #region Properties

        public AutoBuilderAttributeTargets Target { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public AllowAutoBuilderPropertyAttribute(AutoBuilderAttributeTargets taget = AutoBuilderAttributeTargets.AllowAll)
        {
            Target = taget;
        }

        #endregion

    }

    /// <summary>
    /// 设置使用特性<seealso cref="AllowAutoBuilderPropertyAttribute"/>的属性允许哪种操作
    /// 该枚举使用Flags特性,应主动为枚举值赋标识数值(2的幂)
    /// 参考:https://stackoverflow.com/questions/8447/what-does-the-flags-enum-attribute-mean-in-c
    /// </summary>
    [Flags]
    [Serializable]
    [ComVisible(true)]
    public enum AutoBuilderAttributeTargets
    {
        /// <summary>
        /// 允许读取初始化属性
        /// </summary>
        AllowRead = 1,
        /// <summary>
        /// 允许写出属性字符串
        /// </summary>
        AllowWrite = 2,
        /// <summary>
        /// 全部允许
        /// </summary>
        AllowAll = AllowRead | AllowWrite
    }

}

