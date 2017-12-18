namespace Blog.Presentation.Framework.Services.Permissions
{

    public class RoleItem
    {

        /// <summary>
        /// 授权Area
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 授权Controller
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 授权Action列表
        /// </summary>
        public string AuthorizeActions { get; set; }

    }

}
