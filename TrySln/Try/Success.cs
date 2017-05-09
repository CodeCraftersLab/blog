using System;
namespace Try
{
    public class Success<T> : ITry<T>
    {
        public Success(T newValue)
        {
            Value = newValue;
        }

        public T Value { get; private set; }

        public ITry<U> Select<U>(Func<T, ITry<U>> mapper)
        {
            if (mapper == null)
            {
                return new Failure<U>(new ArgumentNullException(nameof(mapper)));
            }
            try
            {
                return mapper(Value);
            }
            catch (Exception ex)
            {
                return ex.ToFailed<U>();
            }
        }
    }
}
