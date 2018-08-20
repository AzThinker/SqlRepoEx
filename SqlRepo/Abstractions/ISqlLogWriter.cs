using System;

namespace SqlRepoEx.Abstractions
{
    public interface ISqlLogWriter
    {
        void Log(string sql);
    }
}