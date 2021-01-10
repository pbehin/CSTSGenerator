using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.CodeAnalysis;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynEnumValueMetadata : IEnumValueMetadata
    {
        public Func<string, string, string> TypeScriptNameFunc { get; }
        private static readonly Int64Converter _converter = new Int64Converter();

        private readonly IFieldSymbol symbol;

        private RoslynEnumValueMetadata(IFieldSymbol symbol, Func<string, string, string> typeScriptNameFunc)
        {
            TypeScriptNameFunc = typeScriptNameFunc;
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes(), TypeScriptNameFunc);
        public long Value => (long?)_converter.ConvertFromString(symbol.ConstantValue.ToString().Trim('\'')) ?? -1;

        internal static IEnumerable<IEnumValueMetadata> FromFieldSymbols(IEnumerable<IFieldSymbol> symbols, Func<string, string, string> typeScriptNameFunc)
        {
            return symbols.Select(s => new RoslynEnumValueMetadata(s,typeScriptNameFunc));
        }
    }
}