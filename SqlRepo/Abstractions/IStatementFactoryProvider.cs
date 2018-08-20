using System;

namespace SqlRepoEx.Abstractions
{
    public interface IStatementFactoryProvider
    {
        IStatementFactory Provide();
    }
}
