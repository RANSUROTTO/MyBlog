using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Services.Services
{

    public class Service<T> : IService<T> where T : BaseEntity
    {

        #region Fields

        private readonly IRepository<T> _repository;

        #endregion

        #region Constructor

        public Service(IRepository<T> repository)
        {
            this._repository = repository;
        }

        #endregion

        #region Properties

        public IQueryable<T> Table => _repository.Table;
        public List<T> Data => _repository.Data;

        #endregion

        #region Methods

        public Task<T> GetByIdAsync(params object[] id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task InsertAsync(T entity)
        {
            return _repository.InsertAsync(entity);
        }

        public Task InsertAsync(IEnumerable<T> entities)
        {
            return _repository.InsertAsync(entities);
        }

        public Task UpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public Task UpdateAsync(T entity, params Expression<Func<T, PropertyInfo>>[] fields)
        {
            return _repository.UpdateAsync(entity, fields);
        }

        public Task UpdateAsync(IEnumerable<T> entities)
        {
            return _repository.UpdateAsync(entities);
        }

        public Task DeleteAsync(T entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public Task DeleteAsync(IEnumerable<T> entities)
        {
            return _repository.DeleteAsync(entities);
        }

        public T GetById(params object[] id)
        {
            return _repository.GetById(id);
        }

        public T GetSingle(Expression<Func<T, bool>> @where)
        {
            return _repository.GetSingle(where);
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> @where)
        {
            return _repository.GetSingleAsync(where);
        }

        public void Insert(T entity)
        {
            _repository.Insert(entity);
        }

        public void Insert(IEnumerable<T> entities)
        {
            _repository.Insert(entities);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Update(T entity, params Expression<Func<T, PropertyInfo>>[] fields)
        {
            _repository.Update(entity, fields);
        }

        public void Update(IEnumerable<T> entities)
        {
            _repository.Update(entities);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _repository.Delete(entities);
        }

        public void ExecuteDbTran(Action execute)
        {
            _repository.ExecuteDbTran(execute);
        }

        public void ExecuteRequiredTran(Action execute)
        {
            _repository.ExecuteRequiredTran(execute);
        }

        #endregion

    }

}
