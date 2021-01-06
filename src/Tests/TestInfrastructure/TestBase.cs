using System.IO;
using EnvDTE;
using Typewriter.CodeModel.Implementation;
using Typewriter.Metadata.Providers;
using Xunit;
using File = Typewriter.CodeModel.File;
using Typewriter.Configuration;
using Typewriter.CodeModel.Configuration;
using System;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Typewriter.Tests.TestInfrastructure
{
    public abstract class TestBase
    {
        protected readonly IMetadataProvider metadataProvider;

        protected readonly bool isRoslyn;
        protected readonly bool isCodeDom;

        protected TestBase(ITestFixture fixture)
        {
            Fixture = fixture;
            this.metadataProvider = fixture.Provider;

            this.isRoslyn = fixture is RoslynFixture;
            this.isCodeDom = fixture is CodeDomFixture;
        }

        protected string SolutionDirectory => Fixture.SolutionDirectory;
        public ITestFixture Fixture { get; set; }

        protected ProjectItem GetProjectItem(string path)
        {
            return Fixture.Dte.Solution.FindProjectItem(Path.Combine(SolutionDirectory, path));
        }

        protected string GetFileContents(string path)
        {
            return System.IO.File.ReadAllText(Path.Combine(SolutionDirectory, path));
        }

        protected File GetFile(string path, Settings settings = null, Action<string[]> requestRender = null)
        {
            if (settings == null)
                settings = new SettingsImpl(null);

            var metadata = metadataProvider.GetFile(Path.Combine(SolutionDirectory, path), settings, requestRender);
            return new FileImpl(metadata);
        }
    }
}