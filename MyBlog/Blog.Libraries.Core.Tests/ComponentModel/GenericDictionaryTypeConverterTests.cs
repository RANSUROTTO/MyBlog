using System.Collections.Generic;
using System.ComponentModel;
using Blog.Libraries.Core.ComponentModel;
using Blog.Tests;
using NUnit.Framework;

namespace Blog.Libraries.Core.Tests.ComponentModel
{

    [TestFixture]
    public class GenericDictionaryTypeConverterTests
    {

        [SetUp]
        public void SetUp()
        {
            TypeDescriptor.AddAttributes(typeof(Dictionary<string, string>)
                , new TypeConverterAttribute(typeof(GenericDictionaryTypeConverter<string, string>)));
            TypeDescriptor.AddAttributes(typeof(Dictionary<int, string>)
                , new TypeConverterAttribute(typeof(GenericDictionaryTypeConverter<int, string>)));
        }

        [Test]
        public void Passes_DictionaryConverter_Equal_GenericeDictionaryTypeConverterStringKeyStringValue()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Dictionary<string, string>));
            converter.GetType().TestEqual(typeof(GenericDictionaryTypeConverter<string, string>));
        }

        [Test]
        public void Passes_DictionaryConverter_Euqal_GenericeDictionaryTypeConverterIntKeyStringValue()
        {
            var conveter = TypeDescriptor.GetConverter(typeof(Dictionary<int, string>));
            conveter.GetType().TestEqual(typeof(GenericDictionaryTypeConverter<int, string>));
        }

        [Test]
        public void Passes_DictionaryToStringConvert_Success()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Dictionary<int, string>));
            var dictionary = new Dictionary<int, string> { { 1, "value1" }, { 2, "value2" }, { 3, "value3" } };
            var str = converter.ConvertTo(dictionary, typeof(string));
            str.TestEqual("1,value1;2,value2;3,value3");
        }

        [Test]
        public void Passes_StringToDictionaryConvert_Success()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Dictionary<int, string>));
            var str = "1,value1;2,value2;3,value3";
            var dictionary = (Dictionary<int, string>)converter.ConvertFrom(str);
            dictionary?[1].TestEqual("value1");
            dictionary?[2].TestEqual("value2");
            dictionary?[3].TestEqual("value3");
        }

    }

}
