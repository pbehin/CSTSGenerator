using System.Linq;
using Typewriter.Metadata.Interfaces;
using Typewriter.Metadata.Providers;
using Typewriter.Metadata.Roslyn;
using Typewriter.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using Typewriter.Generation;

namespace Typewriter.Tests.TestInfrastructure
{
    public class RoslynMetadataProviderStub : IMetadataProvider
    {
        private readonly MSBuildWorkspace _workspace = null;
        private Solution _solution;

        public RoslynMetadataProviderStub(string solutionFilePath)
        {
            _workspace = MSBuildWorkspace.Create();
            _solution = _workspace.OpenSolutionAsync(solutionFilePath).Result.GetIsolatedSolution();
        }

        public IFileMetadata GetFile(string path, Settings settings, Action<string[]> requestRender)
        {
            
            var document = _solution.GetDocumentIdsWithFilePath(path).FirstOrDefault();
            if (document != null)
            {
                return new RoslynFileMetadata(_solution.GetDocument(document), settings, requestRender);
            }

            return null;
        }

        public Solution CurrentSolution => _solution;
    }
}
