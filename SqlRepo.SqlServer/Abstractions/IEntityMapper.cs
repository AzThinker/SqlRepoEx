using System;
using System.Collections.Generic;
using System.Data;

namespace SqlRepoEx.SqlServer.Abstractions
{
    public interface IEntityMapper
    {
        IEnumerable<TEntity> Map<TEntity>(IDataReader reader) where TEntity: class, new();
    }
}