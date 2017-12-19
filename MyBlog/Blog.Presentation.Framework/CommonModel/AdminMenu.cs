using System.Collections.Generic;

namespace Blog.Presentation.Framework.CommonModel
{

    /// <summary>
    /// ����Ա�˵���
    /// </summary>
    public class AdminMenu
    {

        /// <summary>
        /// ͼ��
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// ���ʻ�
        /// </summary>
        public bool I18N { get; set; }

        /// <summary>
        /// �ӽڵ�
        /// </summary>
        public IList<AdminMenu> Children { get; set; }

        /// <summary>
        /// �Ƿ�����ӽڵ�
        /// </summary>
        public bool AnyChidren
        {
            get { return Children != null && Children.Count > 0; }
        }

    }

}
