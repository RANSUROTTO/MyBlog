using System.Collections.Generic;
using NUnit.Framework;
using System.ComponentModel;
using Blog.Libraries.Core.ComponentModel;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests.ComponentModel
{

    [TestFixture]
    public class GenericListTypeConverterTests
    {

        [SetUp]
        public void SetUp()
        {
            TypeDescriptor.AddAttributes(typeof(List<int>),
                new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));

            TypeDescriptor.AddAttributes(typeof(List<string>),
                new TypeConverterAttribute(typeof(GenericListTypeConverter<string>)));
        }

        [Test]
        public void GetIntListConverter_Equal_GenericListTypeConverter_Int()
        {
            var converter = TypeDescriptor.GetConverter(typeof(List<int>));
            converter.GetType().TestEqual(typeof(GenericListTypeConverter<int>));
        }

        [Test]
        public void GetStringListConverter_Equal_GenericListTypeConvert_String()
        {
            var converter = TypeDescriptor.GetConverter(typeof(List<string>));
            converter.GetType().TestEqual(typeof(GenericListTypeConverter<string>));
        }

        [Test]
        public void GenericListTypeConverter_IntList_To_String()
        {
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var convert = TypeDescriptor.GetConverter(typeof(List<int>));
            var str = convert.ConvertTo(items, typeof(string)) as string;
            str.TestEqual("1,2,3,4,5");
        }

        [Test]
        public void GenericListTypeConverter_StringList_To_String()
        {
            var items = new List<string> { "1", "2", "3", "4", "5" };
            var convert = TypeDescriptor.GetConverter(typeof(List<string>));
            var str = convert.ConvertTo(items, typeof(string)) as string;
            str.TestEqual("1,2,3,4,5");
        }

        [Test]
        public void GenericListTypeConverter_String_To_IntList()
        {
            var str = "1,2,3,4,5";
            var convert = TypeDescriptor.GetConverter(typeof(List<int>));
            var items = convert.ConvertFrom(str) as List<int>;
            items.TestIsNotNull();
            items.Count.TestEqual(5);
        }

        [Test]
        public void GenericListTypeConverter_String_To_StringList()
        {
            var str = "1,2,3,4,5";
            var convert = TypeDescriptor.GetConverter(typeof(List<string>));
            var items = convert.ConvertFrom(str) as List<string>;
            items.TestIsNotNull();
            items.Count.TestEqual(5);
        }

    }

}
