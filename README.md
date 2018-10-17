2.2.0

For Dapper
```
       var repository = MsSqlRepoFactory.Create<ToDo>();
            var results = repository.Query().Where(c => c.Id == 6).Go().FirstOrDefault();
            ToDo toDo = new ToDo();
            toDo.Task = "Atk";
            var resultinsert = repository.Insert().For(results);//.With(c => c.Task, "nkk");
            Console.WriteLine(resultinsert.ParamSql());
            var v = resultinsert.ParamSqlWithEntity();
            Console.WriteLine(v.paramsql);
```
```
INSERT  ToDo ( CreatedDate , IsCompleted , Task )

VALUES(@CreatedDate,@IsCompleted,@Task);
```
var resultinsert = repository.Update().For(results);
```
 UPDATE   ToDo
SET CreatedDate  = @CreatedDate, IsCompleted  = @IsCompleted, Task  = @Task
WHERE Id  = @Id;
```
# 本项目是在 SqlRepo 之上进行的二次开发
 
## 主要解决：<br>
 
1、解决拼接语句，使用where以外方法时，缺少Where子句时的错误；<br>
2、解决多条件拼接Where；<br>
3、增加操作时，不再受限于实例必需有Id的自增自段<br>


## Example
``` c#
IRepository<ToDo> repository = repositoryFactory.Create<ToDo>();
            var results = repository.Query()
                         .Select(e => e.Id, e => e.Task, e => e.CreatedDate);
 results = results.Where(e => e.IsCompleted == false);
 results = results.Where(e => e.Id == 3);
```
未改之前
``` sql

SELECT [dbo].[ToDo].[Id]
, [dbo].[ToDo].[Task]
, [dbo].[ToDo].[CreatedDate]
FROM [dbo].[ToDo]
WHERE ([dbo].[ToDo].[IsCompleted] = 0)
WHERE ([dbo].[ToDo].[Id] = 3);

```
更改后
``` sql

SELECT [dbo].[ToDo].[Id]
, [dbo].[ToDo].[Task]
, [dbo].[ToDo].[CreatedDate]
FROM [dbo].[ToDo]
WHERE ([dbo].[ToDo].[IsCompleted] = 0)
And ([dbo].[ToDo].[Id] = 3);

```
指定非自增字段
``` C#
var repository = this.repositoryFactory.Create<DoitTest>();
DoitTest doitTest = new DoitTest();

doitTest.TestRmk = "测试";
doitTest.TestBool = true;
doitTest.TestId = 123;
Console.WriteLine(repository.Insert().UsingIdField(d => d.TestId, false).For(doitTest).Sql());
```
生成的SQL
``` SQL
INSERT [dbo].[DoitTest]([TestId], [TestRmk], [TestBool])
VALUES(123, '测试', 1);

```
指定非自增字段
``` C#
var repository = this.repositoryFactory.Create<DoitTest>();
DoitTest doitTest = new DoitTest();

doitTest.TestRmk = "测试";
doitTest.TestBool = true;
doitTest.TestId = 123;
Console.WriteLine(repository.Insert().UsingIdField(d => d.TestId).For(doitTest).Sql());
```
生成的SQL
``` SQL
INSERT [dbo].[DoitTest]([TestRmk], [TestBool])
VALUES('测试', 1);
SELECT *
FROM [dbo].[DoitTest]
WHERE [TestId] = SCOPE_IDENTITY();
```
 
原项目中的例子：
```csharp

public class GettingStarted
{
    private IRepositoryFactory repositoryFactory;

    public GettingStarted(IRepositoryFactory repositoryFactory)
    {
        this.repositoryFactory = repositoryFactory;
    }

    public void DoIt()
    {
         var repository = this.repositoryFactory.Create<ToDo>();
         var results = repository.Query()
         .Select(e => e.Id, e => e.Task, e => e.CreatedDate)
         .Where(e => e.IsCompleted == false)
         .Go();
    }
}

```
Generates the following SQL statement and maps the results back to the list of ToDo objects.

```sql

SELECT [dbo].[ToDo].[Id], [dbo].[ToDo].[Task], [dbo].[ToDo].[CreatedDate]
FROM [dbo].[ToDo]
WHERE [dbo].[ToDo].[IsCompleted] = 0;

```
2018-9-25增加分页操作<br/>
```csharp
 var repository = RepoFactory.Create<ToDo>();
            var results = repository.Query()
                                    .Select(e => e.Id, e => e.Task, e => e.CreatedDate)
                                    .OrderBy(e => e.Id)
                                    .Page(10, 3)
                                    .Go();
```
2018-9-25增加存储OUTPUT参数返回<br/>
