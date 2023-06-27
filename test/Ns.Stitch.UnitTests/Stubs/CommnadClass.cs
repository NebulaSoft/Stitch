using System;
using System.Diagnostics.CodeAnalysis;

namespace Ns.StitchTests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class CommandClass : Command
    {
        public string Name { get; set; }
        public DateTime Submitted { get; set; }
    }
}