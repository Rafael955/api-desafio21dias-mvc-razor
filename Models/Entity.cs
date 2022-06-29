using System;

public abstract class Entity<T> where T : IConvertible, IComparable
{
    public T Id { get; set; }
}