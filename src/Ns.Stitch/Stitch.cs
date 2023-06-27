using System;
using Ns.StitchFramework;
using Ns.CodeGen;
using Ns.StitchFramework.Configuration;

namespace Ns
{
    public static class Stitch<Destination, Source>
    {
        public delegate Destination FromDelegate(Source source);

        public static FromDelegate From = source => throw new StitchTypeNotRegisteredException(typeof(Destination), typeof(Source));
    }

    public static class Stitch
    {
        public static void Configuration(Action<StitchConfigure> configure)
        {
            var compiler = new CSharpCompiler();
            var config = new StitchConfigure();
            compiler.Begin();
            configure(config);
            config.Build();
            compiler.Compile();
        } 
    }
}
