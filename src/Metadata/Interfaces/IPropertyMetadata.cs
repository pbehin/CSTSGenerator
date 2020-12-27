using System;
using Typewriter.CodeModel;

namespace Typewriter.Metadata.Interfaces
{
    public interface IPropertyMetadata : IFieldMetadata
    {
        bool IsAbstract { get; }
        bool IsStatic { get; }
        bool HasGetter { get; }
        bool HasSetter { get; }
        AccessModifier AccessModifiers { get; }
    }
}