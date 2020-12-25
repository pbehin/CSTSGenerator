using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using EnvDTE;

namespace Typewriter.Tests.TestInfrastructure
{
    internal static class Dte
    {
        private static DTE _dte = null;
        private static readonly object LockObj = new object();

        internal static DTE GetInstance(string solution)
        {
            if (_dte == null)
            {
                lock (LockObj)
                {
                    if (_dte == null)
                    {

                        Type dteType = null;
                        for (int i = 20; i > 10; i--)
                        {
                            try
                            {
                                dteType = Type.GetTypeFromProgID($"VisualStudio.DTE.{i}.0", true);
                                if (dteType != null)
                                    break;
                            }
                            catch
                            {
                            }

                        }

                        if (dteType == null)
                            throw new Exception("Can not find Dte.");
                        _dte = (DTE) Activator.CreateInstance(dteType);
                    }
                }
            }
            _dte.Solution.Open(solution);
            return _dte;

        }
    }
}