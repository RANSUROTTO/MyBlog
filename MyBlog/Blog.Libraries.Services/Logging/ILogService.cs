using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Data.Domain.Logging;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Services.Logging
{

    public interface ILogService
    {

        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="log">日志</param>
        void InsertLog(Log log);

        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="shortMessage">简略信息</param>
        /// <param name="fullMessage">完整信息</param>
        /// <param name="customer">激发此日志记录的用户</param>
        void InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", Customer customer = null);

        /// <summary>
        /// 通过ID获取日志
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>日志</returns>
        Log GetLogById(long logId);

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="log">日志</param>
        void DeleteLog(Log log);

        /// <summary>
        /// 删除多条日志
        /// </summary>
        /// <param name="logs">日志列表</param>
        void DeleteLog(IList<Log> logs);

        /// <summary>
        /// 清空日志
        /// </summary>
        void ClearLog();

    }

}
