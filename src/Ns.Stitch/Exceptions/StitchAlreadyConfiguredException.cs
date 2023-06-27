using System;

namespace Ns.StitchFramework
{
    public sealed class StitchAlreadyConfiguredException : StitchException
    {
        public StitchAlreadyConfiguredException(Type source, Type destination) : base($"Stitch for types {source.Name} and {destination.Name} already configured.")
        {
            this.Data.Add("Source", source);
            this.Data.Add("Destination", destination);
        }
    }
}
