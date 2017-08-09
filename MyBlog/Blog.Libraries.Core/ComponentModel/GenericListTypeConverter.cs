using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Libraries.Core.ComponentModel
{

    /// <summary>
    /// 泛型集合类型转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericListTypeConverter<T> : TypeConverter
    {

        protected readonly TypeConverter typeConverter;

        public GenericListTypeConverter()
        {
            typeConverter = TypeDescriptor.GetConverter(typeof(T));
            if (typeConverter == null)
                throw new InvalidOperationException("No type converter exists for type " + typeof(T).FullName);
        }




    }

}
