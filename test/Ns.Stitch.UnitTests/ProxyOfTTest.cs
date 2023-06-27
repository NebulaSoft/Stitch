using System;
using System.Reflection;
using FluentAssertions;
using Ns.StitchFramework;
using Ns.CodeGen;
using Scenarios;
using Xunit;
using static FluentAssertions.FluentActions;

namespace Ns.StitchTests
{
        
    public class ProxyOfTTest
    {
        private interface ITest
        {
        
        }

        public class TheReCompileMethod
        {
            [Theory]
            [InlineData(typeof(IReadonlyContract))]
            [InlineData(typeof(IReadWriteContract))]
            [InlineData(typeof(INoNameSpace))]
            [InlineData(typeof(IEmpty))]
            [InlineData(typeof(IInsideNamespace))]
            [InlineData(typeof(NestedClass.IInsideClassInheriting))]
            [InlineData(typeof(NestedClass.IInsideClass))]
            public void Should_Compile_With_NoExceptions(Type type)
            {
                var method = GetProxyMethod(type, nameof(Proxy<IReadonlyContract>.Build));
                
                Action sut = () =>
                {
                    var compiler = new CSharpCompiler();
                    compiler.Begin();
                    method.Invoke(null, new object[] {compiler});
                    compiler.Compile();
                };

                sut
                    .Should()
                    .NotThrow();
            }

            private static MethodInfo GetProxyMethod(Type type, string name) => typeof(Proxy<>)
                .MakeGenericType(type)
                .GetMethod(name);

            private static PropertyInfo GetProxyProperty(Type type, string name) => typeof(Proxy<>)
                .MakeGenericType(type)
                .GetProperty(name);

            [Fact]
            public void Should_ThrowNotSupportedException_When_MethodsPresent()
            {
                var method = GetProxyMethod(typeof(IHaveAMethod), nameof(Proxy<IHaveAMethod>.Build));

                Invoking(() =>
                    {
                        var compiler = new CSharpCompiler();
                        compiler.Begin();
                        method.Invoke(null, new object[] {compiler});
                        compiler.Compile();
                    })
                .Should()
                .Throw<TargetInvocationException>()
                    .WithInnerException<TypeInitializationException>()
                    .WithInnerException<StitchInvalidTypeException>();
            }

            [Fact]
            public void Should_ThrowNotSupportedException_When_ClassUsed()
            {
                Invoking(() =>
                {
                    var method = GetProxyMethod(typeof(TheReCompileMethod), nameof(Proxy<TheReCompileMethod>.Build));

                    var compiler = new CSharpCompiler();
                    compiler.Begin();
                    method.Invoke(null, new object[] {compiler});
                    compiler.Compile();
                })
                .Should()
                .Throw<TargetInvocationException>()
                .WithInnerException<TypeInitializationException>()
                .WithInnerException<StitchInvalidTypeException>();
            }
            
            [Fact]
            public void Should_Compile_Then_ConstructorAccessible()
            {
                var type = typeof(IReadonlyContract);
                var compiler = new CSharpCompiler();
                compiler.Begin();

                GetProxyMethod(type, nameof(Proxy<IReadonlyContract>.Build)).Invoke(null, new object[] {compiler});
                var method = GetProxyProperty(type, nameof(Proxy<IReadonlyContract>.Constructor));
                compiler.Compile();
                var result = method.GetValue(null);
                result
                    .Should()
                    .NotBeNull();
            }
        }
    }
}
