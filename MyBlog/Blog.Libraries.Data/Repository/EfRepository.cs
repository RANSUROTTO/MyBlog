using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Context;
using EntityFramework.Extensions;

namespace Blog.Libraries.Data.Repository
{

    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {

        #region Field

        private readonly IDbContext _context;

        private IDbSet<T> _entities;

        #endregion

        #region Properties

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities.Where(p => !p.IsDeleted);
            }
        }

        public List<T> Data { get { return Entities.ToList(); } }

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        #endregion

        #region Methods

        public virtual T GetById(params object[] id)
        {
            return Entities.Find(id);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> where)
        {
            return Entities.FirstOrDefault(where);
        }

        public virtual Task<T> GetByIdAsync(params object[] id)
        {
            return Task.Run(() => Entities.Find(id));
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> @where)
        {
            return Task.Run(() => GetSingle(@where));
        }

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.CreateAt = DateTime.UtcNow;
                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                {
                    entity.CreateAt = DateTime.UtcNow;
                    Entities.Add(entity);
                }

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual async Task InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.CreateAt = DateTime.UtcNow;
                Entities.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual async Task InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                {
                    entity.CreateAt = DateTime.UtcNow;
                    Entities.Add(entity);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Update(T entity, params Expression<Func<T, PropertyInfo>>[] fields)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> update)
        {
            try
            {
                if (where == null)
                    throw new ArgumentNullException("where");
                if (update == null)
                    throw new ArgumentNullException("update");

                Entities.Where(where).Update(update);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual Task UpdateAsync(T entity, params Expression<Func<T, PropertyInfo>>[] fields)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> update)
        {
            try
            {
                if (where == null)
                    throw new ArgumentNullException("where");
                if (update == null)
                    throw new ArgumentNullException("update");

                return Entities.Where(where).UpdateAsync(update);
                //return _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual async Task UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.IsDeleted = true;
                this.Update(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    entity.IsDeleted = true;

                this.Update(entities);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.IsDeleted = true;
                await this.UpdateAsync(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual async Task DeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    entity.IsDeleted = true;

                await this.UpdateAsync(entities);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        public virtual void ExecuteDbTran(Action execute)
        {
            using (var tran = ((DbContext)_context).Database.BeginTransaction())
            {
                try
                {
                    execute.Invoke();
                    ((DbContext)_context).SaveChanges();
                    tran.Commit();
                }
                catch (DbEntityValidationException dbEx)
                {
                    tran.Rollback();
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }

        public virtual void ExecuteRequiredTran(Action execute)
        {
            using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    execute();
                    ((DbContext)_context).SaveChanges();
                    tran.Complete();
                }
                catch (DbEntityValidationException dbEx)
                {
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 获取所有错误
        /// </summary>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("属性: {0} 错误: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        #endregion

    }

}
