using System;
using System.Collections.Generic;
using Blog.Libraries.Data.Domain.Members;
using Blog.Libraries.Data.Domain.Members.Enum;

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
        /// 根据用户名或用户邮箱获取用户
        /// </summary>
        /// <param name="usernameOrEmail">用户名或用户邮箱</param>
        /// <returns>用户</returns>
        Customer GetCustomerByUsernameOrEmail(string usernameOrEmail);

        /// <summary>
        /// 根据用户Id获取用户历史密码列表
        /// </summary>
        /// <param name="customerId">用户ID</param>
        /// <returns>用户历史密码列表</returns>
        IList<CustomerPassword> GetPasswordsByCustomerId(long customerId);

        /// <summary>
        /// 根据用户Id获取该用户当前的密码
        /// </summary>
        /// <param name="customerId">用户ID</param>
        /// <returns>用户密码</returns>
        CustomerPassword GetCurrentPassword(long customerId);

        /// <summary>
        /// 根据一组用户ID获取一组用户
        /// </summary>
        /// <param name="customerIds">用户ID</param>
        /// <returns>用户</returns>
        IList<Customer> GetCustomersByIds(long[] customerIds);

        /// <summary>
        /// 密码对比
        /// </summary>
        /// <param name="customerPassword">用户密码</param>
        /// <param name="enteredPassword">键入密码</param>
        /// <returns>对比结果</returns>
        bool PasswordMatch(CustomerPassword customerPassword, string enteredPassword);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="customer">用户</param>
        void UpdateCustomer(Customer customer);

        /// <summary>
        /// 验证用户是否能够登录
        /// </summary>
        /// <param name="usernameOrEmail">用户名或者邮箱账号</param>
        /// <param name="password">用户密码</param>
        /// <returns>登录结果</returns>
        CustomerLoginResult ValidateCustomer(string usernameOrEmail, string password);

    }

}
