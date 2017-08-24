using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Provider
{

    public class SqlCeDataProvider : IDataProvider
    {

        #region Properties

        /// <summary>
        /// SqlCe数据库不支持存储过程
        /// </summary>
        public virtual bool StoredProceduredSupported
        {
            get { return false; }
        }

        /// <summary>
        /// SqlCe不支持备份
        /// </summary>
        public virtual bool BackupSupported
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化SqlCe数据库连接工厂
        /// </summary>
        public virtual void InitConnectionFactory()
        {
            var connectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
#pragma warning disable 618
            Database.DefaultConnectionFactory = connectionFactory;
#pragma warning restore 618
        }

        public virtual void SetDatabaseInitializer()
        {
        }

        /// <summary>
        /// 初始化SqlCe数据库
        /// </summary>
        public virtual void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }

        public virtual int SupportedLengthOfBinaryHash()
        {
            return 0;
        }

        #endregion

    }

}
