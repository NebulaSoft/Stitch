using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionToCodeLib;
using Ns.CodeGen;

namespace Ns.StitchFramework.Configuration
{
    public class StitchBuilder<Source, Destination> : StitchBuilder
    {
        private readonly List<string> destinationProperties =
            typeof(Destination).GetProperties().Select(p => p.Name).ToList();

        private readonly List<string> sourceProperties = typeof(Source).GetProperties().Select(p => p.Name).ToList();

        internal StitchBuilder()
        {
        }

        public StitchBuilder<Source, Destination> Property<T>(Expression<Func<Destination, T>> to,
            Expression<Func<Source, T>> from)
        {
            if ((from.Body as MemberExpression) == null)
            {
                var rename = from.RenameParameter(from.Parameters[0].Name, "source");
                this.assigmentViaExpressions.Add(ExpressionToCode.GetNameIn(to), rename);
            }
            else
            {
                this.directAssignments.Add(ExpressionToCode.GetNameIn(to), ExpressionToCode.GetNameIn(from));                
            }
            return this;
        }

        public StitchBuilder<Source, Destination> Ignore<T>(Expression<Func<Destination, T>> to)
        {
            this.ignoreDestinations.Add(ExpressionToCode.GetNameIn(to));
            return this;
        }

        public StitchBuilder<Source, Destination> IgnoreSource<T>(Expression<Func<Source, T>> from)
        {
            this.ignoreSources.Add(ExpressionToCode.GetNameIn(from));
            return this;
        }

        public StitchBuilder<Source, Destination> AutoMap()
        {
            foreach (var property in this.sourceProperties.Where(property => this.destinationProperties.Contains(property)))
            {
                this.directAssignments.Add(property, property);
            }

            return this;
        }

        protected override void ValidateProperties()
        {
            this.destinationProperties.RemoveAll(v => this.directAssignments.Keys.Contains(v));
            this.destinationProperties.RemoveAll(v => this.ignoreDestinations.Contains(v));
            this.destinationProperties.RemoveAll(v => this.assigmentViaExpressions.Keys.Contains(v));
            if (this.destinationProperties.Count > 0)
            {
                throw new StitchPropertyNotRegisteredException(typeof(Destination), typeof(Source), this.destinationProperties);
            }

            this.sourceProperties.RemoveAll(v => this.directAssignments.Values.Contains(v));
            this.sourceProperties.RemoveAll(v => this.ignoreSources.Contains(v));
            if (this.sourceProperties.Count > 0)
            {
                throw new StitchPropertyNotRegisteredException(typeof(Destination), typeof(Source), this.sourceProperties);
            }
        }

        protected override void BuildStitch(CSharpCompiler compiler)
        {
            var code = new CSharpClassDefinition(string.Concat(typeof(Destination).Name, typeof(Source).Name, "Stitch"), typeof(Destination).Namespace);

            var codeBlock = new CodeBlockBuilder();

            AddDirectPropertyAssignments(codeBlock);
            AddExpressionAssignments(codeBlock);
            AddProxySetup(codeBlock);

            const string methodName = "Stitch";
            
            code.AddMethod<Destination>(methodName, codeBlock, new ArgumentDefinition(typeof(Source), "source"));

            code.SendToCompiler(compiler, 
                (assembly, type) => 
                    Stitch<Destination, Source>.From = 
                        (Stitch<Destination, Source>.FromDelegate) Delegate.CreateDelegate(typeof(Stitch<Destination, Source>.FromDelegate),null, type.GetMethod(methodName)));
        }

        private void AddProxySetup(CodeBlockBuilder codeBlock)
        {
            var arguments = string.Join(", ", Proxy<Destination>.ConstructorArgumentOrder);
            codeBlock.AddLine($"return new {Proxy<Destination>.FullName}({arguments})");
        }

        private void AddExpressionAssignments(CodeBlockBuilder codeBlock)
        {
            foreach (var (key, value) in this.assigmentViaExpressions)
            {
                codeBlock.AddExpression($"var {key} = ", value);
            }
        }

        private void AddDirectPropertyAssignments(CodeBlockBuilder codeBlock)
        {
            foreach (var (key, value) in this.directAssignments)
            {
                var to = typeof(Destination).GetProperty(key).GetGetMethod().ReturnType;
                var from = typeof(Source).GetProperty(value).GetGetMethod().ReturnType;
                var expression = Value.GetConvertExpression(from, to);
                var rename = expression.RenameParameter(expression.Parameters[0].Name, "source." + value);
                codeBlock.AddExpression( $"var {key} = ", rename);
            }
        }
    }
}
