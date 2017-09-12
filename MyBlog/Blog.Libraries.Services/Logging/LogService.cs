using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Context;
using Blog.Libraries.Data.Domain.Logging;

namespace Blog.Libraries.Services.Logging
{
    public class LogService : ILogService
    {

        #region Fields

        private readonly IRepository<Log> _logRepository;
        private readonly IDbContext _dbContext;

        #endregion

        #region Constructor

        public LogService(IRepository<Log> logRepository,
            IDbContext dbContext)
        {
            _logRepository = logRepository;
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public void InsertLog(Log log)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            _logRepository.Insert(log);
        }

        public Log GetLogById(long id)
        {
            if (id == default(long))
                return null;

            return _logRepository.GetById(id);
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
