using System.Collections.Generic;
using Typewriter.CodeModel;

namespace Typewriter.Metadata.Interfaces
{
    public interface IMethodMetadata : IFieldMetadata
    {
        bool IsAbstract { get; }
        bool IsGeneric { get; }
        bool IsStatic { get; }
        CodeModel.MethodKind MethodKind { get; }
        AccessModifier AccessModifiers { get; }
        IEnumerable<ITypeParameterMetadata> TypeParameters { get; }
        IEnumerable<IParameterMetadata> Parameters { get; }
    }
}