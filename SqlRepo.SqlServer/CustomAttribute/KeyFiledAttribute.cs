using System;
using System.Collections.Generic;
using System.Text;

namespace SqlRepoEx.SqlServer.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class KeyFiledAttribute : Attribute
    {

        public KeyFiledAttribute()
        {
            
        }
 
    }
}
