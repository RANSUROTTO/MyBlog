using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Helper;
using Blog.Tests;
using NUnit.Framework;

namespace Blog.Libraries.Core.Tests.Helper
{

    [TestFixture]
    public class CommonHelperTests
    {

        [Test]
        public void Passes_ValidateEmail_Success()
        {
            CommonHelper.IsValidEmail("admin@site.com").TestBeTrue();
            CommonHelper.IsValidEmail("MyIsNotEmail").TestBeFalse();
        }

        [Test]
        public void Passes_ValidateIpAddress_Success()
        {
            CommonHelper.IsValidIpAddress("127.0.0.1").TestBeTrue();
            CommonHelper.IsValidIpAddress("92.48+4.4").TestBeFalse();
        }

        [Test]
        public void Passes_GenerateRandomDigitCode_Success()
        {
            CommonHelper.GenerateRandomDigitCode(4).Length.TestEqual(4);
        }

        [Test]
        public void Passess_GenerateRandomInteger_Success()
        {
            int number = CommonHelper.GenerateRandomInteger();
            (number >= 0).TestBeTrue();
            (number < int.MaxValue).TestBeTrue();
        }

        [Test]
        public void Passess_EnsureNumericOnly_Success()
        {
            string str = "123456a789";
            string numberStr = CommonHelper.EnsureNumericOnly(str);
            numberStr.TestEqual("123456789");
        }

        [Test]
        public void Passess_EnsureStringIsNotNull_Success()
        {
            string str = null;
            CommonHelper.EnsureNotNull(str).TestEqual(string.Empty);
        }



    }

}
