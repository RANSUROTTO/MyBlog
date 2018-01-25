using Blog.Libraries.Data.Domain.Logging;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Logging
{
	public partial interface ILogService : IService<Log> {}

    public partial class LogService : Service<Log>,ILogService 
    {
        
        #region Constructor

        public LogService (IRepository<Log> repository) : base(repository) {}
        
        #endregion

    }

}
