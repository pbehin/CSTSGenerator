﻿using System.Collections.Generic;
using Typewriter.CodeModel;

namespace Typewriter.Metadata.Interfaces
{
    public interface IMethodMetadata : IFieldMetadata
    {
        bool IsAbstract { get; }
        bool IsGeneric { get; }
        AccessModifier AccessModifiers { get; }
        IEnumerable<ITypeParameterMetadata> TypeParameters { get; }
        IEnumerable<IParameterMetadata> Parameters { get; }
    }
}