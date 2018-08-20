using System;
using System.Collections.Generic;

namespace SqlRepoEx.Abstractions
{
    public interface IExecuteQuerySqlStatement<TEntity> : IExecuteSqlStatement<IEnumerable<TEntity>>
        where TEntity: class, new() { }
}