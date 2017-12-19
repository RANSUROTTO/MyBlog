using System.Collections.Generic;

namespace Blog.Presentation.Framework.CommonModel
{

    /// <summary>
    /// 管理员菜单项
    /// </summary>
    public class AdminMenu
    {

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 国际化
        /// </summary>
        public bool I18N { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public IList<AdminMenu> Children { get; set; }

        /// <summary>
        /// 是否包含子节点
        /// </summary>
        public bool AnyChidren
        {
            get { return Children != null && Children.Count > 0; }
        }

    }

}
