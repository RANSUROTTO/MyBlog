using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Services.Services
{

    /// <summary>
    /// ҵ���ӿ�
    /// </summary>
    public partial interface IService<T> where T : BaseEntity
    {

        /// <summary>
        /// ���ݱ�ʶ�����ʵ�����
        /// </summary>
        /// <param name="id">��ʶ��</param>
        /// <returns>ʵ��</returns>
        T GetById(params object[] id);

        /// <summary>
        /// ����������ȡΨһ����
        /// </summary>
        /// <param name="where">����</param>
        /// <returns>ʵ��</returns>
        T GetSingle(Expression<Func<T, bool>> where);

        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void Insert(T entity);

        /// <summary>
        /// ������ʵ��ͨ������
        /// </summary>
        /// <param name="entities">ʵ�弯��</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void Update(T entity);

        /// <summary>
        /// ����ʵ��,ָ����������
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <param name="fields">����</param>
        void Update(T entity, params Expression<Func<T, PropertyInfo>>[] fields);

        /// <summary>
        /// ���¶��ʵ��ͨ������
        /// </summary>
        /// <param name="entities">ʵ�弯��</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// ɾ��ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void Delete(T entity);

        /// <summary>
        /// ɾ�����ʵ��ͨ������
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// ִ�����ݿ��������
        /// </summary>
        /// <param name="execute"></param>
        void ExecuteDbTran(Action execute);

        /// <summary>
        /// ִ�зֲ�ʽ�������
        /// </summary>
        /// <param name="execute"></param>
        void ExecuteRequiredTran(Action execute);

        /// <summary>
        /// ��ȡʵ�����ݼ�
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// ȫ������
        /// </summary>
        List<T> Data { get; }

    }

    public partial interface IService<T> where T : BaseEntity
    {

        /// <summary>
        /// ���ݱ�ʶ���첽���ʵ�����
        /// </summary>
        /// <param name="id">��ʶ��</param>
        /// <returns>ʵ��</returns>
        Task<T> GetByIdAsync(params object[] id);

        /// <summary>
        /// ���������첽��ȡ��һ��ʵ�����
        /// </summary>
        /// <param name="where">��������</param>
        /// <returns>ʵ��</returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// �첽����ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        Task InsertAsync(T entity);

        /// <summary>
        /// �첽������ʵ��ͨ������
        /// </summary>
        /// <param name="entities">ʵ�弯��</param>
        Task InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// �첽����ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// �첽����ʵ��,ָ����������
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <param name="fields">����</param>
        Task UpdateAsync(T entity, params Expression<Func<T, PropertyInfo>>[] fields);

        /// <summary>
        /// �첽���¶��ʵ��ͨ������
        /// </summary>
        /// <param name="entities">ʵ�弯��</param>
        Task UpdateAsync(IEnumerable<T> entities);

        /// <summary>
        /// �첽ɾ��ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// �첽ɾ�����ʵ��ͨ������
        /// </summary>
        /// <param name="entities"></param>
        Task DeleteAsync(IEnumerable<T> entities);

    }

}
