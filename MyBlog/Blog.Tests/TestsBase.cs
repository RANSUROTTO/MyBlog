using System.Security.Principal;
using NUnit.Framework;
using Rhino.Mocks;

namespace Blog.Tests
{
    public abstract class TestsBase
    {

        /// <summary>
        /// MockRepository实例 Rhino Mocks:模拟对象创建管理的框架
        /// </summary>
        protected MockRepository Mocks;

        /// <summary>
        /// 代表每个测试方法前先调用该方法实例化RhinoMocks对象
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            Mocks = new MockRepository();
        }

        /// <summary>
        /// 代表每个测试方法结束后调用该拆卸方法
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            if (Mocks != null)
            {
                Mocks.ReplayAll();
                Mocks.VerifyAll();
                Mocks = null;
            }
        }

        protected static IPrincipal CreatePrincipal(string name, params string[] roles)
        {
            return new GenericPrincipal(new GenericIdentity(name, "TestIdentity"), roles);
        }

    }
}
