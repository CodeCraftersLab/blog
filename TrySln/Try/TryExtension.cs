using System;
namespace Try
{
    public static class TryExtension
    {
        public static ITry<T> ToSucess<T>(this T data)
        {
            return new Success<T>(data);
        }

        public static ITry<T> ToFailed<T>(this Exception ex)
        {
            return new Failure<T>(ex);
        }
    }
}
