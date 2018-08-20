using System;

namespace SqlRepoEx.Abstractions
{
    public interface IWritablePropertyMatcher {
        bool Test(Type type);
    }
}