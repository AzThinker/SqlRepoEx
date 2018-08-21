using System;
using System.Collections.Generic;
using System.Text;

namespace SqlRepoEx.SqlServer.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableSchemaAttribute : Attribute
    {
        private string tableSchema;
        public TableSchemaAttribute(string tableschema)
        {
            tableSchema = tableschema;
        }

        public string TableSchema { get { return tableSchema; } }
    }
}