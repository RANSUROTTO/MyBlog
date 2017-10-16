using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Presentation.Web.Models.Customer
{

    public class CustomerLoginModel
    {

        /// <summary>
        /// 用户登录名 / Email / Phone?
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string Password { get; set; }

    }

}