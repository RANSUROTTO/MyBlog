using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Libraries.Core.Common
{

    /// <summary>
    /// 表示在应用程序执行期间发生的异常
    /// </summary>
    public class SiteException : Exception
    {

        public SiteException()
        { }

        public SiteException(string message)
            : base(message)
        {

        }

        public SiteException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {

        }

        public SiteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SiteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }

}
