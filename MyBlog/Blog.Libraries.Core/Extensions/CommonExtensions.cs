
namespace Blog.Libraries.Core.Extensions
{

    /// <summary>
    /// 一个常用的扩展类
    /// </summary>
    public static class CommonExtensions
    {

        /// <summary>
        /// 可空类型对象为空或者为默认值
        /// </summary>
        public static bool IsNullOrDefault<T>(this T? value) where T : struct
        {
            return default(T).Equals(value.GetValueOrDefault());
        }

    }

}
