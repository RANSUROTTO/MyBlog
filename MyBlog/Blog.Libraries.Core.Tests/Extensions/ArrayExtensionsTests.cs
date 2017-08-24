using System.Collections.Generic;
using NUnit.Framework;
using Blog.Libraries.Core.Extensions;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests.Extensions
{

    [TestFixture]
    public class ArrayExtensionsTests
    {

        [Test]
        public void Passes_IEnumberableJoin_Success()
        {
            List<string> array = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            };
            string str = array.Join();
            str.TestEqual("1,2,3,4,5");
            string str2 = array.Join("-");
            str2.TestEqual("1-2-3-4-5");
        }

    }

}
