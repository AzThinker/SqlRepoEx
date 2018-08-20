using SqlRepoEx.Abstractions;
using System;
using System.Linq.Expressions;

namespace SqlRepoEx.SqlServer.Abstractions
{
    public interface IOrderByClauseBuilder : IClauseBuilder
    {
        string ActiveAlias { get; }

        IOrderByClauseBuilder By<TEntity>(Expression<Func<TEntity, object>> selector,
            params Expression<Func<TEntity, object>>[] additionalSelectors);

        IOrderByClauseBuilder By<TEntity>(string alias,
            string tableName,
            Expression<Func<TEntity, object>> selector,
            params Expression<Func<TEntity, object>>[] additionalSelectors);

        IOrderByClauseBuilder ByDescending<TEntity>(Expression<Func<TEntity, object>> selector,
            params Expression<Func<TEntity, object>>[] additionalSelectors);

        IOrderByClauseBuilder ByDescending<TEntity>(string alias,
            Expression<Func<TEntity, object>> selector,
            params Expression<Func<TEntity, object>>[] additionalSelectors);

        IOrderByClauseBuilder FromScratch();
        IOrderByClauseBuilder UsingAlias(string alias);

        IOrderByClauseBuilder By<TEntity>(TableAlias alias,
           string tableName,
           Expression<Func<TEntity, object>> selector,
           params Expression<Func<TEntity, object>>[] additionalSelectors);
        IOrderByClauseBuilder By<TEntity>(TableAlias alias,
              string tableName,
              string tableSchema,
              Expression<Func<TEntity, object>> selector,
              params Expression<Func<TEntity, object>>[] additionalSelectors);


        IOrderByClauseBuilder ByDescending<TEntity>(TableAlias alias,
              Expression<Func<TEntity, object>> selector,
              params Expression<Func<TEntity, object>>[] additionalSelectors);

        IOrderByClauseBuilder ByDescending<TEntity>(TableAlias alias,
              string tableName,
              string tableSchema,
              Expression<Func<TEntity, object>> selector,
              params Expression<Func<TEntity, object>>[] additionalSelectors);
    }
}