using System;

namespace SqlRepoEx.Abstractions
{
    public interface ISqlLogger
    {
        void Log(string sql);
    }
}