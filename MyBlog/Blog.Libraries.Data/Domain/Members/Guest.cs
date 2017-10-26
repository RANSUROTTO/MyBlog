using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Domain.Members;

namespace Blog.Libraries.Data.Domain.Members
{

    /// <summary>
    /// 代表一个游客
    /// </summary>
    public class Guest : BaseEntity, IGuest
    {

        /// <summary>
        /// 获取或设置用户的代理信息
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 身份验证类型
        /// </summary>
        public AuthenticationType AuthenticatinType => AuthenticationType.Guest;

    }

}
