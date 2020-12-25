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
            Dte = TestInfrastructure.Dte.GetInstance(_solutionFileInfo.FullName);
            Provider = new RoslynMetadataProviderStub(Path.Combine(AppContext.BaseDirectory, @"..\..\Typewriter.Tests.csproj"));

            // Handle threading errors when calling into Visual Studio.
            MessageFilter.Register();
        }

        public DTE Dte { get; }
        public IMetadataProvider Provider { get; }
        public string SolutionDirectory => Path.Combine(_solutionFileInfo.Directory.FullName, "src");

        public void Dispose()
        {
            Dte.Quit();
            MessageFilter.Revoke();
        }
    }

    [CollectionDefinition(nameof(RoslynFixture))]
    public class RoslynCollection : ICollectionFixture<RoslynFixture>
    {
    }
}
