using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Services.Permissions
{

    public interface IRoleService
    {

        /// <summary>
        /// �жϵ�ǰ����Ա�Ƿ��е�ǰ���ʹ���Ȩ��
        /// ���÷�����Ҫ HTTP������ ����
        /// </summary>
        /// <returns>���</returns>
        bool Authorize();

        /// <summary>
        /// �жϵ�ǰ����Ա�Ƿ���ĳ����Ȩ��
        /// </summary>
        /// <param name="classFullName">�����ȫ����(���������ռ�)</param>
        /// <param name="methodName">��������</param>
        /// <returns>���</returns>
        bool Authorize(string classFullName, string methodName);

        /// <summary>
        /// �ж�ָ������Ա�Ƿ���ĳ����Ȩ��
        /// </summary>
        /// <param name="classFullName">�����ȫ����(���������ռ�)</param>
        /// <param name="methodName">��������</param>
        /// <param name="admin">������Ա</param>
        /// <returns>���</returns>
        bool Authorize(string classFullName, string methodName, Admin admin);

        /// <summary>
        /// �ж�Ȩ���ַ����ǰ���ĳ����Ȩ��
        /// </summary>
        /// <param name="classFullName">�����ȫ����(���������ռ�)</param>
        /// <param name="methodName">��������</param>
        /// <param name="roleString">Ȩ���ַ���</param>
        /// <returns>���</returns>
        bool Authorize(string classFullName, string methodName, string roleString);

    }

}
