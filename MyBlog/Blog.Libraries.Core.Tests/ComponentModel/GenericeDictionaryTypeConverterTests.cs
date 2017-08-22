using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.ComponentModel;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests.ComponentModel
{

    public class GenericeDictionaryTypeConverterTests
    {

        public void SetUp()
        {
            TypeDescriptor.AddAttributes(typeof(Dictionary<string, string>)
                , new TypeConverterAttribute(typeof(GenericeDictionaryTypeConverter<string, string>)));
            TypeDescriptor.AddAttributes(typeof(Dictionary<int, string>)
                , new TypeConverterAttribute(typeof(GenericeDictionaryTypeConverter<int, string>)));
        }

        public void Passes_DictionaryConverter_Equal_GenericeDictionaryTypeConverterStringKeyStringValue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Dictionary<string, string>));
            converter.GetType().TestEqual(typeof(GenericeDictionaryTypeConverter<string, string>));
        }


    }

}
