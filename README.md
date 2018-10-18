## Features
1, SqlRepoEx  Intuitively build SQL statements using C# Lambda Expressions，it is a  simple object mapper for .Net.</br>
2. SqlRepoEx solves the problem of Lambda to Sql statements. We no longer use strings to join Sql statements.</br>
3. SqlRepoEx not only implements the complete statement parser such as Select, Insert, Update, Delete, etc., but also implements clauses such as Select, where, order by, etc., which all support the export of SQL statements, making it easy to splicing SQL statements in complex scenarios.</br>
4.SqlRepoEx also provides IExecuteSqlStatement and its implementation class, which appends the SQL statement you want with the WithSql() method and gets the desired result with Go(), just like Go() in other statement parsers;</br>
5. SqlRepoEx solves the dialect access of common databases such as Sql Server and MySql, which makes it very easy for us to migrate between Sql Server and MySql. You don't need to care what database you are using.</br>
6. SqlRepoEx is fast.The native data access of SqlRepoEx is equal to that of Dapper. In addition, SqlRepoEx can best integrate with Dapper and can easily use Dapper's powerful functions.</br>
7. SqlRepoEx itself supports Sql Server and MySql dialect, and supports non-dialect Sql through sqlrepoex.normal. Other data access libraries, such as Dappers, can access most databases.</br>
8. In fact, SqlRepoEx parses Lambda and converts it into SQL statements, and you can use any ORM tool that can use SQL to access the data you want.</br>
9. SqlRepoEx supports a variety of complex SQL syntax, such as Union,Join, etc. Meanwhile, more complex type results can be expressed through simple Join on statements</br>
10, SqlRepoEx not intrusive, only through simple several features, can let the class with the database, there are no complicated XML configuration, also do not need josn configuration, if SqlRepoEx used in the configuration file, that is configured in the database connection string in the configuration file, of course, you can directly specified in the code, which is depending on your use of the data provider.</br>
11. SqlRepoEx USES Lambda expressions, so it is very simple for c# programmers. The syntax is very similar to Linq to Sql, but does not depend on the data context, so there is no need to accommodate the database design.</br>
12. Most types of SqlRepoEx are rewritable and highly extensible (SqlRepoEx.Adapter.Dapper is a simple but powerful extension).</br>

## Installation
NuGet library that you can add in to your project</br>

## For Inline access Dapper

