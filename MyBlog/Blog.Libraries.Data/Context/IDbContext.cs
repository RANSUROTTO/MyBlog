using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;
using System.Data.Entity.Infrastructure;

namespace Blog.Libraries.Data.Context
{

    /// <summary>
    /// 数据库上下文接口
    /// </summary>
    public interface IDbContext
    {

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>数据集</returns>
        IDbSet<T> Set<T>() where T : BaseEntity;

        /// <summary>
        /// 保存更改
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// 保存更改 (异步)
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="commandText">命令脚本(存储过程名称)</param>
        /// <param name="parameters">参数</param>
        /// <returns>实体集合</returns>
        IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters)
            where T : BaseEntity, new();

        /// <summary>
        /// 执行Sql语句查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">参数</param>
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);

        /// <summary>
        /// 执行Sql命令
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="doNotEnsureTransaction">非事务保证</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="parameters">参数</param>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        /// <summary>
        /// 设置实体在上下文的状态
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">待设置状态的实体</param>
        /// <param name="state">目标状态</param>
        void SetEntityState<T>(T entity, EntityState state) where T : BaseEntity;

        /// <summary>
        /// 获得实体状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        DbEntityEntry Entry<T>(T entity) where T : BaseEntity;

    }

}
