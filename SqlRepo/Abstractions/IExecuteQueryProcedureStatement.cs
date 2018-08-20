using System;
using System.Collections.Generic;

namespace SqlRepoEx.Abstractions
{
    public interface IExecuteQueryProcedureStatement<TEntity> : IExecuteProcedureStatement<IEnumerable<TEntity>>
        where TEntity: class, new() { }
}