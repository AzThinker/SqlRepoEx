using System;
using SqlRepoEx.Model;

namespace SqlRepoEx.SqlServer.Abstractions
{
    public interface IEntityMapperConfiguration
    {
        bool CanHandle<TEntity>() where TEntity: IEntity<int>;
    }
}