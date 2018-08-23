using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SqlRepoEx.SqlServer.CustomAttribute
{
    public static class CustomAttributeHandle
    {

        public static bool IsIdField(this PropertyInfo propertyInfo)
        {

            return propertyInfo.GetCustomAttribute(typeof(IdentityFiledAttribute)) != null;

        }

        public static bool IsIdField(this PropertyInfo propertyInfo, string idname)
        {

            return (propertyInfo.GetCustomAttribute(typeof(IdentityFiledAttribute)) != null) || (propertyInfo.Name == idname);


        }

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

        public static bool IsNonDBField(this PropertyInfo propertyInfo)
        {

            return propertyInfo.GetCustomAttribute(typeof(NonDatabaseFieldAttribute)) != null;

        }


        public static bool IsKeyField(this PropertyInfo propertyInfo)
        {

            return propertyInfo.GetCustomAttribute(typeof(KeyFiledAttribute)) != null;

        }



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
