using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ns.CodeGen;

namespace Ns.StitchFramework.Configuration
{
    public class StitchConfigure
    {
        private readonly List<StitchBuilder> builders = new List<StitchBuilder>();
        private readonly CSharpCompiler compiler;
        private readonly List<Type> proxiesRegistered = new List<Type>();
        
        static StitchConfigure()
        {
            ValueConfigure.ApplyDefaults();
        }

        public StitchConfigure()
        {
            this.compiler = new CSharpCompiler();
            this.compiler.Begin();
        }

        public StitchConfigure Value<From, To>(Expression<Func<From, To>> expression)
        {
            Ns.Value<From, To>.Configure(expression);
            return this;
        }
        
        public StitchConfigure RegisterProxy<TProxy>()
        {
            if (this.proxiesRegistered.Contains(typeof(TProxy)))
            {
                return this;
            }
           
            this.proxiesRegistered.Add(typeof(TProxy));
            Proxy<TProxy>.Build(this.compiler);
            return this;
        }
       
        public StitchBuilder<Source, Destination> Register<Source, Destination>()
        {
            RegisterProxy<Source>();
            RegisterProxy<Destination>();

            if (this.builders.OfType<StitchBuilder<Source, Destination>>().Any())
            {
                throw new StitchAlreadyConfiguredException(typeof(Source), typeof(Destination));
            }
            
            var builder = new StitchBuilder<Source, Destination>();
            this.builders.Add(builder);
            return builder;
        }

        internal void Build()
        {
            foreach (var builder in this.builders)
            {
                builder.Build(this.compiler);
            }

            this.compiler.Compile();
        }
    }
}
