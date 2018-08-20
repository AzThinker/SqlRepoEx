using System;

namespace SqlRepoEx.SqlServer.Abstractions {
    public interface ISqlParameterCollection
    {
        void AddWithValue(string name, object value);
    }
}