using System;

namespace Ns.StitchFramework
{
    public abstract class StitchException : Exception
    {
        protected StitchException(string s) : base(s)
        {
        }
    }
}
