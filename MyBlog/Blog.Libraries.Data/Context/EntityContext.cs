using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Blog.Libraries.Data.Mapping;

namespace Blog.Libraries.Data.Context
{

    /// <summary>
    /// 实体上下文实例
    /// </summary>
    /*使用Sqlce在单元测试下需要注释这行信息*/
    /*[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]*/
    public class EntityContext : DbObjectContext
    {

        public EntityContext()
            //Default Local Data Base Connection String
            : base("name=mySqlLocalConnection") { }

        public EntityContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(CustomEntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

    }

}
