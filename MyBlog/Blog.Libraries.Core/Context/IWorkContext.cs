using Blog.Libraries.Core.Domain.Localization;
using Blog.Libraries.Core.Domain.Members;

namespace Blog.Libraries.Core.Context
{
    public interface IWorkContext
    {

        /// <summary>
        /// 获取工作区的游客
        /// </summary>
        IGuest Guest { get; }

        /// <summary>
        /// 获取工作区的用户
        /// </summary>
        ICustomer Customer { get; }

        /// <summary>
        /// 获取工作区的管理员
        /// </summary>
        IAdmin Admin { get; }

        /// <summary>
        /// 获取或设置工作区语言
        /// </summary>
        Language Language { get; set; }

    }

}
