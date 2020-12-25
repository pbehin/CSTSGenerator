using EnvDTE;
using System;
using System.IO;
using Typewriter.Metadata.CodeDom;
using Typewriter.Metadata.Providers;
using Xunit;

namespace Typewriter.Tests.TestInfrastructure
{
    public class CodeDomFixture : ITestFixture
    {
        public CodeDomFixture()
        {
            _solutionFileInfo = new FileInfo(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Typewriter.sln"));
            Dte = TestInfrastructure.Dte.GetInstance(_solutionFileInfo.FullName);
            Provider = new CodeDomMetadataProvider(Dte);

            // Handle threading errors when calling into Visual Studio.
            MessageFilter.Register();
        }

        private FileInfo _solutionFileInfo;

        public DTE Dte { get; }
        public IMetadataProvider Provider { get; }
        public string SolutionDirectory => Path.Combine(_solutionFileInfo.Directory.FullName, "src");

        public void Dispose()
        {
            Dte.Quit(); 
            MessageFilter.Revoke();
        }

    }

    [CollectionDefinition(nameof(CodeDomFixture))]
    public class CodeDomCollection : ICollectionFixture<CodeDomFixture>
    {
    }
}