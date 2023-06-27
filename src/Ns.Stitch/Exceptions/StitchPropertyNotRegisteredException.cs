using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Ns.StitchFramework
{
    public sealed class StitchPropertyNotRegisteredException : StitchException
    {
        public StitchPropertyNotRegisteredException(Type destination, Type source, IEnumerable<string> names)
            : base(
                $"The following properties '{string.Join(',', names)}' are not registered in stitch '{source.Name}' to '{destination.Name}'.  Either ignore them, supply an expression or map this in stitch registration.")
        {
            this.Data.Add("Names", names);
            this.Data.Add("Source", source);
            this.Data.Add("Destination", destination);
        }
    }
}
