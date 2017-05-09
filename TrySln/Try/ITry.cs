using System;
namespace Try
{
    public interface ITry<T>
    {
        T Value { get; }

        ITry<U> Select<U>(Func<T, ITry<U>> mapper);
    }
}
