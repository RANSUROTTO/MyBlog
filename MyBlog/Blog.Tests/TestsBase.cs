using System.Security.Principal;
using NUnit.Framework;
using Rhino.Mocks;

namespace Blog.Tests
{
    public abstract class TestsBase
    {

        protected MockRepository mocks;

        /// <summary>
        /// 代表每个测试方法前先调用该方法实例化RhinoMocks对象
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            mocks = new MockRepository();
        }

        /// <summary>
        /// 代表每个测试方法结束后调用该拆卸方法
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            if (mocks != null)
            {
                mocks.ReplayAll();
                mocks.VerifyAll();
            }
        }

        protected static IPrincipal CreatePrincipal(string name, params string[] roles)
        {
            return new GenericPrincipal(new GenericIdentity(name, "TestIdentity"), roles);
        }

    }
}
