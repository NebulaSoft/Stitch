using System;
using System.Linq.Expressions;
using Ns.StitchFramework;

namespace Ns
{
    public static class Value
    {
        public static LambdaExpression GetConvertExpression(Type from, Type to)
        {
            var type = typeof(Value<,>).MakeGenericType(from, to);
            var member = type.GetProperty( "Expression")?.GetGetMethod();
            var result = (LambdaExpression) member.Invoke(null, null);
            if (result == null)
            {
                throw new StitchValueNotRegisteredException(from, to);
            }
            return result;
        }
    }
    
    public static class Value<From, To>
    {
        public static Expression<Func<From, To>>? Expression { get; private set; }

        private static readonly Func<From, To> DefaultConverter = t => throw new StitchValueNotRegisteredException(typeof(From), typeof(To));
        
        internal static void Configure(Expression<Func<From, To>>? value)
        {
            Expression = value;
            var hasValue = value != null;
            Convert =  hasValue ? 
                value!.Compile() : DefaultConverter;
            IsRegistered = hasValue;
        }

        public static Func<From, To> Convert { get; private set; } = DefaultConverter;

        public static bool IsRegistered { get; private set; } 
    }
}
