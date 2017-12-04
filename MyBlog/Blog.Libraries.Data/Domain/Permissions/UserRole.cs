using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Data.Domain.Permissions
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
        /// ��ȡ�����ñ�ע��Ϣ
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ��ȡ������Ȩ�޶�Ӧ�û�
        /// </summary>
        public virtual Admin Admin { get; set; }

        /// <summary>
        /// ��ȡ������Ȩ�޶�Ӧ��
        /// </summary>
        public virtual RoleGroup RoleGroup { get; set; }

    }

}
