using FluentAssertions;
using Xunit;

namespace Ns.StitchTests
{
    public class NewTests
    {
        public struct TestStruct
        {
            public int MyNumber { get; set; }
        }

        public class TheInstanceMethod
        {
            [Fact]
            public void Should_Complete_ForClass()
            {
                New<NewTests>.Instance();
            }
            
            [Fact]
            public void Should_Complete_ForStruct()
            {
                New<TestStruct>.Instance();
            }
            
            [Fact]
            public void Should_AllowStructMemberUse()
            {
                var testValue = 1;
                var result = New<TestStruct>.Instance();
                result.MyNumber = 1;

                result.MyNumber.Should().Be(testValue);
            }
        }
    }
}
