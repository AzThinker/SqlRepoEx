using System;
using Microsoft.Extensions.Configuration;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Abstractions;

namespace SqlRepoEx.SqlServer.ConnectionProviders
{
    public class AppSettingsConnectionProvider : ISqlConnectionProvider
    {
        private readonly IConfiguration configuration;
        private readonly string connectionName;

        public AppSettingsConnectionProvider(IConfiguration configuration, string connectionName)
        {
            this.configuration = configuration;
            this.connectionName = connectionName;
        }

        public TConnection Provide<TConnection>()
            where TConnection: class, IConnection
        {
            var connectionString = this.configuration.GetConnectionString(this.connectionName);

            return new SqlConnectionAdapter(connectionString) as TConnection;
        }
    }
}