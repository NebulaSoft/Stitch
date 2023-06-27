using System;
using FluentAssertions;
using Ns.StitchFramework;
using Ns.StitchTests.Stubs;
using Xunit;

namespace Ns.StitchTests
{
    public class StitchTests
    {
        public class TheFromMethod
        {
            [Fact]
            public void Should_ThrowStitchTypeNotRegisteredException_WhenNotRegistered()
            {
                Action act = () => Stitch<StitchTests, StitchTests>.From(null);

                act.Should().Throw<StitchTypeNotRegisteredException>();
            }
            
            [Fact]
            public void Should_Convert_ViaExpression()
            {
                var source = new EventClass() { FORENAME = "Joe", SURNAME = "Blogs"};
                Stitch.Configuration(c =>
                {
                    c.Register<Event, Command>()
                        .Property(t => t.Name, s => s.FORENAME + " " + s.SURNAME)
                        .Property(t => t.Submitted, t => t.SUBMITTED)
                        //.IgnoreSource(f => f.SUBMITTED)
                        //.Ignore(f => f.Submitted)
                        .IgnoreSource(f => f.SURNAME)
                        .IgnoreSource(f => f.FORENAME);
                });

                var result = Stitch<Command, Event>.From(source);

                result.Name.Should().Be("Joe Blogs");
            }
            
            [Fact(Timeout = 1000)]
            public void ShouldNotTimeOut()
            {
                Stitch.Configuration(c =>
                {
                    c.Register<Event, Command>()
                        .AutoMap()
                        .Property(t => t.Name, source => source.FORENAME + " " + source.SURNAME)
                        .Property(t => t.Submitted, source => source.SUBMITTED)
                        .Ignore(f => f.Submitted)
                        .IgnoreSource(f => f.SURNAME)
                        .IgnoreSource(f => f.FORENAME);
                });
            
                var testEvent = new EventClass();
            
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                for (var i = 0; i < 1000000; i++)
                {
                    var command = Stitch<Command, Event>.From(testEvent);                
                }
                sw.Stop();
            }
            
            [Fact]
            public void Should_Convert_ViaAutoMap()
            {
                var source = new CommandClass() {Name = "Test", Submitted = DateTime.Now};
                Stitch.Configuration(c =>
                {
                    c.Register<Command, Command>()
                        .AutoMap();
                });

                var result = Stitch<Command, Command>.From(source);

                result.Name.Should().Be("Test");
            }

            [Fact]
            public void Should_Convert_ViaModifiedValueConverter()
            {
                var source = new CommandClass() {Name = "Test"};
                
                Stitch.Configuration(c =>
                {
                    c.Value<string, string>(v => v + v);
                    c.Register<Command, Command>()
                        .AutoMap();
                });

                var result = Stitch<Command, Command>.From(source);

                result.Name.Should().Be(source.Name + source.Name);
            }
        }
    }
}
