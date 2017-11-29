using System;
using System.Linq;
using System.Text.RegularExpressions;
using Blog.Libraries.Core.Domain.Members;

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

        /// <summary>
        /// ������ʵ��û�����
        /// </summary>
        public AuthenticationType? AuthenticationType { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ͨ��Ȩ�޴������ú���Ȩ��
        /// </summary>
        public RoleActionAttribute(string roleCode)
        {
            if (RoleCodeValidator(roleCode))
                this.RoleCode = RoleCode;
        }

        /// <summary>
        /// ͨ�����ò���������ΪȨ�޴������ú���Ȩ��
        /// </summary>
        public RoleActionAttribute(RoleActionType roleActionType)
        {
            this.RoleCode = GetRoleCodeByRoleActionType(roleActionType);
        }

        /// <summary>
        /// ͨ��Ȩ�޴�����û��������ú���Ȩ��
        /// </summary>
        public RoleActionAttribute(string roleCode, AuthenticationType type)
        {
            if (RoleCodeValidator(roleCode))
                this.RoleCode = RoleCode;
            this.AuthenticationType = type;
        }

        /// <summary>
        /// ͨ�����ò���������ΪȨ�޴�����û��������ú���Ȩ��
        /// </summary>
        public RoleActionAttribute(RoleActionType roleActionType, AuthenticationType type)
        {
            this.RoleCode = GetRoleCodeByRoleActionType(roleActionType);
            this.AuthenticationType = type;
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
