using SqlRepoEx.Abstractions;
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
            var repository = this.repositoryFactory.Create<ToDo>();
            var results = repository.Query()
                                    .Select(e => e.Id, e => e.Task, e => e.CreatedDate);
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
            var repository = this.repositoryFactory.Create<DoitTest>();
            DoitTest doitTest = new DoitTest();

            doitTest.TestRmk = "测试";
            doitTest.TestBool = true;
            doitTest.TestId = 123;
            Console.WriteLine(repository.Insert().UsingIdField(d => d.TestId, false).For(doitTest).Sql());
            //  var results = repository.Insert().UsingIdField(d=>d.TestId).For(doitTest).Go();

            //  Console.WriteLine($"{results.TestId},{results.TestRmk},{results.TestBool} ");


        }
    }
}
