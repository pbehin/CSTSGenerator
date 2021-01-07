using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Typewriter.CodeModel;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynPropertyMetadata : IPropertyMetadata
    {
        public Func<string, string> TypeScriptNameFunc { get; }
        private readonly IPropertySymbol symbol;

        private RoslynPropertyMetadata(IPropertySymbol symbol, Func<string, string> typeScriptNameFunc)
        {
            TypeScriptNameFunc = typeScriptNameFunc;
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes(), TypeScriptNameFunc);
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.Type, TypeScriptNameFunc);
        public bool IsAbstract => symbol.IsAbstract;
        public bool IsStatic => symbol.IsStatic;
        public AccessModifier AccessModifiers => symbol.DeclaredAccessibility.ToAccessModifier();
        public bool HasGetter => symbol.GetMethod != null && symbol.GetMethod.DeclaredAccessibility == Accessibility.Public;
        public bool HasSetter => symbol.SetMethod != null && symbol.SetMethod.DeclaredAccessibility == Accessibility.Public;
        
        public static IEnumerable<IPropertyMetadata> FromPropertySymbol(IEnumerable<IPropertySymbol> symbols, Func<string, string> typeScriptNameFunc)
        {
            return symbols.Select(p => new RoslynPropertyMetadata(p, typeScriptNameFunc));
        }
    }
}