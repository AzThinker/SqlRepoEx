using System;
using SqlRepoEx;
using SqlRepoEx.SqlServer.ConnectionProviders;
using SqlRepoEx.SqlServer.Static;

namespace GettingStartedStatic
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connectionProvider = new AppConfigFirstConnectionProvider();
            RepoFactory.UseConnectionProvider(connectionProvider);
            RepoFactory.UseLogger(new NoOpSqlLogger());
            var gettingStarted = new GettingStarted();
            gettingStarted.DoIt();

            Console.ReadLine();
        }
    }
}