using System;
using Blog.Libraries.Core.Domain.Members;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blog.Libraries.Core.ComponentModel
{

    /// <summary>
    /// ��ҪȨ�޵ķ���
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleActionAttribute : Attribute
    {

        #region Fields

        private readonly string[] _notUseCode = { "INSERT", "UPDATE", "SELECT", "DELETE" };

        #endregion

        #region Properties

        /// <summary>
        /// Ȩ�޴���
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
                    throw new ArgumentException("�޷��ҵ���Ӧ������ʽ��Ȩ�޴���", "actionType");
            }
        }

        private bool RoleCodeValidator(string roleCode)
        {
            if (string.IsNullOrEmpty(roleCode))
                throw new ArgumentException("Ȩ�޴������Ϊ��Чֵ", "roleCode");
            if (_notUseCode.Contains(roleCode))
                throw new ArgumentException(string.Format("Ȩ�޴��벻�����ڡ�{0}����Χ��", string.Join(",", _notUseCode)), "roleCode");

            Regex regex = new Regex("^[A-Z]{1,16}$");
            if (!regex.IsMatch(roleCode))
            {
                throw new ArgumentException("Ȩ�޴����ʽҪ�󣺴�дӢ����ĸ�����Ȳ�����16��", "roleCode");
            }
            return true;
        }

        #endregion

    }

    /// <summary>
    /// ������ʽ
    /// </summary>
    public enum RoleActionType : byte
    {
        /// <summary>
        /// ���
        /// </summary>
        Insert,
        /// <summary>
        /// �鿴
        /// </summary>
        Select,
        /// <summary>
        /// ����
        /// </summary>
        Update,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete
    }

}
