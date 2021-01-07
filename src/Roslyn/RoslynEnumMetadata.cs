using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynEnumMetadata : IEnumMetadata
    {
        public Func<string, string> TypeScriptNameFunc { get; }
        private readonly INamedTypeSymbol _symbol;
        
        public RoslynEnumMetadata(INamedTypeSymbol symbol, Func<string, string> typeScriptNameFunc)
        {
            TypeScriptNameFunc = typeScriptNameFunc;
            _symbol = symbol;
        }

        public string DocComment => _symbol.GetDocumentationCommentXml();
        public string Name => _symbol.Name;
        public string FullName => _symbol.ToDisplayString();
        public string Namespace => _symbol.GetNamespace();

        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(_symbol, TypeScriptNameFunc);

        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(_symbol.GetAttributes(), TypeScriptNameFunc);
        public IClassMetadata ContainingClass => RoslynClassMetadata.FromNamedTypeSymbol(_symbol.ContainingType);
        public IEnumerable<IEnumValueMetadata> Values => RoslynEnumValueMetadata.FromFieldSymbols(_symbol.GetMembers().OfType<IFieldSymbol>(), TypeScriptNameFunc);
        
        internal static IEnumerable<IEnumMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols, Func<string, string> typeScriptNameFunc)
        {
            return symbols.Where(s => s.DeclaredAccessibility == Accessibility.Public).Select(s => new RoslynEnumMetadata(s, typeScriptNameFunc));
        }
    }
}