using System.Data.Common;
using System.Data.Entity.Infrastructure;
using Blog.Libraries.Core.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Blog.Libraries.Data.Provider
{

    /// <summary>
    /// SqlServer数据提供者
    /// </summary>
    public class SqlServerDataProvider : IDataProvider
    {

        /// <summary>
        /// 初始化数据库连接创建工厂
        /// </summary>
        public virtual void InitConnectionFactory()
        {
            var connectionFactory = new SqlConnectionFactory();
#pragma warning disable 618
            Database.DefaultConnectionFactory = connectionFactory;
#pragma warning restore 618
        }

        /// <summary>
        /// 初始化数据库设置
        /// </summary>
        public virtual void SetDatabaseInitializer()
        {

        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        // ReSharper disable once FunctionRecursiveOnAllPaths
        public virtual void InitDatabase()
        {
            InitDatabase();
            SetDatabaseInitializer();
        }

        /// <summary>
        /// SqlServer支持存储过程
        /// </summary>
        public virtual bool StoredProceduredSupported
        {
            get { return true; }
        }

        /// <summary>
        /// SqlServer支持备份
        /// </summary>
        public virtual bool BackupSupported
        {
            get { return true; }
        }

        /// <summary>
        /// 获取支持数据库参数对象 (用于存储过程)
        /// </summary>
        /// <returns>参数对象</returns>
        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }

        public virtual int SupportedLengthOfBinaryHash()
        {
            return 800;
        }

    }

}
