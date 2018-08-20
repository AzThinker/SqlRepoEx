using System;

namespace SqlRepoEx.Abstractions
{
    public interface IConnectionProvider
    {
        TConnection Provide<TConnection>() where TConnection: class, IConnection;
    }
}
