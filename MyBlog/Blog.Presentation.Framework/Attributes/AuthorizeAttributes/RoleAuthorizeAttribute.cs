using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Blog.Libraries.Core.Domain.Members;

namespace Blog.Presentation.Framework.Attributes.AuthorizeAttributes
{

    /// <summary>
    /// ��Ȩ���
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
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
        /// ��Ȩ�û�����
        /// </summary>
        public AuthenticationType? AuthenticationType { get; set; }

        /// <summary>
        /// Ȩ������
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// ͨ��Ȩ�޴������ú���Ȩ��
        /// </summary>
        public RoleAuthorizeAttribute(string roleCode)
        {
            if (RoleCodeValidator(roleCode))
                this.RoleCode = RoleCode;
        }

        /// <summary>
        /// ͨ����������������ΪȨ�޴������ú���Ȩ��
        /// </summary>
        public RoleAuthorizeAttribute(RoleActionType roleActionType)
        {
            this.RoleCode = GetRoleCodeByRoleActionType(roleActionType);
        }

        /// <summary>
        /// ͨ��Ȩ�޴������Ȩ�û��������ú���Ȩ��
        /// </summary>
        public RoleAuthorizeAttribute(string roleCode, AuthenticationType type)
        {
            if (RoleCodeValidator(roleCode))
                this.RoleCode = RoleCode;
            this.AuthenticationType = type;
        }

        /// <summary>
        /// ͨ����������������ΪȨ�޴������Ȩ�û��������ú���Ȩ��
        /// </summary>
        public RoleAuthorizeAttribute(RoleActionType roleActionType, AuthenticationType type)
        {
            this.RoleCode = GetRoleCodeByRoleActionType(roleActionType);
            this.AuthenticationType = type;
        }

        #endregion

        #region Method

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        #endregion

        #region Utilities

        private string GetRoleCodeByRoleActionType(RoleActionType actionType)
        {
            switch (actionType)
            {
                case RoleActionType.Insert:
                    Description = "����";
                    return "INSERT";
                case RoleActionType.Update:
                    Description = "�༭";
                    return "UPDATE";
                case RoleActionType.Select:
                    Description = "��ѯ";
                    return "SELECT";
                case RoleActionType.Delete:
                    Description = "ɾ��";
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
    /// ������������
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
