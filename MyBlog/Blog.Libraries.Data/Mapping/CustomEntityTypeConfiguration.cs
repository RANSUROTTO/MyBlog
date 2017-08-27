using System.Data.Entity.ModelConfiguration;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Mapping
{

    public abstract class CustomEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {

        protected CustomEntityTypeConfiguration()
        {
            this.Initialize();
        }

        /// <summary>
        /// 可以重写这一个方法
        /// 实现通过构造函数执行该自定义初始化代码
        /// </summary>
        protected virtual void Initialize()
        { }

    }

}
