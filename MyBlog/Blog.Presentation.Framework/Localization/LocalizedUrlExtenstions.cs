using System;
using Blog.Libraries.Core.Domain.Localization;

namespace Blog.Presentation.Framework.Localization
{

    public static class LocalizedUrlExtenstions
    {

        #region Fields

        private static int _seoCodeLength = 2;

        #endregion

        /// <summary>
        /// 获取一个值,该值指示应用程序是否运行在虚拟目录
        /// </summary>
        /// <param name="applicationPath">应用路径</param>
        /// <returns>结果</returns>
        private static bool IsVirtualDirectory(this string applicationPath)
        {
            if (string.IsNullOrEmpty(applicationPath))
                throw new ArgumentException("未指定应用程序路径");

            return applicationPath != "/";
        }

        /// <summary>
        /// 从原始URL删除应用程序路径
        /// </summary>
        /// <param name="rawUrl">原始url</param>
        /// <param name="applicationPath">应用路径</param>
        /// <returns>结果</returns>
        public static string RemoveApplicationPathFromRawUrl(this string rawUrl, string applicationPath)
        {
            if (string.IsNullOrEmpty(applicationPath))
                throw new ArgumentException("未指定应用程序路径");

            if (rawUrl.Length == applicationPath.Length)
                return "/";

            var result = rawUrl.Substring(applicationPath.Length);
            //用 '/' 开头
            if (!result.StartsWith("/"))
                result = "/" + result;
            return result;
        }

        /// <summary>
        /// 从URL获取语言搜索引擎优化代码
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="applicationPath">应用路径</param>
        /// <param name="isRawPath">一个值，该值指示是否已传递原始URL</param>
        /// <returns>结果</returns>
        public static string GetLanguageSeoCodeFromUrl(this string url, string applicationPath, bool isRawPath)
        {
            if (isRawPath)
            {
                if (applicationPath.IsVirtualDirectory())
                {
                    url = url.RemoveApplicationPathFromRawUrl(applicationPath);
                }

                return url.Substring(1, _seoCodeLength);
            }

            return url.Substring(2, _seoCodeLength);
        }

        /// <summary>
        /// 获取一个值，该值指示URL是否本地化（包含搜索引擎优化代码）
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="applicationPath">应用路径</param>
        /// <param name="isRawPath">一个值，该值指示是否已传递原始URL</param>
        /// <returns>结果</returns>
        public static bool IsLocalizedUrl(this string url, string applicationPath, bool isRawPath)
        {
            if (string.IsNullOrEmpty(url))
                return false;
            if (isRawPath)
            {
                if (applicationPath.IsVirtualDirectory())
                {
                    url = url.RemoveApplicationPathFromRawUrl(applicationPath);
                }

                int length = url.Length;
                //too short url
                if (length < 1 + _seoCodeLength)
                    return false;

                //url like "/en"
                if (length == 1 + _seoCodeLength)
                    return true;

                //urls like "/en/" or "/en/somethingelse"
                return (length > 1 + _seoCodeLength) && (url[1 + _seoCodeLength] == '/');
            }
            else
            {
                int length = url.Length;
                //too short url
                if (length < 2 + _seoCodeLength)
                    return false;

                //url like "/en"
                if (length == 2 + _seoCodeLength)
                    return true;

                //urls like "/en/" or "/en/somethingelse"
                return (length > 2 + _seoCodeLength) && (url[2 + _seoCodeLength] == '/');
            }
        }

        /// <summary>
        /// 从URL中删除语言搜索引擎优化代码
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="applicationPath">应用路径</param>
        /// <returns>结果</returns>
        public static string RemoveLanguageSeoCodeFromRawUrl(this string url, string applicationPath)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            string result = null;
            if (applicationPath.IsVirtualDirectory())
            {
                url = url.RemoveApplicationPathFromRawUrl(applicationPath);
            }

            int length = url.Length;
            if (length < _seoCodeLength + 1)    //too short url
                result = url;
            else if (length == 1 + _seoCodeLength)  //url like "/en"
                result = url.Substring(0, 1);
            else
                result = url.Substring(_seoCodeLength + 1); //urls like "/en/" or "/en/somethingelse"

            if (applicationPath.IsVirtualDirectory())
                result = applicationPath + result;  //add back application path
            return result;
        }

        /// <summary>
        /// 从URL添加语言搜索引擎优化代码
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="applicationPath">应用路径</param>
        /// <param name="language">语言</param>
        /// <returns>结果</returns>
        public static string AddLanguageSeoCodeToRawUrl(this string url, string applicationPath,
            Language language)
        {
            if (language == null)
                throw new ArgumentNullException("language");

            int startIndex = 0;
            if (applicationPath.IsVirtualDirectory())
            {
                startIndex = applicationPath.Length;
            }

            //添加seo代码
            url = url.Insert(startIndex, language.UniqueSeoCode);
            url = url.Insert(startIndex, "/");

            return url;
        }

    }

}
