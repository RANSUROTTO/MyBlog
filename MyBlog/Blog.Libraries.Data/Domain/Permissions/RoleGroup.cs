using System.Collections.Generic;
using System.Linq;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Domain.Permissions
{

    /// <summary>
    /// ����һ��Ȩ����
    /// </summary>
    public class RoleGroup : BaseEntity
    {

        /// <summary>
        /// ��ȡ������Ȩ���������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������Ȩ����ı�ע��Ϣ
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ��ȡ������Ȩ���ַ���
        /// </summary>
        public string RoleString { get; set; }


        private ICollection<UserRole> _userRoles;
        /// <summary>
        /// ��ȡ�����ö�Ӧ�û�Ȩ��
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get { return _userRoles?.Where(p => !p.IsDeleted).ToList(); } set { _userRoles = value; } }

    }

}
