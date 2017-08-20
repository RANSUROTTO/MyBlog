
namespace Blog.Libraries.Core.Extensions
{

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
