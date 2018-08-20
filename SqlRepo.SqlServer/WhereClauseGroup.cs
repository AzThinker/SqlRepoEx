using System.Collections.Generic;
using System.Linq;

namespace SqlRepoEx.SqlServer
{
    internal class WhereClauseGroup
    {
        public WhereClauseGroup()
        {
            this.Groups = new List<WhereClauseGroup>();
            this.Conditions = new List<WhereClauseCondition>();
        }

        public WhereClauseGroup Parent { get; set; }
        public WhereClauseGroupType GroupType { get; set; }
        public IList<WhereClauseGroup> Groups { get; set; }
        public IList<WhereClauseCondition> Conditions { get; set; }

        public override string ToString()
        {
            var nestedGroups = !this.Groups.Any()? string.Empty: $" {string.Join(" ", this.Groups)}"; 
            return
                $"{this.GroupType.ToString() .ToUpperInvariant()} ({string.Join(" ", this.Conditions)}{nestedGroups})";
        }
    }
}