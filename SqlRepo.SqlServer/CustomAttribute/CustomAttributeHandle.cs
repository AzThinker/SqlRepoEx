using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SqlRepoEx.SqlServer.CustomAttribute
{
    /// <summary>
    /// 自定义特性操作
    /// </summary>
    public static class CustomAttributeHandle
    {
        /// <summary>
        /// 是否为标识字段
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsIdField(this PropertyInfo propertyInfo)
        {

            return propertyInfo.GetCustomAttribute(typeof(IdentityFiledAttribute)) != null;

        }
        /// <summary>
        /// 检查当前字段是不为标识字段
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="idname"></param>
        /// <returns></returns>
        public static bool IsIdField(this PropertyInfo propertyInfo, string idname)
        {

            return (propertyInfo.GetCustomAttribute(typeof(IdentityFiledAttribute)) != null) || (propertyInfo.Name == idname);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="oldId"></param>
        /// <returns></returns>

        public static string IdentityFiledStr<TEntity>(string oldId)
        {
            var property = typeof(TEntity).GetProperties()
                                             .Where(p => p.IsIdField()).FirstOrDefault();
            if (property != null)
            {
                return property.Name;
            }
            return oldId;


        }

        /// <summary>
        /// 是否非数据库属性
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>

        public static bool IsNonDBField(this PropertyInfo propertyInfo)
        {

            return propertyInfo.GetCustomAttribute(typeof(NonDatabaseFieldAttribute)) != null;

        }

        /// <summary>
        /// 是否为关键字段
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsKeyField(this PropertyInfo propertyInfo)
        {

            return propertyInfo.GetCustomAttribute(typeof(KeyFiledAttribute)) != null;

        }

        /// <summary>
        /// 数据表名称
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>

        public static string DbTableName<TEntity>()
        {

            var attribute = typeof(TEntity).GetCustomAttribute(typeof(TableNameAttribute));
            if (attribute != null)
            {
                return (attribute as TableNameAttribute).TableName;
            }
            return typeof(TEntity).Name;
        }

        public static string DbTableSchema<TEntity>()
        {

            var attribute = typeof(TEntity).GetCustomAttribute(typeof(TableSchemaAttribute));
            if (attribute != null)
            {
                return (attribute as TableSchemaAttribute).TableSchema;
            }
            return "dbo";
        }


        public static string DbTableName<TEntity>(this TEntity entity)
        {

            var attribute = typeof(TEntity).GetCustomAttribute(typeof(TableNameAttribute));
            if (attribute != null)
            {
                return (attribute as TableNameAttribute).TableName;
            }
            return typeof(TEntity).Name;
        }

        public static string DbTableSchemae<TEntity>(this TEntity entity)
        {

            var attribute = typeof(TEntity).GetCustomAttribute(typeof(TableSchemaAttribute));
            if (attribute != null)
            {
                return (attribute as TableSchemaAttribute).TableSchema;
            }
            return "dbo";
        }
    }
}
