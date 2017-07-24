using System;
using Blog.Libraries.Core.Infrastructure;
using NUnit.Framework;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests.Infrastructure
{

    [TestFixture]
    public class SingletonTests
    {

        /// <summary>
        /// 断言两个不同静态泛型类的非派生成员引用和值不相等
        /// </summary>
        [Test]
        public void Singleton_NotEqual_Generice()
        {
            Singleton<int>.Instance = 1;
            Singleton<double>.Instance = 2.0;

            typeof(Singleton<int>).TestNotEqual(typeof(Singleton<double>));
            typeof(Singleton<int>).TestBeNotBeTheSameAs(typeof(Singleton<double>));
            Singleton<int>.Instance.TestNotEqual(Singleton<double>.Instance);
            Singleton<int>.Instance.TestBeNotBeTheSameAs(Singleton<double>.Instance);
        }

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

        /// <summary>
        /// 断言通过单例存储基类获取未赋值的集合单例不为null 为new List
        /// </summary>
        [Test]
        public void SingletonList_IsCreatedByDefault()
        {
            var instance = SingletonList<SingletonTests>.Instance;
            instance.TestIsNotNull("SingletonList<SingletonTests>.Instance was null");
        }

        /// <summary>
        /// 断言单例存储基类存储的集合单例可以存储东西
        /// </summary>
        [Test]
        public void SingletonList_CanStoreStuff()
        {
            var instance = SingletonList<SingletonTests>.Instance;
            instance.Insert(0, this);

            instance.Count.TestNotEqual(0, "length of instance was 0");
            instance[0].TestBeTheSameAs(this);
        }

    }

}
