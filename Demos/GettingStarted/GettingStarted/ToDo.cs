using SqlRepoEx.Model;
using SqlRepoEx.SqlServer.CustomAttribute;
using System;
using System.ComponentModel.DataAnnotations;

namespace GettingStartedIoC
{
    [TableName("ToDo")]
    public class ToDo_New : Entity<int>
    {
        [NonDatabaseField]
        public DateTime CreatedDate { get; set; }


        public bool IsCompleted { get; set; }
        public string Task { get; set; }
    }

    [TableName("DoitTest")]
    public class DoitTest_New
    {
        [IdentityFiled]
        public int TestId { get; set; }

        public string TestRmk { get; set; }


        public bool TestBool { get; set; }

        [NonDatabaseField]
        public string tablestr { get; set; }
    }

}