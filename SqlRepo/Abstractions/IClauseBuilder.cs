using System;

namespace SqlRepoEx.Abstractions
{
    public interface IClauseBuilder
    {
        string Sql();
        bool IsClean { get; set; }
    }
}