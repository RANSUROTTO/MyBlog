
namespace Blog.Libraries.Core.Domain.Members
{

    public interface IAdmin
    {

        /// <summary>
        /// 获取或设置绑定用户
        /// </summary>
        ICustomer Customer { get; set; }

        /// <summary>
        /// 获取或设置管理员昵称
        /// </summary>
        string AdminName { get; set; }

    }

}
