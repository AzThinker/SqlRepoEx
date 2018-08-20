using SqlRepoEx.Model;
using System;

namespace GettingStartedIoC
{
    public class ToDo : Entity<int>
    {
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Task { get; set; }
    }

    public class DoitTest
    {
        public int TestId { get; set; }

        public string TestRmk { get; set; }


        public bool TestBool { get; set; }
    }

}