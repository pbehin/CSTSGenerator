using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynEventMetadata : IEventMetadata
    {
        public Func<string, string, string> TypeScriptNameFunc { get; }
        private readonly IEventSymbol symbol;

        public RoslynEventMetadata(IEventSymbol symbol, Func<string, string, string> typeScriptNameFunc)
        {
            TypeScriptNameFunc = typeScriptNameFunc;
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes(), TypeScriptNameFunc);
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.Type, TypeScriptNameFunc);

        public static IEnumerable<IEventMetadata> FromEventSymbols(IEnumerable<IEventSymbol> symbols, Func<string, string, string> typeScriptNameFunc)
        {
            return symbols.Where(s => s.DeclaredAccessibility == Accessibility.Public && s.IsStatic == false).Select(s => new RoslynEventMetadata(s, typeScriptNameFunc));
        }
    }
}
