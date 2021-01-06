using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Typewriter.CodeModel;

namespace Typewriter.Metadata.CodeDom.Extensions.Enums
{
    public static class EnumExtensions
    {
        public static AccessModifier ToAccessModifier(this vsCMAccess vsCmAccess)
        {
            AccessModifier returnValue = AccessModifier.Public;
            switch (vsCmAccess)
            {
                case vsCMAccess.vsCMAccessPublic:
                    returnValue = AccessModifier.Public;
                    break;
                case vsCMAccess.vsCMAccessPrivate:
                    returnValue = AccessModifier.Private;
                    break;
                case vsCMAccess.vsCMAccessProtected:
                    returnValue = AccessModifier.Protected;
                    break;
                case vsCMAccess.vsCMAccessDefault:
                    break;
                case vsCMAccess.vsCMAccessProject:
                case vsCMAccess.vsCMAccessAssemblyOrFamily:
                    returnValue = AccessModifier.Internal;
                    break;
                //case vsCMAccess.vsCMAccessWithEvents:
                //    break;
                case vsCMAccess.vsCMAccessProjectOrProtected:
                    returnValue = AccessModifier.Internal | AccessModifier.Protected;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vsCmAccess), vsCmAccess, null);
            }

            return returnValue;
        }
    }
}
