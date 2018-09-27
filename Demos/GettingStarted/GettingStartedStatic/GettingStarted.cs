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
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Id},{item.Task},{item.CreatedDate} ");
            }
            var repository2 = RepoFactory.Create<TwoRemark>();
            var results2 = repository2.Query()
                                    .Select(t => t.Task, t => t.Remark)
                                    .Go();


            foreach (var item in results2)
            {
                Console.WriteLine($"{item.Task},{item.Remark} ");
            }

            var repository3 = RepoFactory.Create<ToDo>();

            var result3 = repository3.Query().From("a")
                .InnerJoin<TwoRemark>("b").On<TwoRemark>((l, r) => l.Task == r.Task,"a","b").OrderBy(e => e.Id,"a").Page(10, 3).Go();

            //Console.WriteLine(result3);


            // var result4 = result3.Go();
            foreach (var item in result3)
            {
                Console.WriteLine($"{item.Id},{item.Task},{item.CreatedDate} ");
            }
        }
    }
}