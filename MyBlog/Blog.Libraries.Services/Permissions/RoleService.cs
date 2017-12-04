using Blog.Libraries.Core.Context;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Member;
using Blog.Libraries.Data.Domain.Permissions;

namespace Blog.Libraries.Services.Permissions
{

    public class RoleService : IRoleService
    {

        #region fields

        private readonly IWorkContext _workContext;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<RoleGroup> _roleGroupRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoleService(IWorkContext workContext, IRepository<UserRole> userRoleRepository, IRepository<RoleGroup> roleGroupRepository)
        {
            _workContext = workContext;
            _userRoleRepository = userRoleRepository;
            _roleGroupRepository = roleGroupRepository;
        }

        #endregion

        #region Methods

        public bool Authorize()
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(string classFullName, string methodName)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(string classFullName, string methodName, Admin admin)
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize(string classFullName, string methodName, string roleString)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Utilities



        #endregion

    }

}
