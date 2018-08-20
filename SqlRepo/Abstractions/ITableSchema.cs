namespace SqlRepoEx.SqlServer.Abstractions
{
    public interface ITableSchema
    {
        string TableName { get; set; }


        string TableSchema { get; set; }

    }
}
