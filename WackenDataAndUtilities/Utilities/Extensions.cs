using System;

namespace WackenDataAndUtilities.Utilities
{
    public static class Extensions
    {
        public static bool IsNull<T>(this T instance) where T : class
        {
            return instance == null;
        }

        public static bool NotNull<T>(this T instance) where T : class
        {
            return instance != null;
        }

        public static TResult IfNotNull<T, TResult>(this T instance, Func<T, TResult> func) where T : class

        {
            return instance.IsNull() ? default(TResult) : func(instance);
        }
    }
}