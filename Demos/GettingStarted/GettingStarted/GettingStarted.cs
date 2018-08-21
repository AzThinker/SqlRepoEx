using SqlRepoEx.Abstractions;
using SqlRepoEx.SqlServer.CustomAttribute;
using System;

namespace GettingStartedIoC
{
    public class GettingStarted : IGettingStarted
    {
        private readonly IRepositoryFactory repositoryFactory;

        public GettingStarted(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }

        public void DoIt()
        {
            var repository = this.repositoryFactory.Create<ToDo_New>();
            var results = repository.Query();
            //  .Where(e => e.IsCompleted == false)
            //  .Go();

            Console.WriteLine(results.Sql());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            results = results.Where(e => e.IsCompleted == false);

            results = results.Where(e => e.Id == 3);


            Console.WriteLine(results.Sql());

        }


        public void DoIt2()
        {
            Console.WriteLine(CustomAttributeHandle.IdentityFiledStr<DoitTest_New>("OK"));
            var repository = this.repositoryFactory.Create<DoitTest_New>();
            DoitTest_New doitTest = new DoitTest_New();

            doitTest.TestRmk = "测试";
            doitTest.TestBool = true;
            doitTest.TestId = 123;
            Console.WriteLine(repository.Insert().For(doitTest).Sql());
            // Console.WriteLine(repository.Insert().UsingIdField(d => d.TestId).For(doitTest).Sql());
            //  var results = repository.Insert().UsingIdField(d=>d.TestId).For(doitTest).Go();

            //  Console.WriteLine($"{results.TestId},{results.TestRmk},{results.TestBool} ");


        }
    }
}
