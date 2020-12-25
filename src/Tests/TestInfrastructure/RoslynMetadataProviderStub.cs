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
        private readonly Project _project = null;

        public RoslynMetadataProviderStub(string projectPath)
        {
            _project = MSBuildWorkspace.Create().OpenProjectAsync(projectPath).Result;
        }

        public IFileMetadata GetFile(string path, Settings settings, Action<string[]> requestRender)
        {
            
            var document = _project.Solution.GetDocumentIdsWithFilePath(path).FirstOrDefault();
            if (document != null)
            {
                return new RoslynFileMetadata(_project.GetDocument(document), settings, requestRender);
            }

            return null;
        }

        public Solution CurrentSolution => _project.Solution;
    }
}
