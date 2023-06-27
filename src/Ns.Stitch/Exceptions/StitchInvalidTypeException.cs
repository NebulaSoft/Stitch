using System;

namespace Ns.StitchFramework
{
    public sealed class StitchInvalidTypeException : StitchException
    {
        public StitchInvalidTypeException(Type t)
            : base(
                $"The type '{t.Name}' is not valid, only interfaces with no methods are supported.")
        {
            Data.Add("Type", t);
        }
    }
}
