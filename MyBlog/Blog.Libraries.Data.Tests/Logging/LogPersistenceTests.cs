using NUnit.Framework;
using Blog.Tests;

namespace Blog.Libraries.Data.Tests.Logging
{

    [TestFixture]
    public class LogPersistenceTests : PersistenceTest
    {

        [Test]
        public void Passes_AddandLoad_Log_Success()
        {
            var log = this.GetTestLog();

            var fromDb = SaveAndLoadEntity(this.GetTestLog());
            fromDb.TestIsNotNull();
            fromDb.TestPropertiesEqual(log);
        }



    }

}
