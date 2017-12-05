namespace Blog.Libraries.Services.Permissions
{

    public class RoleItem
    {

        /// <summary>
        /// 授权类
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 授权函数集合
        /// </summary>
        public string AuthorizeMethods { get; set; }

    }

}
