using System;
using System.Data;

namespace SqlRepoEx
{
    public class ParameterDefinition
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ParameterDirection Direction { get; set; }

        public int Size { get; set; }

        public bool isNullable { get; set; }

        public DbType DbType { get; set; }
    }
}