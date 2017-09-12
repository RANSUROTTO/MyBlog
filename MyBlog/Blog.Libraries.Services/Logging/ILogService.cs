using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Data.Domain.Logging;

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
        /// 通过ID获取日志
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns>日志</returns>
        Log GetLogById(long id);

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
