using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace SqlRepoEx.SqlServer
{
    public class InsertStatement<TEntity> : SqlStatement<TEntity, TEntity>, IInsertStatement<TEntity>
        where TEntity : class, new()
    {
        private const string StatementTemplateAutoInc = "INSERT [{0}].[{1}]({2})\nVALUES({3}){4};"
                                                 + "\nSELECT *\nFROM [{0}].[{1}]\nWHERE [{5}] = SCOPE_IDENTITY();";
        private const string StatementTemplate = "INSERT [{0}].[{1}]({2})\nVALUES({3}){4};";
        private readonly IList<Expression<Func<TEntity, object>>> selectors =
            new List<Expression<Func<TEntity, object>>>();

        private readonly IList<object> values = new List<object>();
        private readonly IWritablePropertyMatcher writablePropertyMatcher;
        private TEntity entity;
        private string IdentityFiled = "Id";
        private bool IsAutoIncrement = true;


        public InsertStatement(IStatementExecutor statementExecutor,
            IEntityMapper entityMapper,
            IWritablePropertyMatcher writablePropertyMatcher)
            : base(statementExecutor, entityMapper)
        {
            this.writablePropertyMatcher = writablePropertyMatcher;
        }

        public IInsertStatement<TEntity> For(TEntity entity)
        {
            if (this.selectors.Any())
            {
                throw new InvalidOperationException(
                    "For cannot be used once With has been used, please use FromScratch to reset the command before using With.");
            }
            this.IsClean = false;
            this.entity = entity;
            return this;
        }

        public IInsertStatement<TEntity> FromScratch()
        {
            this.selectors.Clear();
            this.values.Clear();
            this.entity = null;
            this.IsClean = true;
            return this;
        }

        public override TEntity Go()
        {
            if (IsAutoIncrement)
            {
                using (var reader = this.StatementExecutor.ExecuteReader(this.Sql()))
                {
                    return this.EntityMapper.Map<TEntity>(reader)
                               .FirstOrDefault();
                }
            }
            else
            {
                this.StatementExecutor.ExecuteNonQuery(this.Sql());
                return this.entity;
            }

        }

        public override async Task<TEntity> GoAsync()
        {
            if (IsAutoIncrement)
            {
                using (var reader = await this.StatementExecutor.ExecuteReaderAsync(this.Sql()))
                {
                    return this.EntityMapper.Map<TEntity>(reader)
                               .FirstOrDefault();
                }
            }
            else
            {
                await this.StatementExecutor.ExecuteNonQueryAsync(this.Sql());
                return this.entity;
            }


        }

        public override string Sql()
        {
            if (this.entity == null && !this.selectors.Any())
            {
                throw new InvalidOperationException(
                    "Sql cannot be used on a command that has not been initialised using With or For.");
            }
            if (IsAutoIncrement)
            {
                return string.Format(StatementTemplateAutoInc,
                                this.TableSchema,
                                this.TableName,
                                this.GetColumnsList(),
                                this.GetValuesList(),
                                string.Empty,
                                IdentityFiled);
            }
            else
            {
                return string.Format(StatementTemplate,
              this.TableSchema,
              this.TableName,
              this.GetColumnsList(),
              this.GetValuesList(),
              string.Empty);
            }

        }

        public IInsertStatement<TEntity> UsingTableName(string tableName)
        {
            this.TableName = tableName;
            return this;
        }


        public IInsertStatement<TEntity> UsingIdField<TMember>(Expression<Func<TEntity, TMember>> idField, bool IsAutoInc = true)
        {
            var fieldSelect = Reflect<TEntity>.GetProperty<TMember>(idField);
            if (IsAutoInc)
            {
                this.IdentityFiled = fieldSelect.Name;
            }
            else
            {
                this.IdentityFiled = "this_is_no_identityfiled";
            }
            

            this.IsAutoIncrement = IsAutoInc;


            return this;
        }

        public IInsertStatement<TEntity> UsingTableSchema(string tableSchema)
        {
            this.TableSchema = tableSchema;
            return this;
        }

        public IInsertStatement<TEntity> With<TMember>(Expression<Func<TEntity, TMember>> selector,
            TMember @value)
        {
            if (this.entity != null)
            {
                throw new InvalidOperationException(
                    "With cannot be used once For has been used, please use FromScratch to reset the command before using With.");
            }

            this.IsClean = false;
            var expression = this.ConvertExpression(selector);
            this.selectors.Add(expression);
            this.values.Add(@value);
            return this;
        }

        private string FormatColumnNames(IEnumerable<string> names)
        {
            return string.Join(", ", names.Select(n => $"[{n}]"));
        }

        private string FormatValues(IEnumerable<object> values)
        {
            return string.Join(", ", values.Select(this.FormatValue));
        }

        private string GetColumnsList()
        {
            return this.selectors.Any() ? this.GetColumnsListFromSelectors() : this.GetColumnsListFromEntity();
        }

        private string GetColumnsListFromEntity()
        {
            var names = typeof(TEntity).GetProperties()
                                       .Where(p => p.Name != IdentityFiled
                                                   && this.writablePropertyMatcher.Test(p.PropertyType))
                                       .Select(p => p.Name);
            return this.FormatColumnNames(names);
        }

        private string GetColumnsListFromSelectors()
        {
            var names = this.selectors.Select(this.GetMemberName);
            return this.FormatColumnNames(names);
        }

        private string GetValuesFromEntity()
        {
            var entityValues = typeof(TEntity).GetProperties()
                                              .Where(p => p.Name != IdentityFiled
                                                          && this.writablePropertyMatcher.Test(
                                                              p.PropertyType))
                                              .Select(p => p.GetValue(this.entity));
            return this.FormatValues(entityValues);
        }

        private string GetValuesList()
        {
            return this.selectors.Any() ? this.FormatValues(this.values) : this.GetValuesFromEntity();
        }
    }
}