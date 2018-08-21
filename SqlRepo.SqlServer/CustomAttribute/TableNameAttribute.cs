using System;
using System.Collections.Generic;
using System.Text;

namespace SqlRepoEx.SqlServer.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableNameAttribute : Attribute
    {
        private string tableName;
        public TableNameAttribute(string tablename)
        {
            tableName = tablename;
        }

        public string TableName { get { return tableName; } }
    }
}