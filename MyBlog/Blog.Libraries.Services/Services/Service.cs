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
    /// 业务层实现
    /// </summary>
    public class Service<T> : IService<T> where T : BaseEntity
    {

        #region Fields

        protected readonly IRepository<T> Repository;

        #endregion

        #region Constructor

        protected Service(IRepository<T> repository)
        {
            this.Repository = repository;
        }

        #endregion

        #region Properties

        public IQueryable<T> Table => Repository.Table;
        public List<T> Data => Repository.Data;

        #endregion

        #region Methods

        public Task<T> GetByIdAsync(params object[] id)
        {
            return Repository.GetByIdAsync(id);
        }

        public Task InsertAsync(T entity)
        {
            return Repository.InsertAsync(entity);
        }

        public Task InsertAsync(IEnumerable<T> entities)
        {
            return Repository.InsertAsync(entities);
        }

        public Task UpdateAsync(T entity)
        {
            return Repository.UpdateAsync(entity);
        }

        public Task UpdateAsync(T entity, params Expression<Func<T, PropertyInfo>>[] fields)
        {
            return Repository.UpdateAsync(entity, fields);
        }

        public Task UpdateAsync(IEnumerable<T> entities)
        {
            return Repository.UpdateAsync(entities);
        }

        public Task DeleteAsync(T entity)
        {
            return Repository.DeleteAsync(entity);
        }

        public Task DeleteAsync(IEnumerable<T> entities)
        {
            return Repository.DeleteAsync(entities);
        }

        public T GetById(params object[] id)
        {
            return Repository.GetById(id);
        }

        public T GetSingle(Expression<Func<T, bool>> @where)
        {
            return Repository.GetSingle(where);
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> @where)
        {
            return Repository.GetSingleAsync(where);
        }

        public void Insert(T entity)
        {
            Repository.Insert(entity);
        }

        public void Insert(IEnumerable<T> entities)
        {
            Repository.Insert(entities);
        }

        public void Update(T entity)
        {
            Repository.Update(entity);
        }

        public void Update(T entity, params Expression<Func<T, PropertyInfo>>[] fields)
        {
            Repository.Update(entity, fields);
        }

        public void Update(IEnumerable<T> entities)
        {
            Repository.Update(entities);
        }

        public void Delete(T entity)
        {
            Repository.Delete(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            Repository.Delete(entities);
        }

        public void ExecuteDbTran(Action execute)
        {
            Repository.ExecuteDbTran(execute);
        }

        public void ExecuteRequiredTran(Action execute)
        {
            Repository.ExecuteRequiredTran(execute);
        }

        #endregion

    }

}
