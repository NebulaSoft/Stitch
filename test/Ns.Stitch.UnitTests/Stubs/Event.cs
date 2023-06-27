using System;

namespace Ns.StitchTests.Stubs
{
    public interface Event
    {
        string FORENAME { get; }

        string SURNAME { get; }

        DateTimeOffset SUBMITTED { get; }
    }
}