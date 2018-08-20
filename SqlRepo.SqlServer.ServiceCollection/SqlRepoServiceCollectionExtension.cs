using System;
using Microsoft.Extensions.DependencyInjection;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Abstractions;

namespace SqlRepoEx.SqlServer.ServiceCollection
{
    public static class SqlRepoServiceCollectionExtension
    {
        public static IServiceCollection AddSqlRepo(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepositoryFactory, RepositoryFactory>();
            serviceCollection.AddTransient<IStatementFactoryProvider, StatementFactoryProvider>();
            serviceCollection.AddTransient<IEntityMapper, DataReaderEntityMapper>();
            serviceCollection.AddTransient<IWritablePropertyMatcher, WritablePropertyMatcher>();
            serviceCollection.AddTransient<ISqlLogger, SqlLogger>();

            return serviceCollection;
        }
    }
}