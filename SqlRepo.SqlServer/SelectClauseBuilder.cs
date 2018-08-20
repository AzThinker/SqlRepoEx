using SqlRepoEx.SqlServer.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlRepoEx.SqlServer
{
    public class SelectClauseBuilder : ClauseBuilder, ISelectClauseBuilder
    {
        private const string ClauseTemplate = "SELECT {0}{1}";
        private readonly IList<ColumnSelection> selections = new List<ColumnSelection>();
        private int? topRows;

        public ISelectClauseBuilder Avg<TEntity>(Expression<Func<TEntity, object>> selector,
            string alias = null,
            string tableName = null,
            string tableSchema = null)
        {
            this.AddColumnSelection<TEntity>(alias,
                tableName,
                tableSchema,
                this.GetMemberName(selector),
                Aggregation.Avg);
            return this;
        }

        public ISelectClauseBuilder Count<TEntity>(Expression<Func<TEntity, object>> selector,
            string alias = null,
            string tableName = null,
            string tableSchema = null)
        {
            this.AddColumnSelection<TEntity>(alias,
                tableName,
                tableSchema,
                this.GetMemberName(selector),
                Aggregation.Count);
            return this;
        }

        public ISelectClauseBuilder CountAll()
        {
            this.selections.Add(new ColumnSelection
            {
                Name = "*",
                Aggregation = Aggregation.Count
            });
            return this;
        }

        public ISelectClauseBuilder For<TEntity>(TEntity entity,
            string alias = null,
            string tableSchema = null,
            string tableName = null)
        {
            foreach (var property in entity.GetType()
                                          .GetProperties()
                                          .Where(p => p.CanWrite))
            {
                this.AddColumnSelection<TEntity>(alias, tableName, tableSchema, property.Name);
            }
            this.IsClean = false;
            return this;
        }

        public ISelectClauseBuilder Max<TEntity>(Expression<Func<TEntity, object>> selector,
            string alias = null,
            string tableName = null,
            string tableSchema = null)
        {
            this.AddColumnSelection<TEntity>(alias,
                tableName,
                tableSchema,
                this.GetMemberName(selector),
                Aggregation.Max);
            return this;
        }

        public ISelectClauseBuilder Min<TEntity>(Expression<Func<TEntity, object>> selector,
            string alias = null,
            string tableName = null,
            string tableSchema = null)
        {
            this.AddColumnSelection<TEntity>(alias,
                tableName,
                tableSchema,
                this.GetMemberName(selector),
                Aggregation.Min);
            return this;
        }

        public ISelectClauseBuilder Select<TEntity>(Expression<Func<TEntity, object>> selector,
            string alias = null,
            string tableName = null,
            string tableSchema = null,
            params Expression<Func<TEntity, object>>[] additionalSelectors)
        {
            this.AddColumnSelection<TEntity>(alias, tableName, tableSchema, this.GetMemberName(selector));
            foreach (var additionalSelector in additionalSelectors)
            {
                this.AddColumnSelection<TEntity>(alias,
                    tableName,
                    tableSchema,
                    this.GetMemberName(additionalSelector));
            }

            this.IsClean = false;
            return this;
        }

        public ISelectClauseBuilder SelectAll<TEntity>(string alias = null,
            string tableName = null,
            string tableSchema = null)
        {
            this.AddColumnSelection<TEntity>(alias, tableName, tableSchema, "*");
            return this;
        }

        public override string Sql()
        {
            var selection = "*";

            if (this.selections.Any())
            {
                selection = string.Join(", ", this.selections);
            }

            return string.Format(ClauseTemplate,
                this.topRows.HasValue ? $"TOP {this.topRows.Value} " : string.Empty,
                selection);
        }

        public ISelectClauseBuilder Sum<TEntity>(Expression<Func<TEntity, object>> selector,
            string alias = null,
            string tableName = null,
            string tableSchema = null)
        {
            this.AddColumnSelection<TEntity>(alias,
                tableName,
                tableSchema,
                this.GetMemberName(selector),
                Aggregation.Sum);
            return this;
        }

        public ISelectClauseBuilder Top(int rows)
        {
            this.topRows = rows;
            return this;
        }
        #region New 2018.8.20
        public ISelectClauseBuilder Avg<TEntity>(Expression<Func<TEntity, object>> selector,
           TableAlias alias,
           string tableName = null,
           string tableSchema = null)
        {

            return Avg<TEntity>(selector, Utils.Alias(alias), tableName, tableSchema);
        }

        public ISelectClauseBuilder Count<TEntity>(Expression<Func<TEntity, object>> selector,
            TableAlias alias,
            string tableName = null,
            string tableSchema = null)
        {

            return Count<TEntity>(selector, Utils.Alias(alias), tableName, tableSchema);
        }


        public ISelectClauseBuilder For<TEntity>(TEntity entity,
            TableAlias alias,
            string tableSchema = null,
            string tableName = null)
        {
            return For<TEntity>(entity, Utils.Alias(alias), tableName, tableSchema);
        }

        public ISelectClauseBuilder Max<TEntity>(Expression<Func<TEntity, object>> selector,
            TableAlias alias,
            string tableName = null,
            string tableSchema = null)
        {
            return Max<TEntity>(selector, Utils.Alias(alias), tableName, tableSchema);
        }

        public ISelectClauseBuilder Min<TEntity>(Expression<Func<TEntity, object>> selector,
            TableAlias alias,
            string tableName = null,
            string tableSchema = null)
        {
            return Min<TEntity>(selector, Utils.Alias(alias), tableName, tableSchema);
        }

        public ISelectClauseBuilder Select<TEntity>(Expression<Func<TEntity, object>> selector,
            TableAlias alias,
            string tableName = null,
            string tableSchema = null,
            params Expression<Func<TEntity, object>>[] additionalSelectors)
        {
            return Select<TEntity>(selector, Utils.Alias(alias), tableName, tableSchema, additionalSelectors);
        }

        public ISelectClauseBuilder SelectAll<TEntity>(TableAlias alias,
            string tableName = null,
            string tableSchema = null)
        {

            return SelectAll<TEntity>(tableName, tableSchema);
        }



        public ISelectClauseBuilder Sum<TEntity>(Expression<Func<TEntity, object>> selector,
             TableAlias alias,
            string tableName = null,
            string tableSchema = null)
        {

            return SelectAll<TEntity>(Utils.Alias(alias), tableName, tableSchema);
        }
        #endregion
        private void AddColumnSelection<TEntity>(string alias,
            string tableName,
            string tableSchema,
            string name,
            Aggregation aggregation = Aggregation.None)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                tableName = this.TableNameFromType<TEntity>();
            }

            if (string.IsNullOrWhiteSpace(tableSchema))
            {
                tableSchema = DefaultSchema;
            }

            this.selections.Add(new ColumnSelection
            {
                Alias = alias,
                Table = tableName,
                Schema = tableSchema,
                Name = name,
                Aggregation = aggregation
            });
        }
    }
}