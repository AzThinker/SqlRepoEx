using System;
using System.Configuration;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Abstractions;

namespace SqlRepoEx.SqlServer.ConnectionProviders
{
    public class AppConfigNamedConnectionProvider : ISqlConnectionProvider
    {
        private readonly string connectionName;

        public AppConfigNamedConnectionProvider(string connectionName)
        {
            this.connectionName = connectionName;
        }

        public TConnection Provide<TConnection>()
            where TConnection: class, IConnection
        {
            var connectionString = ConfigurationManager.ConnectionStrings[this.connectionName]
                                                       .ConnectionString;
            return new SqlConnectionAdapter(connectionString) as TConnection;
        }
    }
}