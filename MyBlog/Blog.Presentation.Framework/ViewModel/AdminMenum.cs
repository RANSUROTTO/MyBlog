using System.Collections.Generic;

namespace Blog.Presentation.Framework.ViewModel
{

    public class AdminMenum
    {

        public string Icon { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public bool I18N { get; set; }

        public IList<AdminMenum> Children { get; set; }

    }

}
