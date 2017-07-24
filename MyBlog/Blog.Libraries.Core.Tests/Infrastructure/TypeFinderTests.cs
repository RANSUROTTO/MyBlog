using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Infrastructure.TypeFinder;
using NUnit.Framework;
using Blog.Tests;
using NUnit.Framework.Internal;

namespace Blog.Libraries.Core.Tests.Infrastructure
{

    public class TypeFinderTests
    {
        [TestFixture]
        public class AppDomainTypeFinderTests
        {

            private AppDomainTypeFinder _finder;

            #region Public Tests

            [SetUp]
            public void SetUp()
            {
                _finder = new AppDomainTypeFinder();
            }

            [TearDown]
            public void TearDown()
            {
                _finder = null;
            }

            #endregion

            /// <summary>
            /// 断言可以正确获取一个类型的派生类集合
            /// </summary>
            [Test]
            public void Passes_FindClassesOfType_OnlyConcreteClassesFalse()
            {
                var types = _finder.FindClassesOfType(typeof(ISomeInterface), false);
                //types.Count().

            }


        }

        public interface ISomeInterface { }

        public interface ISomeChildInterface : ISomeInterface { }

        public class SomeClass<T> : ISomeInterface { }

        public class SomeClass : ISomeInterface { }

        public class SomeChildClass : SomeClass { }

    }
}
