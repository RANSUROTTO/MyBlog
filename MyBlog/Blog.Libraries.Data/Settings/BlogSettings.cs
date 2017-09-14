using Blog.Libraries.Core.Configuration;

namespace Blog.Libraries.Data.Settings
{

    /// <summary>
    /// 系统对博客的设置
    /// </summary>
    public class BlogSettings : ISettings
    {

        /// <summary>
        /// 获取或设置一个值,指示游客是否可以进行评论
        /// </summary>
        public bool AllowGuestToLeaveComments { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否允许匿名创建博文
        /// </summary>
        public bool AllowAnonymousCreateBlogPost { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否允许匿名进行评论
        /// </summary>
        public bool AllowAnonymousCreateComments { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示博文可有的标签数
        /// </summary>
        public int NumberOfTag { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否显示博文的RSS链接
        /// </summary>
        public bool ShowHeaderRssUrl { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否开启全站评论批准制度
        /// </summary>
        public bool BlogCommentsMustBeApproved { get; set; }

    }

}
