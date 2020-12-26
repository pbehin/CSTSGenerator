using System;

namespace Typewriter.CodeModel
{
    /// <summary>
    /// All class Access Modifier
    /// </summary>
    [Flags]
    public enum AccessModifier
    {
        /// <summary>
        /// Access Modifier is not valid !!!
        /// </summary>
        NotApplicable = 0,

        /// <summary>
        /// Public Access Modifier
        /// </summary>
        Public = 1,

        /// <summary>
        /// Private Access Modifier
        /// </summary>
        Private = 2,

        /// <summary>
        /// Protected Access Modifier
        /// </summary>
        Protected = 4,

        /// <summary>
        /// Internal Access Modifier
        /// </summary>
        Internal = 8,
    }

}