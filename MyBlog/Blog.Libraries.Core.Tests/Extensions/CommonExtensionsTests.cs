using Blog.Tests;
using Blog.Libraries.Core.Extensions;
using NUnit.Framework;

namespace Blog.Libraries.Core.Tests.Extensions
{

    [TestFixture]
    public class CommonExtensionsTests
    {

        [Test]
        public void Passes_Object_IsNullOrDefault_Success()
        {
            int? temp1 = null;
            temp1.IsNullOrDefault().TestBeTrue();
            int? temp2 = default(int?);
            temp2.IsNullOrDefault().TestBeTrue();
            int? temp3 = 1;
            temp3.IsNullOrDefault().TestBeFalse();
        }

    }

}
