using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blog.Libraries.Core.Infrastructure.TypeFinder
{
    /// <summary>
    /// 类型查找器接口
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>  
        /// 获取程序集列表  
        /// </summary>  
        /// <returns>程序集列表</returns>  
        IList<Assembly> GetAssemblies();

        /// <summary>  
        /// 获取一个集合，该集合包含派生assignTypeFrom类型的类
        /// </summary>  
        /// <param name="assignTypeFrom">指定父类</param>  
        /// <param name="onlyConcreteClasses">是否只查找具体类</param>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        /// <summary>  
        /// 获取一个集合,该集合包含从assemblies程序集中派生自assignTypeFrom类的类
        /// </summary>  
        /// <param name="assignTypeFrom">指定父类</param>  
        /// <param name="assemblies">指定被查找的程序集集合</param>  
        /// <param name="onlyConcreteClasses">是否只查找具体类</param>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        /// <summary>  
        /// 获取派生自T类的类集合  
        /// </summary>
        /// <typeparam name="T">指定父类</typeparam>  
        /// <param name="onlyConcreteClasses">是否只查找具体类</param>  
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        /// <summary>  
        /// 从assemblies程序集集合中获取派生自T类的类集合  
        /// </summary>  
        /// <typeparam name="T">指定父类</typeparam>  
        /// <param name="assemblies">指定被查找的程序集集合</param>  
        /// <param name="onlyConcreteClasses">是否只查找具体类</param> 
        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

    }
}
