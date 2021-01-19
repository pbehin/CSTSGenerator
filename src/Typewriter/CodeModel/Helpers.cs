using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Typewriter.Metadata.Interfaces;
using Typewriter.CodeModel.Extensions;
using static Typewriter.CodeModel.Extensions.StringHelpers;

namespace Typewriter.CodeModel
{
    public static class Helpers
    {
        public static string GetTypeScriptName(ITypeMetadata metadata)
        {
            var defaultName = string.Empty;
            if (metadata == null)
                defaultName = "any";
            else if (metadata.IsEnumerable)
            {
                var typeArguments = metadata.TypeArguments.ToList();

                if (typeArguments.Count == 0)
                {
                    if (metadata.BaseClass != null && metadata.BaseClass.IsGeneric)
                    {
                        typeArguments = metadata.BaseClass.TypeArguments.ToList();
                    }
                    else
                    {
                        var genericInterface = metadata.Interfaces.FirstOrDefault(i => i.IsGeneric);
                        if (genericInterface != null)
                            typeArguments = genericInterface.TypeArguments.ToList();
                    }

                    if (typeArguments.Any(t => t.FullName == metadata.FullName))
                    {
                        return "any[]";
                    }
                }

                if (typeArguments.Count == 1)
                    return GetTypeScriptName(typeArguments.FirstOrDefault()) + "[]";

                if (typeArguments.Count == 2)
                {
                    var key = GetTypeScriptName(typeArguments[0]);
                    var value = GetTypeScriptName(typeArguments[1]);

                    return string.Concat("{ [key: ", key, "]: ", value, "; }");
                }

                defaultName = "any[]";
            }

            else if (metadata.IsValueTuple)
            {
                var types = string.Join(", ", metadata.TupleElements.Select(p => $"{p.Name}: {GetTypeScriptName(p.Type)}"));
                defaultName = $"{{ {types} }}";
            }

            else if (metadata.IsGeneric)
                defaultName = metadata.Name + string.Concat("<", string.Join(", ", metadata.TypeArguments.Select(GetTypeScriptName)), ">");
            else 
                defaultName = ExtractTypeScriptName(metadata);

            return metadata.TypeScriptNameFunc != null ? metadata.TypeScriptNameFunc(metadata.FullName, defaultName) : defaultName;
        }

        public static string GetOriginalName(ITypeMetadata metadata)
        {
            var name = metadata.Name;
            var fullName = metadata.IsNullable ? metadata.FullName.TrimEnd('?') : metadata.FullName;

            if (PrimitiveTypes.ContainsKey(fullName))
                name = PrimitiveTypes[fullName] + (metadata.IsNullable ? "?" : string.Empty);

            return name;
        }

        private static string ExtractTypeScriptName(ITypeMetadata metadata)
        {
            var fullName = metadata.IsNullable ? metadata.FullName.TrimEnd('?') : metadata.FullName;

            switch (fullName)
            {
                case "System.Boolean":
                    return "boolean";
                case "System.String":
                case "System.Char":
                case "System.Guid":
                case "System.TimeSpan":
                    return "string";
                case "System.Byte":
                case "System.SByte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    return "number";
                case "System.DateTime":
                case "System.DateTimeOffset":
                    return "Date";
                case "System.Void":
                    return "void";
                case "System.Object":
                case "dynamic":
                    return "any";
            }


            return metadata.IsNullable ? metadata.Name.TrimEnd('?') : metadata.Name;
        }

        public static bool IsPrimitive(ITypeMetadata metadata)
        {
            var fullName = metadata.FullName;

            if (metadata.IsNullable)
            {
                fullName = fullName.TrimEnd('?');
            }
            else if (metadata.IsEnumerable)
            {
                var innerType = metadata.TypeArguments.FirstOrDefault();
                if (innerType != null)
                {
                    fullName = innerType.IsNullable ? innerType.FullName.TrimEnd('?') : innerType.FullName;
                }
                else
                {
                    return false;
                }
            }

            return metadata.IsEnum || PrimitiveTypes.ContainsKey(fullName);
        }

    }
}
