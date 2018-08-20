using System;

namespace SqlRepoEx.Abstractions
{
    public interface IExecuteNonQuerySqlStatement : IExecuteSqlStatement<int> { }
}