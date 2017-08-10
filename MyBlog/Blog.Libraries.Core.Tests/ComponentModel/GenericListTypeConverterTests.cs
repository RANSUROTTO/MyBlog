using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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





    }

}
