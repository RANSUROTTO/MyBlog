using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Data.Domain.Logging;

namespace Blog.Libraries.Data.Tests
{

    public static class PersistenceTestHelper
    {

        #region Logging

        public static Log GetTestLog(this PersistenceTest test)
        {
            return new Log
            {
                LogLevel = LogLevel.Error,
                ShortMessage = "ShortMessage",
                FullMessage = "FullMessage",
                IpAddress = "127.0.0.1",
                CreateAt = DateTime.UtcNow,
                Guid = new Guid(),
                PageUrl = "http://127.0.0.1/url",
                ReferrerUrl = "http://127.0.0.1/referrerUrl"
            };
        }

        #endregion


    }

}
