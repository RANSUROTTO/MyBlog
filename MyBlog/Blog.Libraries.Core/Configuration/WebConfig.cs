using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Blog.Libraries.Core.Configuration
{
    public class WebConfig : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new WebConfig();

            //Initialization properties


            return config;
        }

        #region Properties



        #endregion

        #region Utilities

        /// <summary>
        /// 获取节点属性值 转字符串
        /// </summary>
        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement<string>(node, attrName, Convert.ToString);
        }

        /// <summary>
        /// 获取节点属性值 转布尔
        /// </summary>
        private bool GetBool(XmlNode node, string attrName)
        {
            return SetByXElement<bool>(node, attrName, Convert.ToBoolean);
        }

        /// <summary>
        /// 获取节点属性值 通过自定义转换函数转为指定类型
        /// </summary>
        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node?.Attributes == null) return default(T);
            var attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            var attrVal = attr.Value;
            return converter(attrVal);
        }

        #endregion

    }
}
