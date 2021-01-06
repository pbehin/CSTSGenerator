using EnvDTE;
using System;
using System.IO;
using Microsoft.CodeAnalysis.Host.Mef;
using Typewriter.Metadata.Providers;
using Xunit;

namespace Typewriter.Tests.TestInfrastructure
{
    public class RoslynFixture : ITestFixture
    {
        FileInfo _solutionFileInfo;
        public RoslynFixture()
        {
            _solutionFileInfo = new FileInfo(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Typewriter.sln"));
            Dte = TestInfrastructure.Dte.GetNewInstance(_solutionFileInfo.FullName);
            Provider = new RoslynMetadataProviderStub(_solutionFileInfo.FullName);

            // Handle threading errors when calling into Visual Studio.
            MessageFilter.Register();
        }

        public DTE Dte { get; } //=> TestInfrastructure.Dte.GetInstance(_solutionFileInfo.FullName);
        public IMetadataProvider Provider { get; }
        public string SolutionDirectory => Path.Combine(_solutionFileInfo.Directory.FullName, "src");

        public void Dispose()
        {
            MessageFilter.Revoke();
        }

        ~RoslynFixture()
        {
            //TestInfrastructure.Dte.Quit();
            Dte.Quit();
        }
    }

    [CollectionDefinition(nameof(RoslynFixture))]
    public class RoslynCollection : ICollectionFixture<RoslynFixture>
    {
    }
}
