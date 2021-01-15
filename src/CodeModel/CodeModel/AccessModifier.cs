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
        @public = 1,

        /// <summary>
        /// Private Access Modifier
        /// </summary>
        @private = 2,

        /// <summary>
        /// Protected Access Modifier
        /// </summary>
        @protected = 4,

        /// <summary>
        /// Internal Access Modifier
        /// </summary>
        @internal = 8,
    }

}