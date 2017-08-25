using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Context;

namespace Blog.Libraries.Data.Repository
{

    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {

        #region Field

        private IDbContext _context;

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
                return Entities;
            }
        }

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

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual Task<T> GetByIdAsync(object id)
        {
            var result = Task.Run(() =>
            {
                return Entities.Find(id);
            });
            return result;
        }

        public virtual void InsertAsync(T entity)
        {
            Entities.Add(entity);
            _context.SaveChangesAsync();
        }

        public virtual void InsertAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Utilities

        #endregion

    }

}
