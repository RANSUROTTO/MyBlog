using System;
using Blog.Libraries.Core.Domain.Members;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blog.Libraries.Core.ComponentModel
{

    /// <summary>
    /// 需要权限的方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleActionAttribute : Attribute
    {

        #region Fields

        private readonly string[] _notUseCode = { "INSERT", "UPDATE", "SELECT", "DELETE" };

        #endregion

        #region Properties

        /// <summary>
        /// 权限代码
        /// </summary>
        public string RoleCode { get; set; }

        #endregion

        #region Constructor

        public RoleActionAttribute(string roleCode)
        {
            if (RoleCodeValidator(roleCode))
                this.RoleCode = RoleCode;
        }

        public RoleActionAttribute(RoleActionType roleActionType)
        {
            this.RoleCode = GetRoleCodeByRoleActionType(roleActionType);
        }

        #endregion

        #region Utilities

        private string GetRoleCodeByRoleActionType(RoleActionType actionType)
        {
            switch (actionType)
            {
                case RoleActionType.Insert:
                    return "INSERT";
                case RoleActionType.Update:
                    return "UPDATE";
                case RoleActionType.Select:
                    return "SELECT";
                case RoleActionType.Delete:
                    return "DELETE";
                default:
                    throw new ArgumentException("无法找到对应操作形式的权限代码", "actionType");
            }
        }

        private bool RoleCodeValidator(string roleCode)
        {
            if (string.IsNullOrEmpty(roleCode))
                throw new ArgumentException("权限代码必须为有效值", "roleCode");
            if (_notUseCode.Contains(roleCode))
                throw new ArgumentException(string.Format("权限代码不允许在【{0}】范围内", string.Join(",", _notUseCode)), "roleCode");

            Regex regex = new Regex("^[A-Z]{1,16}$");
            if (!regex.IsMatch(roleCode))
            {
                throw new ArgumentException("权限代码格式要求：大写英文字母，长度不大于16。", "roleCode");
            }
            return true;
        }

        #endregion

    }

    /// <summary>
    /// 操作形式
    /// </summary>
    public enum RoleActionType : byte
    {
        /// <summary>
        /// 添加
        /// </summary>
        Insert,
        /// <summary>
        /// 查看
        /// </summary>
        Select,
        /// <summary>
        /// 更新
        /// </summary>
        Update,
        /// <summary>
        /// 删除
        /// </summary>
        Delete
    }

}
