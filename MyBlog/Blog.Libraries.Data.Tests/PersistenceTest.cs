using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Context;
using NUnit.Framework;

namespace Blog.Libraries.Data.Tests
{

    /// <summary>
    /// 数据持久化测试公共类
    /// </summary>
    [TestFixture]
    public class PersistenceTest
    {

        protected EntityContext _context;

        /// <summary>
        /// 此处初始化实体上下文
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            //TODO fix compilation warning (below)
#pragma warning disable 0618
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            _context = new EntityContext(GetTestDbName());
            _context.Database.Delete();
            _context.Database.Create();
        }

        /// <summary>
        /// 获取测试连接字符串
        /// </summary>
        protected string GetTestDbName()
        {
            var testDbName = "Data Source=" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\\MyBlog.Data.Tests.Db.sdf;Persist Security Info=False";
            return testDbName;
        }

        /// <summary>
        /// 持久性测试帮助方法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="disposeContext">一个指示是否处理上下文的值</param>
        protected T SaveAndLoadEntity<T>(T entity, bool disposeContext = true) where T : BaseEntity
        {
            //添加实体至实体上下文
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            object id = entity.Id;

            if (disposeContext)
            {
                //重置实体上下文对象
                _context.Dispose();
                _context = new EntityContext(GetTestDbName());
            }

            var fromDb = _context.Set<T>().Find(id);
            return fromDb;
        }

    }

}
