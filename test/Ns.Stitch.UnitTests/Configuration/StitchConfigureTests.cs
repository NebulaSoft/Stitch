using System;
using FluentAssertions;
using Ns.StitchFramework;
using Ns.StitchTests.Stubs;
using Xunit;

namespace Ns.StitchTests.Configuration
{
    public class StitchConfigureTests
    {
        public class TheRegisterMethod
        {
            [Fact]
            public void Should_Throw_StitchAlreadyConfiguredException()
            {
                Action act = () => Stitch.Configuration(c =>
                {
                    c.Register<Event, Command>();
                    c.Register<Event, Command>();
                });

                act.Should().ThrowExactly<StitchAlreadyConfiguredException>();

            }
            
            [Fact]
            public void Should_ThrowStitchPropertyNotRegisteredException_WhenPropertiesNotRegisteredOnSource()
            {
                Action act = () => Stitch.Configuration(c =>
                {
                    c.Register<Event, Command>()
                        .Ignore(e => e.Name)
                        .Ignore(e => e.Submitted);
                });

                act.Should().ThrowExactly<StitchPropertyNotRegisteredException>();
            }
            
            [Fact]
            public void Should_ThrowStitchPropertyNotRegisteredException_WhenPropertiesNotRegisteredOnDestination()
            {
                Action act = () => Stitch.Configuration(c =>
                {
                    c.Register<Command, Event>()
                        .IgnoreSource(e => e.Name)
                        .IgnoreSource(e => e.Submitted);

                });

                act.Should().ThrowExactly<StitchPropertyNotRegisteredException>();
            }
        }
    }
}
