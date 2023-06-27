using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Ns
{
    public static class New<T>
    {
        public static readonly Func<T> Instance = Activate();

        private static Func<T> Activate() => Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
