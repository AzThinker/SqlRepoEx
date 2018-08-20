using System;

namespace SqlRepoEx.Abstractions
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>() where TEntity: class, new();
    }
}