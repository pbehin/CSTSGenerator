using System;
using Microsoft.CodeAnalysis;
using System.Linq;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynAttrubuteArgumentMetadata : IAttributeArgumentMetadata
    {
        private TypedConstant typeConstant;

        public RoslynAttrubuteArgumentMetadata(TypedConstant typeConstant, Func<string, string, string> typeScriptNameFunc)
        {
            this.typeConstant = typeConstant;
            this.TypeScriptNameFunc = typeScriptNameFunc;
        }

        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(typeConstant.Type, this.TypeScriptNameFunc);

        private Func<string, string, string> TypeScriptNameFunc { get; }

        public ITypeMetadata TypeValue => typeConstant.Kind == TypedConstantKind.Type ? RoslynTypeMetadata.FromTypeSymbol((INamedTypeSymbol)typeConstant.Value, TypeScriptNameFunc) : null;
        public object Value => typeConstant.Kind == TypedConstantKind.Array ? typeConstant.Values.Select(prop => prop.Value).ToArray() : typeConstant.Value;
    }
}