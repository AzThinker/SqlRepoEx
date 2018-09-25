using System;
using System.Collections.Generic;
using System.Data;
namespace SqlRepoEx.SqlServer.Abstractions
{
    public interface ISqlParameterCollection
    {
        void AddWithValue(string name, object value, bool isnullable, DbType dbType, int size = 0, ParameterDirection direction = ParameterDirection.Input);
    }
}