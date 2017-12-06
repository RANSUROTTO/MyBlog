using System;

namespace Blog.Presentation.Framework.Attributes
{

    /// <inheritdoc />
    /// <summary>
    /// 代表一个控制器的描述信息特性
    /// </summary>
    public class ControllerDescriptionAttribute : Attribute
    {

        public ControllerDescriptionAttribute(params string[] args)
        {
        }

        [Serializable]
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

    }

}
