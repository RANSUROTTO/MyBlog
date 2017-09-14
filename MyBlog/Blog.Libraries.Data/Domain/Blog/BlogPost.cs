using System;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Blog.Enum;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Domain.Blog
{

    /// <summary>
    /// 代表一个博文
    /// </summary>
    public class BlogPost : BaseEntity
    {

        /// <summary>
        /// 获取或设置博文标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置博文内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 获取或设置博文的概述
        /// 如果不为空,展示时则不显示博文内容,而是显示概述
        /// </summary>
        public string BodyOverview { get; set; }

        /// <summary>
        /// 获取或设置博文的浏览数
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否允许评论
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否匿名发布
        /// </summary>
        public bool Anonymous { get; set; }

        /// <summary>
        /// 获取或设置创建博文时的ip地址
        /// </summary>
        public string CreateAtIp { get; set; }

        /// <summary>
        /// 获取或设置文章最后一次编辑时间
        /// </summary>
        public DateTime? LastUpdateAt { get; set; }

        /// <summary>
        /// 获取或设置最后一次编辑博文时的ip地址
        /// </summary>
        public string LastUpdateAtIp { get; set; }

        /// <summary>
        /// 获取或设置一个值,设置文章在指定时间后可被浏览
        /// 为null时,则不限制显示开始时间
        /// </summary>
        public DateTime? ShowStartAt { get; set; }

        /// <summary>
        /// 获取或设置一个值,设置文章在指定时间前可被浏览
        /// 为null时,则不限制显示结束时间
        /// </summary>
        public DateTime? ShowEndAt { get; set; }

        /// <summary>
        /// 获取或设置博文的显示方式
        /// </summary>
        public BlogPostShowType ShowType { get; set; }

        /// <summary>
        /// 获取或设置文章作者
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// 获取或设置文章的分类
        /// </summary>
        public virtual BlogCategorie BlogCategorie { get; set; }

    }
}
