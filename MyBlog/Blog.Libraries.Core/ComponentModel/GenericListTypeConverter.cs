using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Blog.Libraries.Core.ComponentModel
{

    /// <summary>
    /// 泛型集合类型转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericListTypeConverter<T> : TypeConverter
    {

        protected readonly TypeConverter TypeConverter;

        public GenericListTypeConverter()
        {
            TypeConverter = TypeDescriptor.GetConverter(typeof(T));
            if (TypeConverter == null)
                throw new InvalidOperationException("No type converter exists for type " + typeof(T).FullName);
        }

        /// <summary>
        /// 从逗号分割的字符串获取字符串数组
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>分割后的字符串数组</returns>
        protected virtual string[] GetStringByArray(string input)
        {
            return string.IsNullOrEmpty(input) ? new string[0] : input.Split(',').Select(p => p.Trim()).ToArray();
        }

        /// <summary>
        /// 获取一个值，表示该转换器是否可以
        /// 将给定源类型中的对象转换为转换器的本机类型
        /// 使用上下文
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                string[] items = GetStringByArray(sourceType.ToString());
                return items.Any();
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 将给定对象转换为转换器的本机类型
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                var items = GetStringByArray((string)value);
                var result = new List<T>();
                Array.ForEach(items, p =>
                {
                    object item = TypeConverter.ConvertFromInvariantString(p);
                    if (item != null) result.Add((T)item);
                });
                return result;
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// 使用指定的上下文和参数将给定值对象转换为指定的目标类型
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string result = string.Empty;
                if (value != null)
                {
                    for (int i = 0; i < ((IList<T>)value).Count; i++)
                    {
                        var str = Convert.ToString(((IList<T>)value)[i], CultureInfo.InvariantCulture);
                        result += str;
                        if (i != ((IList<T>)value).Count - 1)
                            result += ",";
                    }
                }
                return result;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }

}
