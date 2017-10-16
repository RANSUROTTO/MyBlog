namespace Blog.Libraries.Data.Domain.Members.Enum
{
    public enum CustomerLoginPattern : byte
    {

        /// <summary>
        /// 仅可以使用用户名登录
        /// </summary>
        OnlyUseUserName,

        /// <summary>
        /// 仅可以使用用户Email登录
        /// </summary>
        OnlyUseEmail,

        /// <summary>
        /// 可以使用所有账号形式登录
        /// Username And Email
        /// </summary>
        Full

    }
}
