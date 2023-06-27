using System;

namespace Ns.StitchTests.Stubs
{
    public interface Command
    {
        string Name { get; }

        DateTime Submitted { get; }
    }
}