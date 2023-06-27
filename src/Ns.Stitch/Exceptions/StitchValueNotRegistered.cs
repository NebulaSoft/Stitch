using System;

namespace Ns.StitchFramework
{
    public sealed class StitchValueNotRegisteredException : StitchException
    {
        public StitchValueNotRegisteredException(Type from, Type to)
            : base(
                $"The type from '{from.Name}' to '{to.Name}' is not a registered value converter.  Please register the value converter during application startup.")
        {
        }
    }
}
