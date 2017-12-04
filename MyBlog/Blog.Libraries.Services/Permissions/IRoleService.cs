using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Services.Permissions
{

    public interface IRoleService
    {

        /// <summary>
        /// 判断当前管理员是否有当前访问功能权限
        /// ，该方法需要 HTTP上下文 可用
        /// </summary>
        /// <returns>结果</returns>
        bool Authorize();

        /// <summary>
        /// 判断当前管理员是否有某功能权限
        /// </summary>
        /// <param name="classFullName">类的完全名称(包含命名空间)</param>
        /// <param name="methodName">方法名称</param>
        /// <returns>结果</returns>
        bool Authorize(string classFullName, string methodName);

        /// <summary>
        /// 判断指定管理员是否有某功能权限
        /// </summary>
        /// <param name="classFullName">类的完全名称(包含命名空间)</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="admin">检查管理员</param>
        /// <returns>结果</returns>
        bool Authorize(string classFullName, string methodName, Admin admin);

        /// <summary>
        /// 判断权限字符串是包含某功能权限
        /// </summary>
        /// <param name="classFullName">类的完全名称(包含命名空间)</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="roleString">权限字符串</param>
        /// <returns>结果</returns>
        bool Authorize(string classFullName, string methodName, string roleString);

    }

}
