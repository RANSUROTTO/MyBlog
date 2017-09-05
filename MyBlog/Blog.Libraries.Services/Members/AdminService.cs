using System;
using System.Linq;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Services.Members
{
    public class AdminService : IAdminService
    {

        #region Fields

        private readonly IRepository<Admin> _adminRepository;

        #endregion

        #region Constructor

        public AdminService(IRepository<Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        #endregion

        public Admin GetAdminById(long id)
        {
            throw new NotImplementedException();
        }

        public Admin GetAdminByGuid(Guid guid)
        {
            if (guid == Guid.Empty)
                return null;

            var query = from a in _adminRepository.Table
                        where a.Guid == guid
                        orderby a.Id
                        select a;

            return query.FirstOrDefault();
        }

    }

}
