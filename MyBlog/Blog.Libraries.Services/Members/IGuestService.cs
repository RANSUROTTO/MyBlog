using System;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Services.Members
{
    /// <summary>
    /// 游客业务接口
    /// </summary>
    public interface IGuestService
    {

        /// <summary>
        /// 通过ID获取游客
        /// </summary>
        /// <param name="guestId">游客ID</param>
        /// <returns>一个游客</returns>
        Guest GetGuestById(long guestId);

        /// <summary>
        /// 通过Guid获取游客
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>游客</returns>
        Guest GetGuestByGuid(Guid guid);

    }

}
