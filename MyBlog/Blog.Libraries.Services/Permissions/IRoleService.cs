using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Services.Permissions
{

    public interface IRoleService
    {

        /// <summary>
        /// �жϵ�ǰ����Ա�Ƿ��е�ǰ���ʲ���Ȩ��
        /// ���÷�����Ҫ HTTP������ ����
        /// </summary>
        /// <returns>���</returns>
        bool Authorize();

        /// <summary>
        /// �жϵ�ǰ����Ա�Ƿ���ĳ����Ȩ��
        /// </summary>
        /// <param name="area">��������</param>
        /// <param name="controllerName">����������</param>
        /// <param name="actionName">��������</param>
        /// <returns>���</returns>
        bool Authorize(string area, string controllerName, string actionName);

        /// <summary>
        /// �ж�ָ������Ա�Ƿ���ĳ����Ȩ��
        /// </summary>
        /// <param name="area">��������</param>
        /// <param name="controllerName">����������</param>
        /// <param name="actionName">��������</param>
        /// <param name="admin">������Ա</param>
        /// <returns>���</returns>
        bool Authorize(string area, string controllerName, string actionName, Admin admin);

        /// <summary>
        /// �ж�Ȩ���ַ����ǰ���ĳ����Ȩ��
        /// </summary>
        /// <param name="area">��������</param>
        /// <param name="controllerName">����������</param>
        /// <param name="actionName">��������</param>
        /// <param name="roleString">Ȩ���ַ���</param>
        /// <returns>���</returns>
        bool Authorize(string area, string controllerName, string actionName, string roleString);

    }

}
