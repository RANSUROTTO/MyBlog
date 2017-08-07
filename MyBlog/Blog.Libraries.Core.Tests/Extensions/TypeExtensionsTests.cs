using Blog.Libraries.Core.Extensions;
using Blog.Tests;
using NUnit.Framework;

namespace Blog.Libraries.Core.Tests.Extensions
{
    [TestFixture]
    public class TypeExtensionsTests
    {

        #region Unit Test IsBasicTypeOrString

        [Test]
        public void Passes_ShortIsBasicType()
        {
            typeof(short).IsBasicTypeOrString().TestBeTrue();
        }

        [Test]
        public void Passes_ByteIsBasicType()
        {
            typeof(byte).IsBasicTypeOrString().TestBeTrue();
        }

        [Test]
        public void Passes_IntIsBasicType()
        {
            typeof(int).IsBasicTypeOrString().TestBeTrue();
        }

        [Test]
        public void Passes_LongIsBasicType()
        {
            typeof(long).IsBasicTypeOrString().TestBeTrue();
        }

        [Test]
        public void Passes_FloatIsBasicType()
        {
            typeof(float).IsBasicTypeOrString().TestBeTrue();
        }

        [Test]
        public void Passes_DoubleIsBasicType()
        {
            typeof(double).IsBasicTypeOrString().TestBeTrue();
        }

        [Test]
        public void Passes_BooleanIsBasicType()
        {
            typeof(bool).IsBasicTypeOrString().TestBeTrue();
        }

        [Test]
        public void Passes_StringIsBasicTypeOfString()
        {
            typeof(string).IsBasicTypeOrString().TestBeTrue();
        }

        #endregion

    }
}
