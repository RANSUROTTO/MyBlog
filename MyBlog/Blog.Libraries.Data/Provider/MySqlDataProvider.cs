using System.Data.Common;
using Blog.Libraries.Core.Data;
using System.Data.Entity;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace Blog.Libraries.Data.Provider
{

    public class MySqlDataProvider : IDataProvider
    {

        #region Properties

        /// <summary>
        /// MySql支持存储过程
        /// </summary>
        public virtual bool StoredProceduredSupported
        {
            get { return true; }
        }

        /// <summary>
        /// MySql支持备份
        /// </summary>
        public virtual bool BackupSupported
        {
            get { return true; }
        }

        public virtual DbParameter GetParameter()
        {
            return new MySqlParameter();
        }

        public virtual int SupportedLengthOfBinaryHash()
        {
            return 0;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化数据库连接工厂
        /// </summary>
        public virtual void InitConnectionFactory()
        {
            var connectionFactory = new MySqlConnectionFactory();
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
        public virtual void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

        #endregion

    }

}
