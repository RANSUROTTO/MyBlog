using System;
using System.Collections.Generic;

namespace Blog.Libraries.Core.Infrastructure
{
    /// <summary>
    /// 提供存储所有单例对象的基类
    /// </summary>
    public class Singleton
    {

        /// <summary>
        /// 存储单例实例的字典
        /// </summary>
        public static IDictionary<Type, object> AllSingletons { get; }

        static Singleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

    }

    /// <summary>
    /// 该Singleton中包含一个T的实例，并存入类实例字典中
    /// 提供某种类型的单例对象
    /// </summary>
    /// <typeparam name="T">存储的对象的类型</typeparam>
    /// <remarks>访问实例不同步</remarks>
    public class Singleton<T> : Singleton
    {

        private static T _instance;

        /// <summary>
        /// 指定泛型的单个实例。每个对象只有一个实例
        /// </summary>
        public static T Instance
        {
            get { return _instance; }
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }

    }

    /// <summary>
    /// 该Singleton中包含一个Singleton(IList(T))实例，并存入类实例字典中
    /// 提供某种类型集合的单例
    /// </summary>
    /// <typeparam name="T">要存储的集合的类型</typeparam>
    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        public new static IList<T> Instance
        {
            get { return Singleton<IList<T>>.Instance; }
        }
    }

    /// <summary>
    /// 该Singleton中包含一个Singleton(Dictionary(TKey,TValue))实例，并存入类实例字典中
    /// 提供对应类型键值对字典的单例
    /// </summary>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        public new static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<Dictionary<TKey, TValue>>.Instance; }
        }
    }

}
