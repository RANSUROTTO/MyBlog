using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Libraries.Core.ComponentModel
{
    public class GenericeDictionaryTypeConverter<K, V> : TypeConverter
    {

        protected readonly TypeConverter typeConverterKey;
        protected readonly TypeConverter typeConverterValue;

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public GenericeDictionaryTypeConverter()
        {
            typeConverterKey = TypeDescriptor.GetConverter(typeof(K));
            if (typeConverterKey == null)
                throw new InvalidOperationException(("No type converter exists for type " + typeof(K).FullName));

            typeConverterValue = TypeDescriptor.GetConverter(typeof(V));
            if (typeConverterValue == null)
                throw new InvalidOperationException(("No type converter exists for type " + typeof(V).FullName));
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取一个值，表示该转换器是否可以
        /// 将给定源类型中的对象转换为转换器的类型
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 将给定对象转换为转换器的类型
        /// 目前实现的例子:
        /// string:"1,2;3,4" => dictionary:{{1,2},{3,4}}
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var input = value as string;
            if (input != null)
            {
                string[] items = string.IsNullOrEmpty(input)
                    ? new string[0]
                    : input.Split(';').Select(p => p.Trim()).ToArray();

                var result = new Dictionary<K, V>();
                Array.ForEach(items, p =>
                {
                    string[] keyValueStr = string.IsNullOrEmpty(p)
                        ? new string[0]
                        : p.Split(',').Select(x => x.Trim()).ToArray();

                    if (keyValueStr.Length == 2)
                    {
                        object dictionaryKey = (K)typeConverterKey.ConvertFromInvariantString(keyValueStr[0]);
                        object dictionaryValue = (V)typeConverterValue.ConvertFromInvariantString(keyValueStr[1]);
                        if (dictionaryKey != null && dictionaryValue != null)
                        {
                            if (!result.ContainsKey((K)dictionaryKey))
                                result.Add((K)dictionaryKey, (V)dictionaryValue);
                        }
                    }
                });
                return result;
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// 将给定对象转换为转换器的类型
        /// 目前实现的例子:
        /// dictionary:{{1,2},{3,4}} => string:"1,2;3,4" 
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion

    }

}
