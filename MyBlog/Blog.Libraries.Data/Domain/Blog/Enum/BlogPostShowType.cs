
namespace Blog.Libraries.Data.Domain.Blog.Enum
{

    /// <summary>
    /// 博文的显示方式
    /// </summary>
    public enum BlogPostShowType : byte
    {

        /// <summary>
        /// 公开显示(所有人可见)
        /// </summary>
        Public,

        /// <summary>
        /// 私人显示(仅自己可以查看)
        /// </summary>
        Private

    }

}
