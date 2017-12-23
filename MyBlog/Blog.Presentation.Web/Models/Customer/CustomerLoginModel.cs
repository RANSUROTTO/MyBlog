namespace Blog.Presentation.Web.Models.Customer
{

    public class CustomerLoginModel
    {

        /// <summary>
        /// 获取或设置登录用户名或电子邮箱
        /// </summary>
        public string UsernameOrEmail { get; set; }

        /// <summary>
        /// 获取或设置登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置一个值,该值指示是否记住登录状态
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示登录失败验证消息
        /// </summary>
        public string Message { get; set; }

    }

}