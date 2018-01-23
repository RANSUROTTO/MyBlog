using System;

namespace Blog.Libraries.Core.T4
{

    /// <summary>
    /// T4��װ��ģ��
    /// </summary>
    public class T4ModelInfo
    {

        #region Proerties

        /// <summary>
        /// ��ȡ������T4ģ������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������T4ģ������װ����
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// ��ȡ������T4ģ������ģ������
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// ��ȡ�������Ƿ�ָ������ģ������
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
