using Blog.Libraries.Core.Extensions;
using Blog.Tests;
using NUnit.Framework;

namespace Blog.Libraries.Core.Tests.Extensions
{
    [TestFixture]
    public class TypeExtensionsTests
    {

        [Test]
        public void Passes_ShortIsBasicType()
        {
            typeof(short).IsCSharpBasicTypeOrOtherBasicType().TestBeTrue();
        }

        [Test]
        public void Passes_ByteIsBasicType()
        {
            typeof(byte).IsCSharpBasicTypeOrOtherBasicType().TestBeTrue();
        }

        [Test]
        public void Passes_IntIsBasicType()
        {
            typeof(int).IsCSharpBasicTypeOrOtherBasicType().TestBeTrue();
        }

        [Test]
        public void Passes_LongIsBasicType()
        {
            typeof(long).IsCSharpBasicTypeOrOtherBasicType().TestBeTrue();
        }

        [Test]
        public void Passes_FloatIsBasicType()
        {
            typeof(float).IsCSharpBasicTypeOrOtherBasicType().TestBeTrue();
        }

        [Test]
        public void Passes_DoubleIsBasicType()
        {
            typeof(double).IsCSharpBasicTypeOrOtherBasicType().TestBeTrue();
        }

        [Test]
        public void Passes_BooleanIsBasicType()
        {
            typeof(bool).IsCSharpBasicTypeOrOtherBasicType().TestBeTrue();
        }

        [Test]
        public void Passes_StringIsBasicTypeOfString()
        {
            typeof(string).IsCSharpBasicTypeOrOtherBasicType().TestBeTrue();
        }

    }
}
