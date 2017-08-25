using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Context
{

    /// <summary>
    /// 数据上下文
    /// </summary>
    public class DbObjectContext : DbContext, IDbContext
    {

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public DbObjectContext(string nameOrConnectionString) : base(nameOrConnectionString)
        { }

        #endregion

        #region Methods

        public new IDbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters) where T : BaseEntity, new()
        {
            //添加参数至命令
            if (parameters != null && parameters.Any())
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("不支持的参数类型");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //输出参数
                        commandText += " output";
                    }
                }
            }

            //执行查询
            var result = base.Database.SqlQuery<T>(commandText, parameters).ToList();

            return result;
        }

        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return base.Database.SqlQuery<T>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;

            if (timeout.HasValue)
            {
                //存储之前的超时时间
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;

            var result = base.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //更新为设置前的超时时间
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            return result;
        }

        public void SetEntityState<T>(T entity, EntityState state) where T : BaseEntity
        {
            base.Entry(entity).State = state;
        }

        public new DbEntityEntry Entry<T>(T entity) where T : BaseEntity
        {
            return base.Entry(entity);
        }

        #endregion

    }

}
