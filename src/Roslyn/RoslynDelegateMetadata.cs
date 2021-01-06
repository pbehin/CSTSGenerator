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
        private readonly INamedTypeSymbol symbol;
        private readonly IMethodSymbol methodSymbol;

        public RoslynDelegateMetadata(INamedTypeSymbol symbol)
        {
            this.symbol = symbol;
            this.methodSymbol = symbol.DelegateInvokeMethod;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.GetFullName();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes());
        public ITypeMetadata Type => methodSymbol == null ? null : RoslynTypeMetadata.FromTypeSymbol(methodSymbol.ReturnType);
        public bool IsAbstract => false;
        public bool IsGeneric => symbol.TypeParameters.Any();
        public AccessModifier AccessModifiers => symbol.DeclaredAccessibility.ToAccessModifier();
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(symbol.TypeParameters);
        public IEnumerable<IParameterMetadata> Parameters => methodSymbol == null ? new IParameterMetadata[0] : RoslynParameterMetadata.FromParameterSymbols(methodSymbol.Parameters);

        public static IEnumerable<IDelegateMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols)
        {
            return symbols.Select(s => new RoslynDelegateMetadata(s));
        }
    }
}