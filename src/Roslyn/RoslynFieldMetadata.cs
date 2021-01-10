using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynFieldMetadata : IFieldMetadata
    {
        public Func<string, string, string> TypeScriptNameFunc { get; }
        private readonly IFieldSymbol symbol;

        protected RoslynFieldMetadata(IFieldSymbol symbol, Func<string, string, string> typeScriptNameFunc)
        {
            TypeScriptNameFunc = typeScriptNameFunc;
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes(), TypeScriptNameFunc);
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.Type, TypeScriptNameFunc);

        public static IEnumerable<IFieldMetadata> FromFieldSymbols(IEnumerable<IFieldSymbol> symbols, Func<string, string, string> typeScriptNameFunc)
        {
            return symbols.Where(s => s.DeclaredAccessibility == Accessibility.Public && s.IsConst == false && s.IsStatic == false).Select(s => new RoslynFieldMetadata(s,typeScriptNameFunc));
        }
    }
}