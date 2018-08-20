using SqlRepoEx.Abstractions;
using System;
using System.Linq.Expressions;

namespace SqlRepoEx.SqlServer.Abstractions
{
    public interface IFromClauseBuilder : IClauseBuilder
    {
        IFromClauseBuilder From<T>(string tableAlias = null,
            string tableName = null,
            string tableSchema = null);

        IFromClauseBuilder InnerJoin<TLeft, TRight>(string leftTableAlias = null,
            string rightTableAlias = null,
            string rightTableName = null,
            string rightTableSchema = null);

        IFromClauseBuilder LeftOuterJoin<TLeft, TRight>(string leftTableAlias = null,
            string rightTableAlias = null,
            string rightTableName = null,
            string rightTableSchema = null);

        IFromClauseBuilder On<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> expression,
            string leftTableAlias = null,
            string rightTableAlias = null);

        IFromClauseBuilder RightOuterJoin<TLeft, TRight>(string leftTableAlias = null,
            string rightTableAlias = null,
            string rightTableName = null,
            string rightTableSchema = null);

        #region New 2018.8.20
        IFromClauseBuilder From<TEntity>(TableAlias alias,
                    string tableName = null,
                    string tableSchema = null);


        IFromClauseBuilder InnerJoin<TLeft, TRight>(TableAlias leftTableAlias,
              TableAlias rightTableAlias,
              string rightTableName = null,
              string rightTableSchema = null);


        IFromClauseBuilder LeftOuterJoin<TLeft, TRight>(TableAlias leftTableAlias,
             TableAlias rightTableAlias,
             string rightTableName = null,
             string rightTableSchema = null);

        IFromClauseBuilder On<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> expression,
             TableAlias leftTableAlias,
             TableAlias rightTableAlias);

        IFromClauseBuilder RightOuterJoin<TLeft, TRight>(TableAlias leftTableAlias,
             TableAlias rightTableAlias,
              string rightTableName = null,
              string rightTableSchema = null);
        #endregion
        TableDefinition TableDefinition<T>(string alias = null);
    }
}