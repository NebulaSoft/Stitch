using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ns.CodeGen;
using Ns.StitchFramework;
// ReSharper disable PossibleMultipleEnumeration

// ReSharper disable once CheckNamespace
namespace Ns
{
    public static class Proxy<T>
    {
        private static readonly Type SourceType = typeof(T);

        public static readonly string Namespace = SourceType.Namespace;
        public static readonly string ClassName = GenerateClassName(SourceType.Name);
        private static IEnumerable<PropertyInfo> properties;

        public static ConstructorInfo? Constructor { get; private set; }
        public static IReadOnlyList<string> ConstructorArgumentOrder { get; private set; } 
        public static object FullName { get; } = Namespace + "." + ClassName;
     
        static Proxy()
        {
            if (!SourceType.IsInterface)
            {
                throw new StitchInvalidTypeException(SourceType);
            }

            properties = SetupProperties();

            var hasMethods = (from m in SourceType.GetMethods()
                where !SourceType.GetProperties().Any(p => p.GetGetMethod() == m || p.GetSetMethod() == m)
                select m).SingleOrDefault() != null;
            
            if (hasMethods)
            {
                throw new StitchInvalidTypeException(SourceType);
            }
            
            ConstructorArgumentOrder = properties.Select(p => p.Name).ToArray();
        }
        
        public static void Build(CSharpCompiler compiler)
        {
            var code = new CSharpClassDefinition(ClassName, Namespace);
          
            AppendProperties(code, properties);
            
            var arguments = properties
                .Select(p => new ArgumentDefinition(p.GetMethod.ReturnType, p.Name))
                .ToArray();
            
            code
                .Inherits(SourceType)
                .AddConstructor(GetConstructorCodeBlock(properties), arguments)
                .SendToCompiler(compiler, (a, t) =>
                    {
                        Constructor = t.GetConstructors().First();
                    });
        }

        private static IEnumerable<PropertyInfo> SetupProperties()
        {
            var result = new List<PropertyInfo>();
            result.AddRange(SourceType.GetProperties());
            foreach (var i in SourceType.GetInterfaces())
            {
                result.AddRange(i.GetProperties());
            }
            return result.OrderBy(p => p.Name);
        }

        private static SimpleCodeBlock GetConstructorCodeBlock(IEnumerable<PropertyInfo> properties)
        {
            var result = new StringBuilder();
            foreach (var property in properties)
            {
                result.AppendLine($"            this.{property.Name} = {property.Name};");
            }
            return new SimpleCodeBlock(result.ToString());
        }


        private static void AppendProperties(CSharpClassDefinition code, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                code.AddAutoProperty(property.GetMethod.ReturnType, property.Name, property.SetMethod == null);
            }
        }

        private static string GenerateClassName(string proxyTypeName)
        {
            const string classNameSuffix = "Proxy";
            if (proxyTypeName.StartsWith("I", StringComparison.InvariantCultureIgnoreCase))
            {
                proxyTypeName = proxyTypeName.Substring(1);
            }

            return $"{proxyTypeName}{classNameSuffix}";
        }
    }
}
