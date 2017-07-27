using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests
{
    [TestFixture]
    public class AutoBuilderBaseInterfaceTests
    {

        [Test]
        public void PassesAutoBuilderAttributeTargets_AllowAll_Contain_AllowReadAndAllowWrite()
        {
            AutoBuilderAttributeTargets enum1 = AutoBuilderAttributeTargets.AllowAll;

            ((enum1 & AutoBuilderAttributeTargets.AllowRead) != 0).TestBeTrue();
            ((enum1 & AutoBuilderAttributeTargets.AllowWrite) != 0).TestBeTrue();

            (enum1.HasFlag(AutoBuilderAttributeTargets.AllowRead)).TestBeTrue();
            (enum1.HasFlag(AutoBuilderAttributeTargets.AllowWrite)).TestBeTrue();

        }

        [Test]
        public void PassesAutoBuilderInterface_Builder_AllowRead()
        {
            string str = "Id=1;AppKey=abcdefg;Md5Key=e12306";
            AutoBuilderBaseModel model = new AutoBuilderBaseModel(str);
            model.Id.TestEqual(0);
            model.AppKey.TestEqual("abcdefg");
            model.Md5Key.TestEqual("e12306");
        }

        [Test]
        public void PassesAutoBuilderInterface_GetSettings_AllowWrite()
        {
            AutoBuilderBaseModel model = new AutoBuilderBaseModel
            {
                Id = 1,
                AppKey = "abcdefg",
                Md5Key = "e12306"
            };
            string str = model.GetSettings();

            str.Contains("Md5Key").TestBeFalse();
            str.Split(';').ToList().ForEach(p =>
            {
                string key = p.Split('=')[0];
                if (key == "Id")
                    p.Split('=')[1].TestEqual("1");

                if (key == "AppKey")
                    p.Split('=')[1].TestEqual("abcdefg");
            });
        }

        [Serializable]
        public class AutoBuilderBaseModel : AutoBuilderBaseInterface
        {

            public AutoBuilderBaseModel() { }

            public AutoBuilderBaseModel(string args) : base(args) { }

            public AutoBuilderBaseModel(string[] args) : base(args) { }

            [AllowAutoBuilderProperty(AutoBuilderAttributeTargets.AllowWrite)]
            public long Id { get; set; }

            [AllowAutoBuilderProperty]
            public string AppKey { get; set; }

            [AllowAutoBuilderProperty(AutoBuilderAttributeTargets.AllowRead)]
            public string Md5Key { get; set; }

        }

    }
}
