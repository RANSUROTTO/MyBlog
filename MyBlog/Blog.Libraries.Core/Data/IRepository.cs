using System.Collections.Generic;
using System.Linq;

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
        T GetById(object id);

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
        T GetByIdAsync(object id);

        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        void InsertAsync(T entity);

        /// <summary>
        /// 异步插入多个实体通过集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// 异步更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void UpdateAsync(T entity);

        /// <summary>
        /// 异步更新多个实体通过集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void UpdateAsync(IEnumerable<T> entities);

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void DeleteAsync(T entity);

        /// <summary>
        /// 异步删除多个实体通过集合
        /// </summary>
        /// <param name="entities"></param>
        void DeleteAsync(IEnumerable<T> entities);

    }

}
