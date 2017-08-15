using System;
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

        [Test]
        public void Passess_AreNullOrEmptyByArray_Success()
        {
            string[] strs = { "a", "b", "", "c" };
            CommonHelper.AreNullOrEmpty(strs).TestBeTrue();
            strs[2] = "d";
            CommonHelper.AreNullOrEmpty(strs).TestBeFalse();
        }

        [Test]
        public void Passes_ArrayEqual_Success()
        {
            var array1 = new[] { 1, 2, 3, 4, 5 };
            var array2 = new[] { 1, 2, 3, 4, 5 };
            CommonHelper.ArraysEqual(array1, array2).TestBeTrue();
        }

        [Test]
        public void Passess_SetProperty_Success()
        {
            var model = new SetPropertyUnitTestModel();
            CommonHelper.SetProperty(model, "Name", "lancelot");
            model.Name.TestEqual("lancelot");
        }

        [Test]
        public void Passes_To_Convert_Success()
        {
            int i = 10;
            string j = CommonHelper.To<string>(i);
            j.TestEqual("10");
        }

        [Test]
        public void Passes_GetDifferenceInYears_Success()
        {
            DateTime date1 = new DateTime(2012, 11, 2);
            DateTime date2 = new DateTime(2017, 2, 3);
            CommonHelper.GetDifferenceInYears(date1, date2).TestEqual(4);
        }

        public class SetPropertyUnitTestModel
        {
            public string Name { get; set; }
        }

    }

}