[SqlRepoEx.Adapter.Dapper](https://www.nuget.org/packages/SqlRepoEx.Adapter.Dapper/)</br>

For SQL Server ,Use static mode(WinForm,Asp.Net Core etc.)

[SqlRepoEx.MsSql.Static](https://www.nuget.org/packages/SqlRepoEx.MsSql.Static/)</br>

For SQL Server ,Use Autofac mode(WinForm,Asp.Net Core etc.)

[SqlRepoEx.MsSql.Autofac](https://www.nuget.org/packages/SqlRepoEx.MsSql.Autofac/)</br>

For SQL Server ,Use ServiceCollection mode ( Asp.Net Core etc.)

[SqlRepoEx.MsSql.ServiceCollection](https://www.nuget.org/packages/SqlRepoEx.MsSql.ServiceCollection/)</br>

For MySql ,Use static mode(WinForm,Asp.Net Core etc.)
[SqlRepoEx.MySql.Static](https://www.nuget.org/packages/SqlRepoEx.MySql.Static/)</br>

For MySql ,Use ServiceCollection mode(Asp.Net Core etc.) 

[SqlRepoEx.MySql.ServiceCollection](https://www.nuget.org/packages/SqlRepoEx.MySql.ServiceCollection/)</br>

For SQL Server ,Use Autofac mode(WinForm,Asp.Net Core etc.)
[SqlRepoEx.MySql.Autofac](https://www.nuget.org/packages/SqlRepoEx.MySql.Autofac/)</br>

## Example
``` c#
public class GettingStarted
{
    private IRepositoryFactory repositoryFactory;

    public GettingStarted(IRepositoryFactory repositoryFactory)
    {
        this.repositoryFactory = repositoryFactory;
    }

    public void DoIt()
    {
         IRepository<ToDo> repository = repositoryFactory.Create<ToDo>();
            var results = repository.Query()
                         .Select(e => e.Id, e => e.Task, e => e.CreatedDate);
	 results = results.Where(e => e.IsCompleted == false);
	 results = results.Where(e => e.Id >3);
	 var values=result.Go();
    }
}
```
Generates the following SQL statement and maps the results back to the list of ToDo objects.
``` sql

SELECT [dbo].[ToDo].[Id]
, [dbo].[ToDo].[Task]
, [dbo].[ToDo].[CreatedDate]
FROM [dbo].[ToDo]
WHERE ([dbo].[ToDo].[IsCompleted] = 0)
And ([dbo].[ToDo].[Id] > 3);

```  

## Page(SQL Server,Note that Order by is required)

```
var repository = MsSqlRepoFactory.Create<ToDo>();
 var results2 = repository.Query().Page(10, 3).Go();
```
Generates the following SQL statement and maps the results back to the list of ToDo objects.
``` sql
SELECT TOP (10) * FROM (SELECT row_number() OVER (
ORDER BY [dbo].[ToDo].[Id] ASC) as row_number,[dbo].[ToDo].[CreatedDate]
, [dbo].[ToDo].[IsCompleted]
, [dbo].[ToDo].[Task]
, [dbo].[ToDo].[Id]
FROM [dbo].[ToDo])As __Page_Query WHERE row_number > 20;

```
## Union

``` C#
 var repository = MsSqlRepoFactory.Create<ToDo>();
            var results = repository.Query().Select(e => e.Id, e => e.Task);
            var results5 = repository.Query().Select(e => e.Id, e => e.Task)
                          .Where(c => c.Id > 0 && c.Id < 7);
            var results6 = repository.Query()
                           .Select(e => e.Id, e => e.Task)
                          .Where(c => c.Id > 10 && c.Id < 15);
            var results2 = results.Union(new List<UnionSql> {
                     UnionSql.New(  results5,UnionType.Union ),
                     UnionSql.New(  results6,UnionType.Union )  })
```
Generates the following SQL statement and maps the results back to the list of ToDo objects.
``` sql
SELECT [_this_is_union].[Id]
, [_this_is_union].[Task]
FROM ( SELECT [dbo].[ToDo].[Id]
, [dbo].[ToDo].[Task]
FROM [dbo].[ToDo]
WHERE ((([dbo].[ToDo].[Id] > 0) and ([dbo].[ToDo].[Id] < 7)))
UNION
 SELECT [dbo].[ToDo].[Id]
, [dbo].[ToDo].[Task]
FROM [dbo].[ToDo]
WHERE ((([dbo].[ToDo].[Id] > 10) and ([dbo].[ToDo].[Id] < 15))) )
AS  _this_is_union
```
## Join
```
 var repository = MsSqlRepoFactory.Create<ToDo>();
 var results2 = repository.Query().Select(c=>c.Id,c=>c.Task,c=>c.Remark)
                           .LeftOuterJoin<TaskRemark>()
			   // add additional conditions. If the main selection has this property, query what is set in this sentence
                           .On<TaskRemark>((r, l) => r.Task == l.Task, l => l.Remark).Go();
 
```
Generates the following SQL statement and maps the results back to the list of ToDo objects.</br>

``` sql

SELECT [dbo].[ToDo].[Id]
, [dbo].[ToDo].[Task]
, [dbo].[TaskRemark].[Remark]
FROM [dbo].[ToDo]
LEFT OUTER JOIN [dbo].[TaskRemark]
ON [dbo].[ToDo].[Task] = [dbo].[TaskRemark].[Task];
```

## For   Dapper </br>
(Notice, Only Insert,Update Support   ParamSqlWithEntity() and ParamSql() method）</br>
```
	var repository = MySqlRepoFactory.Create<ToDo>();
            var results1 = repository.Query().Where(c => c.Id == 2).Go().FirstOrDefault();
            results1.Task = "B21";
            var rest2=   repository.Update().For(results1);
            var rest3 = rest2.ParamSqlWithEntity();
	    // Get  IDbConnection
            IDbConnection dbConnection = repository.GetConnectionProvider.GetDbConnection;
	    // Use Dapper
            dbConnection.Execute(rest3.paramsql, rest3.entity);
```
Generates the following SQL statement and maps the results back to the list of ToDo objects.
``` sql
UPDATE  `ToDo`
SET CreatedDate  = @CreatedDate, IsCompleted  = @IsCompleted, Task  = @Task
WHERE Id  = @Id;
```
## For Inline access Dapper

```
           string ConnectionString = "datasource=127.0.0.1;username=test;password=test;database=sqlrepotest;charset=gb2312;SslMode = none;";
            var connectionProvider = new  ConnectionStringConnectionProvider(ConnectionString);
            MySqlRepoFactory.UseConnectionProvider(connectionProvider);
            MySqlRepoFactory.UseStatementExecutor(new DapperStatementExecutor(connectionProvider));
            MySqlRepoFactory.UseDataReaderEntityMapper(new DapperEntityMapper());
            MySqlRepoFactory.UseWritablePropertyMatcher(new SimpleWritablePropertyMatcher());
```
