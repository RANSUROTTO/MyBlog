namespace Blog.Libraries.Core.Domain.Members
{

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum AuthenticationType : byte
    {

        /// <summary>
        /// 游客
        /// </summary>
        Guest,

        /// <summary>
        /// 用户
        /// </summary>
        Customer,

        /// <summary>
        /// 管理员
        /// </summary>
        Admin,

    }

}
