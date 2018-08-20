using System;
using SqlRepoEx.Abstractions;

namespace SqlRepoEx
{
    public class ConsoleSqlLogger : ISqlLogWriter
    {
        public void Log(string sql)
        {
            Console.WriteLine(sql);
        }
    }
}