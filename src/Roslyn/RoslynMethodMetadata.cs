using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Typewriter.CodeModel;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynMethodMetadata : IMethodMetadata
    {
        public Func<string, string> TypeScriptNameFunc { get; }
        private IMethodSymbol symbol;

        public RoslynMethodMetadata(IMethodSymbol symbol, Func<string, string> typeScriptNameFunc)
        {
            TypeScriptNameFunc = typeScriptNameFunc;
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.GetFullName();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes(), TypeScriptNameFunc);
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.ReturnType, TypeScriptNameFunc);
        public bool IsAbstract => symbol.IsAbstract;
        public bool IsGeneric => symbol.IsGenericMethod;
        public AccessModifier AccessModifiers => symbol.DeclaredAccessibility.ToAccessModifier();
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(symbol.TypeParameters);
        public IEnumerable<IParameterMetadata> Parameters => RoslynParameterMetadata.FromParameterSymbols(symbol.Parameters, TypeScriptNameFunc);

        public static IEnumerable<IMethodMetadata> FromMethodSymbols(IEnumerable<IMethodSymbol> symbols, Func<string, string> typeScriptNameFunc)
        {
            return symbols.Where(s => s.DeclaredAccessibility == Accessibility.Public && s.MethodKind == MethodKind.Ordinary && s.IsStatic == false).Select(p => new RoslynMethodMetadata(p, typeScriptNameFunc));
        }
    }
}