using System;
using System.Linq;
using Blog.Libraries.Core.Data;
using System.Collections.Generic;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Services.Members
{

    public class CustomerService : BaseEntity, ICustomerService
    {

        #region Fields

        private readonly IRepository<Customer> _customerRepository;

        #endregion

        #region Constructor

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
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

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Update(customer);
        }

        #endregion

    }

}
