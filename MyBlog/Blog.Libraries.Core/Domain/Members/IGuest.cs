using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Core.Domain.Members
{

    /// <summary>
    /// 代表一个游客
    /// </summary>
    public interface IGuest
    {

        /// <summary>
        /// 获取或设置用户代理信息
        /// </summary>
        string UserAgent { set; get; }

    }

}
