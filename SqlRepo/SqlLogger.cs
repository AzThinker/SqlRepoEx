using System;
using System.Collections.Generic;
using SqlRepoEx.Abstractions;

namespace SqlRepoEx
{
    public class SqlLogger : ISqlLogger
    {
        private readonly IEnumerable<ISqlLogWriter> sqlLogWriters;

        public SqlLogger(IEnumerable<ISqlLogWriter> sqlLogWriters)
        {
            this.sqlLogWriters = sqlLogWriters;
        }

        public void Log(string sql)
        {
            foreach(var sqlLogWriter in this.sqlLogWriters)
            {
                sqlLogWriter.Log(sql);
            }
        }
    }
}