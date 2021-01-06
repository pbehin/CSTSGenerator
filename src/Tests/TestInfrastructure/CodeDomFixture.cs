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
            Dte = TestInfrastructure.Dte.GetNewInstance(_solutionFileInfo.FullName);
            Provider = new CodeDomMetadataProvider(Dte);

            // Handle threading errors when calling into Visual Studio.
            MessageFilter.Register();
        }

        private FileInfo _solutionFileInfo;

        public DTE Dte { get; } //=> TestInfrastructure.Dte.GetInstance(_solutionFileInfo.FullName);
        public IMetadataProvider Provider { get; }
        public string SolutionDirectory => Path.Combine(_solutionFileInfo.Directory.FullName, "src");

        public void Dispose()
        {
            MessageFilter.Revoke();
        }

        ~CodeDomFixture()
        {
            //TestInfrastructure.Dte.Quit();
            Dte.Quit();
        }
    }

    [CollectionDefinition(nameof(CodeDomFixture))]
    public class CodeDomCollection : ICollectionFixture<CodeDomFixture>
    {
    }
}