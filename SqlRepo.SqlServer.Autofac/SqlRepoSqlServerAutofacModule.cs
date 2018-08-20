using System;
using Autofac;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Abstractions;

namespace SqlRepoEx.SqlServer.Autofac
{
    public class SqlRepoSqlServerAutofacModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<RepositoryFactory>()
                            .As<IRepositoryFactory>();
            containerBuilder.RegisterType<StatementFactoryProvider>()
                            .As<IStatementFactoryProvider>();
            containerBuilder.RegisterType<DataReaderEntityMapper>()
                            .As<IEntityMapper>();
            containerBuilder.RegisterType<WritablePropertyMatcher>()
                            .As<IWritablePropertyMatcher>();
            containerBuilder.RegisterType<SqlLogger>()
                            .As<ISqlLogger>();
        }
    }
}