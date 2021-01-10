using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynConstantMetadata : RoslynFieldMetadata, IConstantMetadata
    {
        private readonly IFieldSymbol symbol;

        private RoslynConstantMetadata(IFieldSymbol symbol, Func<string, string, string> typeScriptNameFunc) : base(symbol, typeScriptNameFunc)
        {
            this.symbol = symbol;
        }

        public string Value => $"{symbol.ConstantValue}";

        public new static IEnumerable<IConstantMetadata> FromFieldSymbols(IEnumerable<IFieldSymbol> symbols, Func<string, string, string> typeScriptNameFunc)
        {
            return symbols.Where(s =>s.IsConst).Select(s => new RoslynConstantMetadata(s, typeScriptNameFunc));
        }
    }
}