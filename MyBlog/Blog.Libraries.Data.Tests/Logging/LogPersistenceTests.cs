using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Blog.Tests;

namespace Blog.Libraries.Data.Tests.Logging
{

    [TestFixture]
    public class LogPersistenceTests : PersistenceTest
    {

        [Test]
        public void Can_save_and_load_log()
        {
            var log = this.GetTestLog();

            var fromDb = SaveAndLoadEntity(this.GetTestLog());
            fromDb.TestIsNotNull();
            fromDb.TestBeNotBeTheSameAs(log);
        }

    }

}
