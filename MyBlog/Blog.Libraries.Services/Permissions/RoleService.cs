using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Blog.Libraries.Core.Caching;
using Blog.Libraries.Core.ComponentModel;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Data.Domain.Member;
using Newtonsoft.Json;

namespace Blog.Libraries.Services.Permissions
{

    /// <summary>
    /// 仅限使用于限制管理员(Admin)的权限
    /// </summary>
    public class RoleService : IRoleService
    {

        #region fields

        private readonly IWorkContext _workContext;
        private readonly HttpContextBase _httpContextBase;
        private readonly ICacheManager _cacheManager;

        private const string AdminRoleCacheKey = "ransurotto.com.role.admin_{0}";
        private const string GroupRoleCacheKey = "ransurotto.com.role.group_{0}";

        #endregion

        #region Constructor

        public RoleService(IWorkContext workContext, HttpContextBase httpContextBase, ICacheManager cacheManager)
        {
            _workContext = workContext;
            _httpContextBase = httpContextBase;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public bool Authorize()
        {

            throw new System.NotImplementedException();
        }

        public bool Authorize(string classFullName, string methodName)
        {
            var admin = _workContext.Admin as Admin;
            return Authorize(classFullName, methodName, admin);
        }

        public bool Authorize(string classFullName, string methodName, Admin admin)
        {
            if (admin == null)
                return false;

            return Authorize(classFullName, methodName, GetRoleStringCache(admin, "admin"))
                   || Authorize(classFullName, methodName, GetRoleStringCache(admin, "group"));
        }

        public bool Authorize(string classFullName, string methodName, string roleString)
        {
            //获取classType,获取失败将抛出异常
            var classType = Type.GetType(classFullName, true);
            var method = classType.GetMethod(methodName);
            if (method == null) throw new Exception("无法找到对应方法");

            var roleAttribute = method.GetCustomAttribute<RoleActionAttribute>(true);
            if (roleAttribute == null) return true;

            if (string.IsNullOrEmpty(roleString)) return false;
            var authorizeRoleList = JsonConvert.DeserializeObject<List<RoleItem>>(roleString);

            return authorizeRoleList.Any(p => p.ClassName == classFullName
                && p.AuthorizeMethods.Split('|').Contains(roleAttribute.RoleCode));
        }

        #endregion

        #region Utilities

        private string GetRoleStringCache(Admin admin, string roleStringType)
        {
            var cacheTime = (int)(DateTime.Now.AddDays(7) - DateTime.Now).TotalMinutes;
            switch (roleStringType)
            {
                case "admin":
                    return _cacheManager.Get(GetAdminRoleCacheKeyByAdmin(admin), cacheTime, () => admin.UserRole?.RoleString);
                case "group":
                    return _cacheManager.Get(GetGroupRoleCacheKeyByAdmin(admin), cacheTime, () => admin.UserRole?.RoleGroup?.RoleString);
                default:
                    throw new ArgumentException("不支持的roleStringType", "roleStringType");
            }
        }

        private string GetAdminRoleCacheKeyByAdmin(Admin admin)
        {
            if (admin == null)
                throw new ArgumentNullException("admin");
            return string.Format(AdminRoleCacheKey, admin.Id);
        }

        private string GetGroupRoleCacheKeyByAdmin(Admin admin)
        {
            if (admin == null)
                throw new ArgumentNullException("admin");
            return string.Format(GroupRoleCacheKey, admin.UserRole?.RoleGroup?.Id);
        }

        #endregion

    }

}
