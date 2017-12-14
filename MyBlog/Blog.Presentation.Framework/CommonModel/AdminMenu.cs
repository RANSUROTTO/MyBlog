using System.Collections.Generic;

namespace Blog.Presentation.Framework.CommonModel
{

    public class AdminMenu
    {

        public string Icon { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public bool I18N { get; set; }

        public IList<AdminMenu> Children { get; set; }

    }

}
