
namespace Blog.Libraries.Data.Domain.Members
{

    /// <summary>
    /// 用户名称显示方式
    /// </summary>
    public enum CustomerNameFormat : byte
    {
        /// <summary>
        /// 显示邮箱
        /// </summary>
        ShowEmails = 1,

        /// <summary>
        /// 显示用户名
        /// </summary>
        ShowUsernames = 2,

        /// <summary>
        /// 显示全名
        /// </summary>
        ShowFullNames = 3,

        /// <summary>
        /// 显示姓
        /// </summary>
        ShowFirstName = 10

    }

}
