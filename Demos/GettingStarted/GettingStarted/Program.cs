using System;
using System.Linq.Expressions;
using Autofac;
using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.Autofac;
using SqlRepoEx.SqlServer.ConnectionProviders;

namespace GettingStartedIoC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<SqlRepoSqlServerAutofacModule>();

            var connectionProvider = new AppConfigFirstConnectionProvider();
            containerBuilder.RegisterInstance(connectionProvider)
                            .As<IConnectionProvider>();

            containerBuilder.RegisterType<GettingStarted>()
                            .As<IGettingStarted>();

            // ... other registrations

            var container = containerBuilder.Build();

            var gettingStarted = container.Resolve<IGettingStarted>();
            // gettingStarted.DoIt();
            gettingStarted.DoIt2();


          //  var exp= Expression<Func<

            Console.ReadLine();
        }
    }
}