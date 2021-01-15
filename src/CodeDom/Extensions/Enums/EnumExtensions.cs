using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Typewriter.CodeModel;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.CodeDom.Extensions.Enums
{
    public static class EnumExtensions
    {
        public static AccessModifier ToAccessModifier(this vsCMAccess vsCmAccess)
        {
            AccessModifier returnValue = AccessModifier.@public;
            switch (vsCmAccess)
            {
                case vsCMAccess.vsCMAccessPublic:
                    returnValue = AccessModifier.@public;
                    break;
                case vsCMAccess.vsCMAccessPrivate:
                    returnValue = AccessModifier.@private;
                    break;
                case vsCMAccess.vsCMAccessProtected:
                    returnValue = AccessModifier.@protected;
                    break;
                case vsCMAccess.vsCMAccessDefault:
                    break;
                case vsCMAccess.vsCMAccessProject:
                case vsCMAccess.vsCMAccessAssemblyOrFamily:
                    returnValue = AccessModifier.@internal;
                    break;
                //case vsCMAccess.vsCMAccessWithEvents:
                //    break;
                case vsCMAccess.vsCMAccessProjectOrProtected:
                    returnValue = AccessModifier.@internal | AccessModifier.@protected;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vsCmAccess), vsCmAccess, null);
            }

            return returnValue;
        }
        public static CodeModel.MethodKind ToMethodKind(this vsCMFunction functionKind)
        {
            CodeModel.MethodKind returnValue = CodeModel.MethodKind.Ordinary;
            switch (functionKind)
            {
                case vsCMFunction.vsCMFunctionOther:
                    returnValue = CodeModel.MethodKind.Ordinary;
                    break;
                case vsCMFunction.vsCMFunctionConstructor:
                    returnValue = CodeModel.MethodKind.Constructor;
                    break;
                case vsCMFunction.vsCMFunctionPropertyGet:
                    returnValue = CodeModel.MethodKind.PropertyGet;
                    break;
                case vsCMFunction.vsCMFunctionPropertyLet:
                 case vsCMFunction.vsCMFunctionPutRef:
                case vsCMFunction.vsCMFunctionPropertyAssign:
                case vsCMFunction.vsCMFunctionPropertySet:
                    returnValue = CodeModel.MethodKind.PropertySet;
                    break;
               case vsCMFunction.vsCMFunctionSub:
                    returnValue = CodeModel.MethodKind.LambdaMethod;
                    break;
                case vsCMFunction.vsCMFunctionFunction:
                    returnValue = CodeModel.MethodKind.DelegateInvoke;
                    break;
                case vsCMFunction.vsCMFunctionTopLevel:
                    returnValue = CodeModel.MethodKind.Ordinary;
                    break;
                case vsCMFunction.vsCMFunctionDestructor:
                    returnValue = CodeModel.MethodKind.Destructor;
                    break;
                case vsCMFunction.vsCMFunctionOperator:
                    returnValue = CodeModel.MethodKind.UserDefinedOperator;
                    break;
                case vsCMFunction.vsCMFunctionVirtual:
                    returnValue = CodeModel.MethodKind.Ordinary;
                    break;
                case vsCMFunction.vsCMFunctionPure:
                    returnValue = CodeModel.MethodKind.Ordinary;
                    break;
                case vsCMFunction.vsCMFunctionConstant:
                    returnValue = CodeModel.MethodKind.Ordinary;
                    break;
                case vsCMFunction.vsCMFunctionShared:
                    returnValue = CodeModel.MethodKind.Ordinary;
                    break;
                case vsCMFunction.vsCMFunctionInline:
                    returnValue = CodeModel.MethodKind.LambdaMethod;
                    break;
                case vsCMFunction.vsCMFunctionComMethod:
                    returnValue = CodeModel.MethodKind.DelegateInvoke;
                    break;
            }

            return returnValue;
        }
    }
}
