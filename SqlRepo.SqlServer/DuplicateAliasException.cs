﻿using System;

namespace SqlRepoEx.SqlServer
{
    public class DuplicateAliasException : Exception
    {
        public DuplicateAliasException()
            : base("The alias is already in use in the statement.") {}
    }
}