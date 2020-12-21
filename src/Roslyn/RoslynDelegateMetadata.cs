using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
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
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(symbol.TypeParameters);
        public IEnumerable<IParameterMetadata> Parameters => methodSymbol == null ? new IParameterMetadata[0] : RoslynParameterMetadata.FromParameterSymbols(methodSymbol.Parameters);

        internal static IEnumerable<IDelegateMetadata> FromPublicNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols)
        {
            return FromNamedTypeSymbols(symbols, new[] { Accessibility.Public });
        }

        internal static IEnumerable<IDelegateMetadata> FromAllNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols)
        {
            var all = Enum.GetValues(typeof(Accessibility)).Cast<Accessibility>();
            return FromNamedTypeSymbols(symbols, all);
        }

        public static IEnumerable<IDelegateMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols,
            IEnumerable<Accessibility> declaredAccessibilities
            )
        {
            return symbols.Where(s => declaredAccessibilities.Contains(s.DeclaredAccessibility)).Select(s => new RoslynDelegateMetadata(s));
        }
    }
}