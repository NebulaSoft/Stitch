using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using Ns.StitchFramework;
using Xunit;
using static FluentAssertions.FluentActions;

namespace Ns.StitchTests
{
    public class ValueTypeOfTest
    {
        public class TheIsRegisteredProperty
        {
            private class A
            {
            }

            private class B
            {
            }

            [Fact]
            public void Should_ThrowStitchTypeNotRegisteredException_When_NotRegistered()
            {
                Invoking(() => Value<A, B>.Convert(new A()))
                .Should()
                .Throw<StitchValueNotRegisteredException>();
            }
        }

        public class TheGetConvertExpressionMethod
        {
            [Fact]
            public void Should_Throw_StitchValueNotRegisteredException()
            {
                Action act = () => Value.GetConvertExpression(typeof(TheGetConvertExpressionMethod),
                    typeof(TheGetConvertExpressionMethod));

                act.Should().Throw<StitchValueNotRegisteredException>();
            }
        }
        
        
        public class TheConvertProperty
        {
            [Fact]
            public void Should_Convert_WithExpectedResult()
            {
                Func<int, string> expression = i => i.ToString();
                var type = typeof(Value<int, string>);
                var prop = type.GetProperty("Convert");
                prop.SetValue(null, expression); // Setter is internal so access directly
                
                const int testValue = 99999;

                Invoking(() => Value<int, string>.Convert(testValue))
                    .Should()
                    .Equals(testValue.ToString());
            }
        }

        public class ThisIsRegisteredProperty
        {
            [Fact]
            public void Should_Return_True()
            {
                Expression<Func<int, string>> expression = i => i.ToString();
                var type = typeof(Value<int, string>);
                var method = type.GetMethod("Configure", BindingFlags.Static | BindingFlags.NonPublic );
                method.Invoke(null, new object[] { expression });
                var result = Value<int, string>.IsRegistered;

                result.Should().BeTrue();
            }
        }
    }
}
