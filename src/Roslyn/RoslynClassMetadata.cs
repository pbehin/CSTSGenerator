using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Typewriter.CodeModel;
using Typewriter.Configuration;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.Roslyn
{
    public class RoslynClassMetadata : IClassMetadata
    {
        private readonly INamedTypeSymbol _symbol;
        private readonly RoslynFileMetadata _file;

        private RoslynClassMetadata(INamedTypeSymbol symbol, RoslynFileMetadata file)
        {
            _symbol = symbol;
            _file = file;
        }

        private IReadOnlyCollection<ISymbol> _members;
        private IReadOnlyCollection<ISymbol> Members
        {
            get
            {
                if (_members == null)
                {
                    if (_file?.Settings.PartialRenderingMode == PartialRenderingMode.Partial && _symbol.Locations.Length > 1)
                    {
                        _members = _symbol.GetMembers().Where(m => m.Locations.Any(l => string.Equals(l.SourceTree.FilePath, _file.FullName, StringComparison.OrdinalIgnoreCase))).ToArray();
                    }
                    else
                    {
                        _members = _symbol.GetMembers();
                    }
                }
                return _members;
            }
        }

        public string DocComment => _symbol.GetDocumentationCommentXml();
        public string Name => _symbol.Name;
        public string FullName => _symbol.ToDisplayString();
        public bool IsAbstract => _symbol.IsAbstract;
        public AccessModifier AccessModifiers => _symbol.DeclaredAccessibility.ToAccessModifier();
        public bool IsGeneric => _symbol.TypeParameters.Any();
        public string Namespace => _symbol.GetNamespace();
        public Func<string, string> TypeScriptNameFunc => _file?.Settings.TypeScriptNameFunc;

        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(_symbol, TypeScriptNameFunc);

        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(_symbol.GetAttributes(), TypeScriptNameFunc);
        public IClassMetadata BaseClass => FromNamedTypeSymbol(_symbol.BaseType);
        public IClassMetadata ContainingClass => FromNamedTypeSymbol(_symbol.ContainingType);
        public IEnumerable<IConstantMetadata> Constants => RoslynConstantMetadata.FromFieldSymbols(Members.OfType<IFieldSymbol>(), TypeScriptNameFunc);
        public IEnumerable<IDelegateMetadata> Delegates => RoslynDelegateMetadata.FromNamedTypeSymbols(Members.OfType<INamedTypeSymbol>().Where(s => s.TypeKind == TypeKind.Delegate), TypeScriptNameFunc);
        public IEnumerable<IEventMetadata> Events => RoslynEventMetadata.FromEventSymbols(Members.OfType<IEventSymbol>(), TypeScriptNameFunc);
        public IEnumerable<IFieldMetadata> Fields => RoslynFieldMetadata.FromFieldSymbols(Members.OfType<IFieldSymbol>(), TypeScriptNameFunc);
        public IEnumerable<IInterfaceMetadata> Interfaces => RoslynInterfaceMetadata.FromNamedTypeSymbols(_symbol.Interfaces);
        public IEnumerable<IMethodMetadata> Methods => RoslynMethodMetadata.FromMethodSymbols(Members.OfType<IMethodSymbol>(), TypeScriptNameFunc);
        public IEnumerable<IPropertyMetadata> Properties => RoslynPropertyMetadata.FromPropertySymbol(Members.OfType<IPropertySymbol>(), TypeScriptNameFunc);
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(_symbol.TypeParameters);
        public IEnumerable<ITypeMetadata> TypeArguments => RoslynTypeMetadata.FromTypeSymbols(_symbol.TypeArguments, TypeScriptNameFunc);
        public IEnumerable<IClassMetadata> NestedClasses => FromNamedTypeSymbols(Members.OfType<INamedTypeSymbol>().Where(s => s.TypeKind == TypeKind.Class));
        public IEnumerable<IEnumMetadata> NestedEnums => RoslynEnumMetadata.FromNamedTypeSymbols(Members.OfType<INamedTypeSymbol>().Where(s => s.TypeKind == TypeKind.Enum), TypeScriptNameFunc);
        public IEnumerable<IInterfaceMetadata> NestedInterfaces => RoslynInterfaceMetadata.FromNamedTypeSymbols(Members.OfType<INamedTypeSymbol>().Where(s => s.TypeKind == TypeKind.Interface));

        internal static IClassMetadata FromNamedTypeSymbol(INamedTypeSymbol symbol)
        {
            if (symbol == null) return null;
            if (symbol.ToDisplayString() == "object") return null;

            return new RoslynClassMetadata(symbol, null);
        }

        internal static IEnumerable<IClassMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols, RoslynFileMetadata file = null)
        {
            return symbols.Where(s => s.ToDisplayString() != "object")
                          .Select(s => new RoslynClassMetadata(s, file));
        }
    }
}
