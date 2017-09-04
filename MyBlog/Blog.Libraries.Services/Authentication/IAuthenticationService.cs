using System;

namespace Blog.Libraries.Services.Authentication
{

    /// <summary>
    /// 身份验证业务接口
    /// </summary>
    public interface IAuthenticationService
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <param name="type">登录用户类型</param>
        /// <param name="expirantion">过期时间</param>
        void SignIn(Guid guid, AuthenticationType type, DateTime? expirantion = null);

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <param name="type">需退出的已登录用户类型</param>
        void SignOut(Guid guid, AuthenticationType type);

        /// <summary>
        /// 获取已登录的用户
        /// </summary>
        /// <typeparam name="T">用户类型</typeparam>
        /// <param name="type">用户类型枚举</param>
        /// <returns></returns>
        T GetAuthenticationMember<T>(AuthenticationType type);



    }

}
