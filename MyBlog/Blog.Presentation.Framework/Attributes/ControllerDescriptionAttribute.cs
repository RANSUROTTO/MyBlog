using System;

namespace Blog.Presentation.Framework.Attributes
{

    /// <inheritdoc />
    /// <summary>
    /// ����һ����������������Ϣ����
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
            /// ˳��
            /// </summary>
            public int Order { get; set; }

            /// <summary>
            /// ͼ��
            /// </summary>
            public string Icon { get; set; }

            /// <summary>
            /// ����
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// ���ʻ�
            /// </summary>
            public bool I18N { get; set; }

        }

    }

}
