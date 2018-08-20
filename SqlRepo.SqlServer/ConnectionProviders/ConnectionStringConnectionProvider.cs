using System;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Abstractions;

namespace SqlRepoEx.SqlServer.ConnectionProviders
{
    public class ConnectionStringConnectionProvider : ISqlConnectionProvider
    {
        private readonly string connectionString;

        public ConnectionStringConnectionProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public TConnection Provide<TConnection>()
            where TConnection: class, IConnection
        {
            return new SqlConnectionAdapter(this.connectionString) as TConnection;
        }
    }
}