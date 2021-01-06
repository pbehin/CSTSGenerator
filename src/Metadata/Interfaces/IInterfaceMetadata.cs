using System.Collections.Generic;
using Typewriter.CodeModel;

namespace Typewriter.Metadata.Interfaces
{
    public interface IInterfaceMetadata : INamedItem
    {
        string DocComment { get; }
        string Namespace { get; }
        bool IsGeneric { get; }
        AccessModifier AccessModifiers { get; }
        ITypeMetadata Type { get; }
        IEnumerable<IAttributeMetadata> Attributes { get; }
        IClassMetadata ContainingClass { get; }
        IEnumerable<IEventMetadata> Events { get; }
        IEnumerable<ITypeParameterMetadata> TypeParameters { get; }
        IEnumerable<ITypeMetadata> TypeArguments { get; }
        IEnumerable<IInterfaceMetadata> Interfaces { get; }
        IEnumerable<IMethodMetadata> Methods { get; }
        IEnumerable<IPropertyMetadata> Properties { get; }
    }
}