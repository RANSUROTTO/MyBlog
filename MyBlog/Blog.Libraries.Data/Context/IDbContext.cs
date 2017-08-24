using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;

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


        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);

        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

    }

}
