using System;
using System.Collections.Generic;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Data.Context;
using Blog.Libraries.Data.Domain.Logging;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Services.Logging
{
    public class LogService : ILogService
    {

        #region Fields

        private readonly IRepository<Log> _logRepository;
        private readonly IWebHelper _webHelper;
        private readonly IDbContext _dbContext;

        #endregion

        #region Constructor

        public LogService(IRepository<Log> logRepository,
            IDbContext dbContext,
            IWebHelper webHelper)
        {
            this._logRepository = logRepository;
            this._dbContext = dbContext;
            this._webHelper = webHelper;
        }

        #endregion

        #region Methods

        public void InsertLog(Log log)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            _logRepository.Insert(log);
        }

        public void InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", Customer customer = null)
        {
            Log log = new Log
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                PageUrl = _webHelper.GetThisPageUrl(true),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
                Customer = customer
            };

            _logRepository.Insert(log);
        }

        public Log GetLogById(long logId)
        {
            if (logId == default(long))
                return null;

            return _logRepository.GetById(logId);
        }

        public void DeleteLog(Log log)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            _logRepository.Delete(log);
        }

        public void ClearLog()
        {

            throw new NotImplementedException();
        }

        public void DeleteLog(IList<Log> logs)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
