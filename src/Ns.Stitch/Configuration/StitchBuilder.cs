using System.Collections.Generic;
using System.Linq.Expressions;
using Ns.CodeGen;

namespace Ns.StitchFramework.Configuration
{
    public abstract class StitchBuilder
    {
        protected readonly Dictionary<string, LambdaExpression> assigmentViaExpressions = new Dictionary<string, LambdaExpression>();
        protected readonly Dictionary<string, string> directAssignments = new Dictionary<string, string>();
        protected readonly List<string> ignoreDestinations = new List<string>();
        protected readonly List<string> ignoreSources = new List<string>();

        internal void Build(CSharpCompiler compiler)
        {
            ValidateProperties();
            BuildStitch(compiler);
        }

        protected abstract void ValidateProperties();

        protected abstract void BuildStitch(CSharpCompiler compiler);
    }
}
