using System;
using EnvDTE;

namespace Typewriter.Tests.TestInfrastructure
{
    internal static class Dte
    {
        private static DTE _dte;
        private static readonly object LockObj = new object();

        internal static DTE GetInstance(string solution)
        {
            if (_dte == null)
            {
                lock (LockObj)
                {
                    if (_dte != null) return _dte;
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
                            // ignored
                        }
                    }

                    if (dteType == null)
                        throw new Exception("Cannot find Dte Type.");
                    _dte = (DTE)Activator.CreateInstance(dteType);
                    if (_dte == null)
                        throw new TypeAccessException("Cannot Create Instance of DTE.");
                    try
                    {
                        _dte.Solution.Open(solution);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Cannot Open Solution.", e);
                    }
                }
            }
            return _dte;

        }
    }
}