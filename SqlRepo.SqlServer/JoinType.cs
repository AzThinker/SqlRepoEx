﻿namespace SqlRepoEx.SqlServer
{
    public enum JoinType
    {
        None = 0,
        Inner,
        LeftOuter,
        RightOuter,
        Full,
        Cross
    }
}