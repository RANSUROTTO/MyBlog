using System.Collections.Generic;

namespace Blog.Presentation.Framework.Localization
{

    public interface ILocalizedModel
    {
    }

    public interface ILocalizedModel<T> : ILocalizedModel
    {
        IList<T> Locales { get; set; }
    }

}
