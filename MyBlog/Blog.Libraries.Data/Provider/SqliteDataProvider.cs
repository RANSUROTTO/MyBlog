using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Provider
{

    public class SqliteDataProvider : IDataProvider
    {
        public void InitConnectionFactory()
        {
            throw new NotImplementedException();
        }

        public void SetDatabaseInitializer()
        {
            throw new NotImplementedException();
        }

        public void InitDatabase()
        {
            throw new NotImplementedException();
        }

        public bool StoredProceduredSupported { get; }
        public bool BackupSupported { get; }
        public DbParameter GetParameter()
        {
            throw new NotImplementedException();
        }

        public int SupportedLengthOfBinaryHash()
        {
            throw new NotImplementedException();
        }
    }

}
