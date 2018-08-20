using System;
using System.Linq.Expressions;

namespace SqlRepoEx.Abstractions
{
    public interface IInsertStatement<TEntity> : ISqlStatement<TEntity>
        where TEntity : class, new()
    {
        IInsertStatement<TEntity> For(TEntity entity);
        IInsertStatement<TEntity> FromScratch();
        IInsertStatement<TEntity> UsingTableName(string tableName);

        IInsertStatement<TEntity> UsingIdField<TMember>(Expression<Func<TEntity, TMember>> idField, bool IsAutoInc = true);
        IInsertStatement<TEntity> UsingTableSchema(string tableSchema);
        IInsertStatement<TEntity> With<TMember>(Expression<Func<TEntity, TMember>> selector, TMember @value);
    }
}