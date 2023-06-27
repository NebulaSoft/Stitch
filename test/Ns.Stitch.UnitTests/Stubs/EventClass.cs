using System;
using System.Diagnostics.CodeAnalysis;

namespace Ns.StitchTests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class EventClass : Event
    {
        public string FORENAME { get; set; }

        public string SURNAME { get; set; }

        public DateTimeOffset SUBMITTED { get; set; }
    }
}