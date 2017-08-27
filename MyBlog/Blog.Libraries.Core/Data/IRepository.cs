using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Blog.Libraries.Core.Data
{

    /// <summary>
    /// 仓储
    /// </summary>
    public partial interface IRepository<T> where T : BaseEntity
    {

        /// <summary>
        /// 根据标识符获得实体对象
        /// </summary>
        /// <param name="id">标识符</param>
        /// <returns>实体</returns>
        T GetById(params object[] id);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Insert(T entity);

        /// <summary>
        /// 插入多个实体通过集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(T entity);

        /// <summary>
        /// 更新实体,指定更新属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fields">属性</param>
        void Update(T entity, params Expression<Func<T, PropertyInfo>>[] fields);

        /// <summary>
        /// 更新多个实体通过集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(T entity);

        /// <summary>
        /// 删除多个实体通过集合
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// 获取实体数据集
        /// </summary>
        IQueryable<T> Table { get; }

    }

    public partial interface IRepository<T> where T : BaseEntity
    {

        /// <summary>
        /// 根据标识符异步获得实体对象
        /// </summary>
        /// <param name="id">标识符</param>
        /// <returns>实体</returns>
        Task<T> GetByIdAsync(params object[] id);

        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task InsertAsync(T entity);

        /// <summary>
        /// 异步插入多个实体通过集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        Task InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// 异步更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// 异步更新实体,指定更新属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fields">属性</param>
        Task UpdateAsync(T entity, params Expression<Func<T, PropertyInfo>>[] fields);

        /// <summary>
        /// 异步更新多个实体通过集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        Task UpdateAsync(IEnumerable<T> entities);

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// 异步删除多个实体通过集合
        /// </summary>
        /// <param name="entities"></param>
        Task DeleteAsync(IEnumerable<T> entities);

    }

}
