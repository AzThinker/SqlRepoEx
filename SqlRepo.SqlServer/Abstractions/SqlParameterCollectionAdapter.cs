using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SqlRepoEx.SqlServer.Abstractions
{
    public class SqlParameterCollectionAdapter : ISqlParameterCollection
    {
        private readonly SqlParameterCollection parameters;

       

        public SqlParameterCollectionAdapter(SqlParameterCollection parameters)
        {
            this.parameters = parameters;
        }


        public void AddWithValue(string name, object value, bool isnullable, DbType dbType, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            this.parameters.Add(new SqlParameter { ParameterName = name, DbType = dbType, IsNullable = isnullable, Size = size, Value = value, Direction = direction });
           
        }

       
    }
}
