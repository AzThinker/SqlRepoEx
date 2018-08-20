using System;

namespace SqlRepoEx.Abstractions
{
    public interface IExecuteNonQueryProcedureStatement : IExecuteProcedureStatement<int> { }
}