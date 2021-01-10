using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Typewriter.CodeModel;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynDelegateMetadata : IDelegateMetadata
    {
        public Func<string, string, string> TypeScriptNameFunc { get; }
        private readonly INamedTypeSymbol symbol;
        private readonly IMethodSymbol methodSymbol;

        public RoslynDelegateMetadata(INamedTypeSymbol symbol, Func<string, string, string> typeScriptNameFunc)
        {
            TypeScriptNameFunc = typeScriptNameFunc;
            this.symbol = symbol;
            this.methodSymbol = symbol.DelegateInvokeMethod;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.GetFullName();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes(), TypeScriptNameFunc);
        public ITypeMetadata Type => methodSymbol == null ? null : RoslynTypeMetadata.FromTypeSymbol(methodSymbol.ReturnType, TypeScriptNameFunc);
        public bool IsAbstract => false;
        public bool IsGeneric => symbol.TypeParameters.Any();
        public AccessModifier AccessModifiers => symbol.DeclaredAccessibility.ToAccessModifier();
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(symbol.TypeParameters);
        public IEnumerable<IParameterMetadata> Parameters => methodSymbol == null ? new IParameterMetadata[0] : RoslynParameterMetadata.FromParameterSymbols(methodSymbol.Parameters, TypeScriptNameFunc);

        public static IEnumerable<IDelegateMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols, Func<string, string, string> typeScriptNameFunc)
        {
            return symbols.Select(s => new RoslynDelegateMetadata(s, typeScriptNameFunc));
        }
    }
}