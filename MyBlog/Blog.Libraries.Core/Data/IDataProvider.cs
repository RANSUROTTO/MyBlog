using System.Data.Common;

namespace Blog.Libraries.Core.Data
{

    /// <summary>
    /// 数据提供者接口
    /// </summary>
    public interface IDataProvider
    {

        /// <summary>
        /// 初始化连接工厂
        /// </summary>
        void InitConnectionFactory();

        /// <summary>
        /// 设置数据库初始化
        /// </summary>
        void SetDatabaseInitializer();

        /// <summary>
        /// 初始化数据库
        /// </summary>
        void InitDatabase();

        /// <summary>
        /// 存储一个值,该值标识该数据提供者是否支持存储过程
        /// </summary>
        bool StoredProceduredSupported { get; }

        /// <summary>
        /// 存储一个值,该值标识该数据提供者是否支持备份
        /// </summary>
        bool BackupSupported { get; }

        /// <summary>
        /// 获取数据库支持的参数对象(用于存储过程)
        /// </summary>
        /// <returns>Parameter</returns>
        DbParameter GetParameter();

        /// <summary>
        /// 支持的哈希散列数据最大长度
        /// 不支持则返回0
        /// </summary>
        /// <returns></returns>
        int SupportedLengthOfBinaryHash();

    }

}
