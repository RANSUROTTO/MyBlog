using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Services.Members
{

    /// <summary>
    /// 用户业务接口
    /// </summary>
    public interface ICustomerService
    {

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="customerId">用户ID</param>
        /// <returns>一个用户</returns>
        Customer GetCustomerById(long customerId);

        /// <summary>
        /// 根据Guid获取用户
        /// </summary>
        /// <param name="customerGuid">Guid</param>
        /// <returns>用户</returns>
        Customer GetCustomerByGuid(Guid customerGuid);

        /// <summary>
        /// 根据一组用户ID获取一组用户
        /// </summary>
        /// <param name="customerIds">用户ID</param>
        /// <returns>用户</returns>
        IList<Customer> GetCustomersByIds(long[] customerIds);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="customer">用户</param>
        void UpdateCustomer(Customer customer);



    }

}
