using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Typewriter.CodeModel.Attributes;

namespace Typewriter.CodeModel.Extensions
{
    /// <summary>
    /// Helper methods for working with strings.
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Returns the string with CamelCase format
        /// </summary>
        public static string CamelCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (char.IsUpper(s[0]) == false) return s;

            var chars = s.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 1 && char.IsUpper(chars[i]) == false)
                {
                    break;
                }

                var hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && char.IsUpper(chars[i + 1]) == false)
                {
                    break;
                }

                chars[i] = char.ToLowerInvariant(chars[i]);
            }

            return new string(chars);
        }

        /// <summary>
        /// Returns the string with KebabCase format
        /// </summary>
        public static string KebabCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            
            return Regex.Replace(
                s,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                "-$1",
                RegexOptions.Compiled)
                .Trim()
                .ToLower();
        }

        /// <summary>
        /// Primitive Types and TS conversion
        /// </summary>
        public static readonly Dictionary<string, string> PrimitiveTypes = new Dictionary<string, string>
        {
            { "System.Boolean", "bool" },
            { "System.Byte", "byte" },
            { "System.Char", "char" },
            { "System.Decimal", "decimal" },
            { "System.Double", "double" },
            { "System.Int16", "short" },
            { "System.Int32", "int" },
            { "System.Int64", "long" },
            { "System.SByte", "sbyte" },
            { "System.Single", "float" },
            { "System.String", "string" },
            { "System.UInt32", "uint" },
            { "System.UInt16", "ushort" },
            { "System.UInt64", "ulong" },

            { "System.DateTime", "DateTime" },
            { "System.DateTimeOffset", "DateTimeOffset" },
            { "System.Guid", "Guid" },
            { "System.TimeSpan", "TimeSpan" },
        };
    }
}
