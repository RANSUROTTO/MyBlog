using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Domain.Jurisdiction
{

    /// <summary>
    /// ����һ���û�Ȩ��
    /// </summary>
    public class UserRole : BaseEntity
    {

        /// <summary>
        /// ��ȡ������Ȩ���ַ���
        /// </summary>
        public string RoleString { get; set; }

        /// <summary>
        /// ��ȡ������Ȩ�޶�Ӧ�û�
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// ��ȡ������Ȩ�޶�Ӧ��
        /// </summary>
        public virtual RoleGroup RoleGroup { get; set; }

    }

}
