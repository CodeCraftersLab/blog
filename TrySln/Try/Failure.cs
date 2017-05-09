using System;
namespace Try
{
    public class Failure<T> : ITry<T>
    {
        private readonly Exception ex;

        public Failure(Exception ex)
        {
            this.ex = ex;
        }

        public T Value
        {
            get { throw new InvalidOperationException("Can't get value for failed Try"); }
        }

        public ITry<U> Select<U>(Func<T, ITry<U>> mapper)
        {
            return ex.ToFailed<U>();
        }

        public Exception GetException()
        {
            return ex;
        }
    }
}
