using System;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Domain.Member
{

    /// <summary>
    /// 代表一个用户的概况资料
    /// </summary>
    public class CustomerProfile : BaseEntity
    {

        /// <summary>
        /// 获取或设置用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置用户性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 获取或设置用户出生时间
        /// </summary>
        public DateTime BirthAt { get; set; }

        /// <summary>
        /// 获取或设置用户居住省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 获取或设置用户居住城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 获取或设置用户所在行业
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// 获取或设置用户的个人介绍(一句话)
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 获取或设置用户个人描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置用户的手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 获取或设置绑定的用户
        /// </summary>
        public virtual Customer Customer { get; set; }

    }

}
