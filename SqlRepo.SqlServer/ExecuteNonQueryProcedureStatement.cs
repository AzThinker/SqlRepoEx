using System;
using System.Linq;
using System.Threading.Tasks;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Abstractions;

namespace SqlRepoEx.SqlServer
{
    public class ExecuteNonQueryProcedureStatement : ExecuteProcedureStatement<int>, IExecuteNonQueryProcedureStatement
    {
        public ExecuteNonQueryProcedureStatement(IStatementExecutor statementExecutor)
            : base(statementExecutor) { }

        public override int Go()
        {
            if(string.IsNullOrWhiteSpace(this.ProcedureName))
            {
                throw new MissingProcedureNameException();
            }

            var procedureName = $"[{this.SchemaName}].[{this.ProcedureName}]";
            return this.ParameterDefinitions.Any()
                       ? this.StatementExecutor.ExecuteNonQueryStoredProcedure(procedureName,
                           this.ParameterDefinitions.ToArray())
                       : this.StatementExecutor.ExecuteNonQueryStoredProcedure(procedureName);
        }

        public override async Task<int> GoAsync()
        {
            if(string.IsNullOrWhiteSpace(this.ProcedureName))
            {
                throw new MissingProcedureNameException();
            }

            var procedureName = $"[{this.SchemaName}].[{this.ProcedureName}]";
            return this.ParameterDefinitions.Any()
                       ? await this.StatementExecutor.ExecuteNonQueryStoredProcedureAsync(procedureName,
                             this.ParameterDefinitions.ToArray())
                       : await this.StatementExecutor.ExecuteNonQueryStoredProcedureAsync(procedureName);
        }
    }
}