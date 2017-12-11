using System;

namespace Blog.Libraries.Core.Infrastructure.TypeFinder
{
    public static class TypeFinderExtensions
    {

        /// <summary>
        /// 通过类的全名在运行程序的程序集列表中查找类型
        /// </summary>
        /// <param name="typeFinder">类型查找器对象</param>
        /// <param name="classFullName">类的完全名称（包含命名空间）</param>
        /// <returns>类型</returns>
        public static Type GetType(this ITypeFinder typeFinder, string classFullName)
        {
            var assemblies = typeFinder.GetAssemblies();
            Type type = null;
            foreach (var assembly in assemblies)
            {
                type = assembly.GetType(classFullName);
                if (type != null) break;
            }
            return type;
        }

    }
}
