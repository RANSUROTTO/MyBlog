using System;
using System.Linq;
using Blog.Libraries.Core.Data;
using System.Collections.Generic;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Data.Domain.Members;
using Blog.Libraries.Data.Domain.Members.Enum;
using Blog.Libraries.Data.Settings;

namespace Blog.Libraries.Services.Members
{

    public class CustomerService : ICustomerService
    {

        #region Fields

        private readonly IRepository<Customer> _customerRepository;
        private readonly CustomerSettings _customerSettings;
        private readonly IRepository<CustomerPassword> _customerPasswordRepository;

        #endregion

        #region Constructor

        public CustomerService(IRepository<Customer> customerRepository, CustomerSettings customerSettings, IRepository<CustomerPassword> customerPasswordRepository)
        {
            _customerRepository = customerRepository;
            _customerSettings = customerSettings;
            _customerPasswordRepository = customerPasswordRepository;
        }

        #endregion

        #region Methods

        public Customer GetCustomerById(long customerId)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByGuid(Guid customerGuid)
        {
            if (customerGuid == Guid.Empty)
                return null;

            var query = from c in _customerRepository.Table
                        where c.Guid == customerGuid
                        orderby c.Id
                        select c;

            return query.FirstOrDefault();
        }

        public IList<Customer> GetCustomersByIds(long[] customerIds)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByUsernameOrEmail(string usernameOrEmail)
        {
            IQueryable<Customer> query;

            switch (_customerSettings.CustomerLoginPattern)
            {

                case CustomerLoginPattern.OnlyUseUserName:
                    query = from c in _customerRepository.Table
                            where c.Username.Equals(usernameOrEmail)
                            select c;
                    return query.FirstOrDefault();

                case CustomerLoginPattern.OnlyUseEmail:
                    query = from c in _customerRepository.Table
                            where c.Email.Equals(usernameOrEmail)
                            select c;
                    return query.FirstOrDefault();

                case CustomerLoginPattern.Full:
                    if (CommonHelper.IsValidEmail(usernameOrEmail))
                    {
                        query = from c in _customerRepository.Table
                                where c.Email.Equals(usernameOrEmail)
                                select c;
                        return query.FirstOrDefault();
                    }
                    else
                    {
                        query = from c in _customerRepository.Table
                                where c.Username.Equals(usernameOrEmail)
                                select c;
                        return query.FirstOrDefault();
                    }

                default:
                    return null;

            }
        }

        public IList<CustomerPassword> GetPasswordsByCustomerId(long customerId)
        {
            throw new NotImplementedException();
        }

        public CustomerPassword GetCurrentPassword(long customerId)
        {
            var query = from p in _customerPasswordRepository.Table
                        where p.Customer.Id == customerId
                        orderby p.CreateAt descending
                        select p;

            return query.FirstOrDefault();
        }

        public bool PasswordMatch(CustomerPassword customerPassword, string enteredPassword)
        {
            switch (_customerSettings.DefaultPasswordFormat)
            {
                case PasswordFormat.Clear:
                    return customerPassword.Password.Equals(enteredPassword, StringComparison.InvariantCulture);

                case PasswordFormat.Encrypted:
                    break;

                case PasswordFormat.Hashed:
                    break;

                default:
                    return false;
            }
            throw new NotImplementedException();
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Update(customer);
        }

        public CustomerLoginResult ValidateCustomer(string usernameOrEmail, string password)
        {
            var customer = this.GetCustomerByUsernameOrEmail(usernameOrEmail);

            if (customer == null)
                return CustomerLoginResult.CustomerNotExist;

            if (customer.CannotLoginUntilDate != null && customer.CannotLoginUntilDate > DateTime.UtcNow)
                return CustomerLoginResult.LockedOut;

            if (!this.PasswordMatch(this.GetCurrentPassword(customer.Id), password))
            {
                customer.FailedLoginAttempts++;
                if (_customerSettings.FailedPasswordAllowedAttempts > 0 && customer.FailedLoginAttempts >=
                    _customerSettings.FailedPasswordAllowedAttempts)
                {
                    customer.FailedLoginAttempts = 0;
                    customer.CannotLoginUntilDate =
                        DateTime.UtcNow.AddMinutes(_customerSettings.FailedPasswordLockoutMinutes);
                }
                this.UpdateCustomer(customer);
                return CustomerLoginResult.WrongPassword;
            }

            //clear state
            customer.FailedLoginAttempts = 0;
            customer.RequireReLogin = false;
            customer.CannotLoginUntilDate = null;
            customer.LastLoginDate = DateTime.UtcNow;

            return CustomerLoginResult.Successful;
        }

        #endregion

    }

}
