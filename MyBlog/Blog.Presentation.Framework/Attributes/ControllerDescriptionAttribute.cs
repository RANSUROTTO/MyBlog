using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Blog.Presentation.Framework.Attributes
{

    /// <inheritdoc />
    /// <summary>
    /// 代表一个控制器的描述信息特性
    /// </summary>
    public class ControllerDescriptionAttribute : Attribute
    {

        #region Properties

        public List<ControllerDescription> Descriptions { get; }

        #endregion

        #region indexer

        public ControllerDescription this[int index]
        {
            get { return Descriptions[index]; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// args template:
        /// [Name={string},Icon={string},Order={int},I18n={boolean}]
        /// </summary>
        public ControllerDescriptionAttribute(params string[] args)
        {
            Descriptions = new List<ControllerDescription>();
            var regex = new Regex(@"\[Name=(?<name>[\d\D]*),Icon=(?<icon>[\d\D]*)?,Order=(?<order>[\d]*)?,I18n=(?<i18n>true|false)?\]");
            foreach (var item in args)
            {
                int order;
                bool i18N;
                var match = regex.Match(item);
                var name = match.Groups["name"].Value;
                var icon = match.Groups["icon"].Value;
                order = int.TryParse(match.Groups["order"].Value, out order) ? order : 0;
                i18N = bool.TryParse(match.Groups["i18n"].Value, out i18N) && i18N;
                Descriptions.Add(new ControllerDescription
                {
                    Name = name,
                    Icon = icon,
                    Order = order,
                    I18N = i18N
                });
            }
        }

        #endregion

        #region InternalClass

        public class ControllerDescription
        {

            /// <summary>
            /// 顺序
            /// </summary>
            public int Order { get; set; }

            /// <summary>
            /// 图标
            /// </summary>
            public string Icon { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 国际化
            /// </summary>
            public bool I18N { get; set; }

        }

        #endregion

    }

    [TestFixture]
    public class ControllerDescriptionAttributeTest
    {

        [Test]
        public void Passes_ControllerDescription_Constructor_Success()
        {
            var controllerDescription = new ControllerDescriptionAttribute("[Name=test,Icon=fa fa-awit,Order=0,I18n=true]");
            Assert.AreEqual(controllerDescription[0].Name, "test");
            Assert.AreEqual(controllerDescription[0].Icon, "fa fa-awit");
            Assert.AreEqual(controllerDescription[0].Order, 0);
            Assert.AreEqual(controllerDescription[0].I18N, true);
        }

    }

}
