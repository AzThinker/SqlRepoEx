using System;
using SqlRepoEx.Model;

namespace GettingStartedStatic
{
    public class ToDo : Entity<int>
    {
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Task { get; set; }
    }

    public class TwoRemark : Entity<int>
    {
        public string Remark { get; set; }
        public string Task { get; set; }
    }

}