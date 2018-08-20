using System;
using System.Threading.Tasks;
using SqlRepoEx.Abstractions;

namespace SqlRepoEx.SqlServer.Abstractions {
    public interface ISqlConnection : IConnection
    {
        void Open();
        ISqlCommand CreateCommand();

        Task OpenAsync();
    }
}