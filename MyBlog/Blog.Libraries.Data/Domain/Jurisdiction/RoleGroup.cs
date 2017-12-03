using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Domain.Jurisdiction
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

    }

}
