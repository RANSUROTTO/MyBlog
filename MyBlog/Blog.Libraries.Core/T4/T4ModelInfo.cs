using System;

namespace Blog.Libraries.Core.T4
{

    /// <summary>
    /// T4封装类模型
    /// </summary>
    public class T4ModelInfo
    {

        #region Proerties

        /// <summary>
        /// 获取或设置T4模型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置T4模型所封装类型
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// 获取或设置T4模型所在模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 获取或设置是否指定生成模块名称
        /// </summary>
        public bool UseModule { get; set; }

        #endregion

        #region Constructor

        public T4ModelInfo(Type modelType, bool useModule = false)
        {
            ModelType = modelType;
            UseModule = useModule;
            Name = ModelType.Name;

            if (UseModule)
            {
                var modelTypeNamespace = ModelType.Namespace;
                var index = modelTypeNamespace.LastIndexOf('.') + 1;
                ModuleName = modelTypeNamespace.Substring(index, modelTypeNamespace.Length - index);
            }
        }

        #endregion

        #region Methods

        public static T4ModelInfo Create(Type modelType, bool useModule = false)
        {
            return new T4ModelInfo(modelType, useModule);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(ModelType)}: {ModelType}, {nameof(ModuleName)}: {ModuleName}, {nameof(UseModule)}: {UseModule}";
        }

        #endregion

    }

}
