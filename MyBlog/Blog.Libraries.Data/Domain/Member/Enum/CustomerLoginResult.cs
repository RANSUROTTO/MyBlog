namespace Blog.Libraries.Data.Domain.Member.Enum
{
    public enum CustomerLoginResult : byte
    {

        /// <summary>
        /// 验证成功
        /// </summary>
        Successful,

        /// <summary>
        /// 用户不存在
        /// </summary>
        CustomerNotExist,

        /// <summary>
        /// 用户处于锁定中
        /// </summary>
        LockedOut,

        /// <summary>
        /// 密码错误
        /// </summary>
        WrongPassword

    }
}
