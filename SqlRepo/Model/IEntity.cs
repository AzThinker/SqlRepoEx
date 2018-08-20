using System;

namespace SqlRepoEx.Model
{
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }

    public interface IEntity
    {
        bool IsTransient();
    }
}