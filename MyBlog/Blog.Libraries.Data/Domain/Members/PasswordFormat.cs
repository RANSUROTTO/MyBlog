
namespace Blog.Libraries.Data.Domain.Members
{

    /// <summary>
    /// 用户密码保存格式
    /// </summary>
    public enum PasswordFormat : byte
    {

        /// <summary>
        /// 无
        /// </summary>
        Clear,

        /// <summary>
        /// 散列
        /// </summary>
        Hashed,

        /// <summary>
        /// 加密
        /// </summary>
        Encrypted,

    }

}
