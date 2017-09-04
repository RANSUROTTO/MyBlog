using System;
using System.Linq;
using System.Data.Entity;
using Blog.Libraries.Core.Data;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace Blog.Libraries.Data.Context
{

    /// <summary>
    /// 针对数据库持久化上下文的扩展类
    /// </summary>
    public static class DbContextExtensions
    {

        #region Methods

        /// <summary>
        /// 加载实体对象原始值副本
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="context">上下文对象</param>
        /// <param name="currentCopy">需要复制的实体</param>
        /// <returns></returns>
        public static T LoadOriginalCopy<T>(this IDbContext context, T currentCopy) where T : BaseEntity
        {
            return InnerGetCopy(context, currentCopy, e => e.OriginalValues);
        }

        /// <summary>
        /// 加载实体对象当前值副本
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="context">上下文对象</param>
        /// <param name="currentCopy">需要复制的实体</param>
        /// <returns></returns>
        public static T LoadDatabaseCopy<T>(this IDbContext context, T currentCopy) where T : BaseEntity
        {
            return InnerGetCopy(context, currentCopy, e => e.GetDatabaseValues());
        }

        /// <summary>
        /// 删除数据库表格
        /// </summary>
        /// <param name="context">上下文对象</param>
        /// <param name="tableName">表格名称</param>
        public static void DropTable(this DbContext context, string tableName)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");

            //先查询是否存在表,存在则删除
            if (context.Database.SqlQuery<int>("SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = {0}", tableName).Any())
            {
                var dbScript = "DROP TABLE [" + tableName + "]";
                context.Database.ExecuteSqlCommand(dbScript);
            }
            context.SaveChanges();
        }

        /// <summary>
        /// 通过实体类型来获取其在数据库的表名称
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="context">上下文对象</param>
        /// <returns>表格名称</returns>
        public static string GetTableName<T>(this IDbContext context) where T : BaseEntity
        {
            //var tableName = typeof(T).Name;
            //return tableName;

            //此代码仅适用于Entity Framework
            //如果要支持其他ORM框架,请使用上面的代码（已注释）

            var adapter = ((IObjectContextAdapter)context).ObjectContext;
            var storageModel = (StoreItemCollection)adapter.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
            var containers = storageModel.GetItems<EntityContainer>();
            var entitySetBase = containers.SelectMany(c => c.BaseEntitySets.Where(bes => bes.Name == typeof(T).Name)).First();

            //表名称
            string tableName = entitySetBase.MetadataProperties.First(p => p.Name == "Table").Value.ToString();
            //模式名称
            //string schemaName = productEntitySetBase.MetadataProperties.First(p => p.Name == "Schema").Value.ToString();
            return tableName;
        }

        /// <summary>
        /// 获取某一数据列的最大长度
        /// </summary>
        /// <param name="context">上下文对象</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>最大长度.如没有限制最大长度,则返回null</returns>
        public static int? GetColumnMaxLength(this IDbContext context, string entityTypeName, string columnName)
        {
            var rez = GetColumnsMaxLength(context, entityTypeName, columnName);
            return rez.ContainsKey(columnName) ? rez[columnName] as int? : null;
        }

        /// <summary>
        /// 获取数据列的最大长度
        /// </summary>
        /// <param name="context">上下文对象</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="columnNames">列名称</param>
        public static IDictionary<string, int> GetColumnsMaxLength(this IDbContext context, string entityTypeName, params string[] columnNames)
        {
            int temp;

            var fildFacets = GetFildFacets(context, entityTypeName, "String", columnNames);

            var queryResult = fildFacets
                .Select(f => new { Name = f.Key, MaxLength = f.Value["MaxLength"].Value })
                .Where(p => int.TryParse(p.MaxLength.ToString(), out temp))
                .ToDictionary(p => p.Name, p => Convert.ToInt32(p.MaxLength));

            return queryResult;
        }

        /// <summary>
        /// 获取最大十进制值
        /// </summary>
        /// <param name="context">上下文对象</param>
        /// <param name="entityTypeName">实体类型名称</param>
        /// <param name="columnNames">列名称</param>
        public static IDictionary<string, decimal> GetDecimalMaxValue(this IDbContext context, string entityTypeName, params string[] columnNames)
        {
            var fildFacets = GetFildFacets(context, entityTypeName, "Decimal", columnNames);

            return fildFacets.ToDictionary(p => p.Key, p => int.Parse(p.Value["Precision"].Value.ToString()) - int.Parse(p.Value["Scale"].Value.ToString()))
                .ToDictionary(p => p.Key, p => new decimal(Math.Pow(10, p.Value)));
        }

        private static Dictionary<string, ReadOnlyMetadataCollection<Facet>> GetFildFacets(this IDbContext context,
            string entityTypeName, string edmTypeName, params string[] columnNames)
        {
            var entType = Type.GetType(entityTypeName);
            var adapter = ((IObjectContextAdapter)context).ObjectContext;
            var metadataWorkspace = adapter.MetadataWorkspace;
            var q = from meta in metadataWorkspace.GetItems(DataSpace.CSpace).Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                    from p in (meta as EntityType).Properties.Where(p => columnNames.Contains(p.Name) && p.TypeUsage.EdmType.Name == edmTypeName)
                    select p;

            var queryResult = q.Where(p =>
            {
                var match = p.DeclaringType.Name == entityTypeName;
                if (!match && entType != null)
                {
                    match = entType.Name == p.DeclaringType.Name;
                }

                return match;

            }).ToDictionary(p => p.Name, p => p.TypeUsage.Facets);

            return queryResult;
        }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <param name="context">上下文对象</param>
        /// <returns>数据库名称</returns>
        public static string DbName(this IDbContext context)
        {
            var connection = ((IObjectContextAdapter)context).ObjectContext.Connection as EntityConnection;
            if (connection == null)
                return string.Empty;

            return connection.StoreConnection.Database;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 复制对象内部属性
        /// </summary>
        private static T InnerGetCopy<T>(IDbContext context, T currentCopy, Func<DbEntityEntry<T>, DbPropertyValues> func) where T : BaseEntity
        {
            //获取数据库上下文
            DbContext dbContext = CastOrThrow(context);

            //获取实体跟踪对象
            DbEntityEntry<T> entry = GetEntityOrReturnNull(currentCopy, dbContext);

            //创建输出对象
            T output = null;

            //尝试并获取值
            if (entry != null)
            {
                DbPropertyValues dbPropertyValues = func(entry);
                if (dbPropertyValues != null)
                {
                    output = dbPropertyValues.ToObject() as T;
                }
            }

            return output;
        }

        /// <summary>
        /// 获取实体跟踪对象或返回null
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="currentCopy">实体对象</param>
        /// <param name="dbContext">数据库持久上下文</param>
        /// <returns>实体跟踪对象</returns>
        private static DbEntityEntry<T> GetEntityOrReturnNull<T>(T currentCopy, DbContext dbContext) where T : BaseEntity
        {
            return dbContext.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity == currentCopy);
        }

        /// <summary>
        /// 转换IDbContext对象为DbContext对象
        /// 失败将抛出异常
        /// </summary>
        /// <param name="context">IDbContext对象</param>
        /// <returns>DbContext对象</returns>
        private static DbContext CastOrThrow(IDbContext context)
        {
            var output = context as DbContext;

            if (output == null)
            {
                throw new InvalidOperationException("Context does not support operation.");
            }

            return output;
        }

        #endregion

    }

}
