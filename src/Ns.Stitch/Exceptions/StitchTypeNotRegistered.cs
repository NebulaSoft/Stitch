using System;

namespace Ns.StitchFramework
{
    public sealed class StitchTypeNotRegisteredException : StitchException
    {
        public StitchTypeNotRegisteredException(Type from, Type to)
            : base(
                $"The type stitch from '{from.Name}' to '{to.Name}' is not registered.  Please call register during application startup.")
        {
        }
    }
}
