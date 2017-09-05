using System;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Services.Members
{

    /// <summary>
    /// 管理员业务接口
    /// </summary>
    public interface IAdminService
    {

        /// <summary>
        /// 通过ID获取管理员
        /// </summary>
        /// <param name="id">管理员ID</param>
        /// <returns>一个管理员</returns>
        Admin GetAdminById(long id);

        /// <summary>
        /// 通过Guid获取管理员
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>管理员</returns>
        Admin GetAdminByGuid(Guid guid);

    }

}
