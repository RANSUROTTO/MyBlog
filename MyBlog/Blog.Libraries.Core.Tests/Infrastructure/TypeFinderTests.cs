using System.Linq;
using Blog.Libraries.Core.Infrastructure.TypeFinder;
using NUnit.Framework;
using Blog.Tests;

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

            #region Tests FindClassesOfType

            /// <summary>
            /// 断言可以正确获取一个类型的非具体派生类集合
            /// </summary>
            [Test]
            public void Passes_FindClassesOfType_OnlyConcreteClassesFalse()
            {
                var types = _finder.FindClassesOfType(typeof(ISomeInterface), false);
                types.Count().TestEqual(4);
                types.Contains(typeof(AbstractSomeClass)).TestBeTrue();
                types.Contains(typeof(SomeClass<>)).TestBeTrue();
                types.Contains(typeof(SomeClass)).TestBeTrue();
                types.Contains(typeof(SomeChildClass)).TestBeTrue();
            }

            /// <summary>
            /// 断言可以正确获取一个类型的具体派生类集合
            /// </summary>
            [Test]
            public void Passes_FindClassesOfType_OnlyConcreteClassesTrue()
            {
                var types = _finder.FindClassesOfType(typeof(ISomeInterface), true);
                types.Count().TestEqual(3);
                types.Contains(typeof(AbstractSomeClass)).TestBeFalse();
                types.Contains(typeof(SomeClass<>)).TestBeTrue();
                types.Contains(typeof(SomeClass)).TestBeTrue();
                types.Contains(typeof(SomeChildClass)).TestBeTrue();
            }

            #endregion

            #region Tests FindClassesOfType Generice

            /// <summary>
            /// 断言可以正确获取一个类型的非具体派生类集合
            /// </summary>
            [Test]
            public void Passes_FindClassesOfType_OnlyConcreteClassesFalse_Generice()
            {
                var types = _finder.FindClassesOfType<ISomeInterface>(false);
                types.Count().TestEqual(4);
                types.Contains(typeof(AbstractSomeClass)).TestBeTrue();
                types.Contains(typeof(SomeClass<>)).TestBeTrue();
                types.Contains(typeof(SomeClass)).TestBeTrue();
                types.Contains(typeof(SomeChildClass)).TestBeTrue();
            }

            /// <summary>
            /// 断言可以正确获取一个类型的非具体派生类集合
            /// </summary>
            [Test]
            public void Passes_FindClassesOfType_OnlyConcreteClassesTrue_Generice()
            {
                var types = _finder.FindClassesOfType<ISomeInterface>(true);
                types.Count().TestEqual(3);
                types.Contains(typeof(AbstractSomeClass)).TestBeFalse();
                types.Contains(typeof(SomeClass<>)).TestBeTrue();
                types.Contains(typeof(SomeClass)).TestBeTrue();
                types.Contains(typeof(SomeChildClass)).TestBeTrue();
            }

            #endregion

        }

        [TestFixture]
        public class WebAppTypeFinderTests
        {

            private WebAppTypeFinder _finder;

            #region Public Tests

            [SetUp]
            public void SetUp()
            {
                _finder = new WebAppTypeFinder();
            }

            [TearDown]
            public void TearDown()
            {
                _finder = null;
            }

            #endregion

            #region Tests GetBinDirectory

            [Test]
            public void Passes_GetBinDirectory_NotEqualNull()
            {
                string directoryPath = _finder.GetBinDirectory();
                directoryPath.TestIsNotNull("directoryPath is null");
            }

            #endregion

        }


        public interface ISomeInterface { }

        public interface ISomeChildInterface : ISomeInterface { }

        public abstract class AbstractSomeClass : ISomeInterface { }

        public class SomeClass<T> : ISomeInterface { }

        public class SomeClass : ISomeInterface { }

        public class SomeChildClass : SomeClass { }

    }
}
