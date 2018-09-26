using System;
using SqlRepoEx.SqlServer.Static;

namespace GettingStartedStatic
{
    public class GettingStarted
    {
        public void DoIt()
        {
            var repository = RepoFactory.Create<ToDo>();
            var results = repository.Query()
                                    .Select(e => e.Id, e => e.Task, e => e.CreatedDate)
                                    .OrderBy(e => e.Id)
                                    .Page(10, 3)
                                    .Go();
            var results2 = repository.Query()
                                    .Select(e => e.Id, e => e.Task, e => e.CreatedDate)
                                    .OrderBy(e => e.Id)
                                    .Page(10, 3).Sql();
            Console.WriteLine(results2);
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Id},{item.Task},{item.CreatedDate} ");
            }

        }
    }
}