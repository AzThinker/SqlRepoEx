using SqlRepoEx.Abstractions;
using System;
using System.Linq.Expressions;

namespace SqlRepoEx.SqlServer.Abstractions
{
    public interface IWhereClauseBuilder : IClauseBuilder
    {
        IWhereClauseBuilder And<TEntity>(Expression<Func<TEntity, bool>> expression,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder AndIn<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
            TMember[] values,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder EndNesting();

        IWhereClauseBuilder NestedAnd<TEntity>(Expression<Func<TEntity, bool>> expression,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder NestedOr<TEntity>(Expression<Func<TEntity, bool>> expression,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder Or<TEntity>(Expression<Func<TEntity, bool>> expression,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder OrIn<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
            TMember[] values,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder Where<TEntity>(Expression<Func<TEntity, bool>> expression,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder WhereIn<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
            TMember[] values,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder WhereBetween<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
            TMember start,
            TMember end,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder AndBetween<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
            TMember start,
            TMember end,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        IWhereClauseBuilder OrBetween<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
            TMember start,
            TMember end,
            string alias = null,
            string tableName = null,
            string tableSchema = null);

        #region New 2018.8.20
        IWhereClauseBuilder And<TEntity>(Expression<Func<TEntity, bool>> expression,
          TableAlias alias,
          string tableName = null,
          string tableSchema = null);

        IWhereClauseBuilder AndBetween<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
             TMember start,
             TMember end,
             TableAlias alias,
              string tableName = null,
              string tableSchema = null);
        IWhereClauseBuilder AndIn<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
              TMember[] values, TableAlias alias,
              string tableName = null,
              string tableSchema = null);

        IWhereClauseBuilder NestedAnd<TEntity>(Expression<Func<TEntity, bool>> expression,
               TableAlias alias,
              string tableName = null,
              string tableSchema = null);

        IWhereClauseBuilder NestedOr<TEntity>(Expression<Func<TEntity, bool>> expression,
               TableAlias alias,
              string tableName = null,
              string tableSchema = null);

        IWhereClauseBuilder Or<TEntity>(Expression<Func<TEntity, bool>> expression,
              TableAlias alias,
              string tableName = null,
              string tableSchema = null);

        IWhereClauseBuilder OrBetween<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
              TMember start,
              TMember end,
              TableAlias alias,
              string tableName = null,
              string tableSchema = null);
        IWhereClauseBuilder OrIn<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
              TMember[] values,
              TableAlias alias,
              string tableName = null,
              string tableSchema = null);
        IWhereClauseBuilder Where<TEntity>(Expression<Func<TEntity, bool>> expression,
            TableAlias alias, bool ClearBeforeWhereeClause = false,
              string tableName = null,
              string tableSchema = null);
        IWhereClauseBuilder WhereBetween<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
              TMember start,
              TMember end,
               TableAlias alias,
               bool ClearBeforeWhereeClause = false,
              string tableName = null,
              string tableSchema = null);
        IWhereClauseBuilder WhereIn<TEntity, TMember>(Expression<Func<TEntity, TMember>> selector,
              TMember[] values,
             TableAlias alias,
             bool ClearBeforeWhereeClause = false,
              string tableName = null,
              string tableSchema = null);
        #endregion
    }
}