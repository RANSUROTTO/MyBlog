using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Infrastructure;
using NUnit.Framework;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests.Infrastructure
{
    [TestFixture]
    public class SingletonTests
    {

        /// <summary>
        /// 断言通过单例存储基类获取未赋值的单例值为null
        /// </summary>
        [Test]
        public void Singleton_IsNullByDefault()
        {
            var instance = Singleton<SingletonTests>.Instance;
            instance.TestIsNull(string.Format("instance was not null"));
        }

        /// <summary>
        /// 断言单例存储类基类存储的单例对象所在的单例字典是互相共享的
        /// </summary>
        [Test]
        public void Singleton_ShareSame_SingletonDictionary()
        {
            Singleton<int>.Instance = 1;
            Singleton<double>.Instance = 2.0d;

            Singleton<int>.AllSingletons.TestBeTheSameAs(Singleton<double>.AllSingletons);
            Singleton<int>.Instance.TestEqual(1, "Singleton<int>.Instance was not equal 1");
            Singleton<double>.Instance.TestEqual(2.0d, "Singleton<int>.Instance was not equal 1");
        }

        /// <summary>
        /// 断言通过单例存储基类获取未赋值的字典单例不为null 为new Dictionary
        /// </summary>
        [Test]
        public void SingletonDictionary_IsCreatedByDefault()
        {
            var instance = SingletonDictionary<SingletonTests, object>.Instance;
            instance.TestIsNotNull("SingletonDictionary<SingletonTests, object>.Instance was null");
        }

        /// <summary>
        /// 断言通过单例存储基类获取未赋值的字典单例长度为0
        /// </summary>
        [Test]
        public void SingletonDictionary_IsCreateByDefault_LengthEqual0()
        {
            var instance = SingletonDictionary<SingletonTests, object>.Instance;
            instance.Count.TestEqual(0, "instance length was not 0");
        }

        /// <summary>
        /// 断言单例存储基类存储的字典单例可以存储东西
        /// </summary>
        [Test]
        public void SingletonDictionary_CanStoreStuff()
        {
            var instance = SingletonDictionary<Type, object>.Instance;
            instance[typeof(SingletonTests)] = this;

            instance.Count.TestNotEqual(0, "length of instance was 0");
            instance[typeof(SingletonTests)].TestBeTheSameAs(this);
        }





    }
}
