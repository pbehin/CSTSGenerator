using System;
using System.IO;
using File = Typewriter.CodeModel.File;

namespace Typewriter.Configuration
{
    /// <summary>
    /// Provides settings for Typewriter Templates
    /// </summary>
    public abstract class Settings
    {
        /// <summary>
        /// Includes files in the specified project when rendering the template.
        /// </summary>
        public abstract Settings IncludeProject(string projectName);

        /// <summary>
        /// Includes files in the current project when rendering the template.
        /// </summary>
        public abstract Settings IncludeCurrentProject();

        /// <summary>
        /// Includes files in all referenced projects when rendering the template.
        /// </summary>
        public abstract Settings IncludeReferencedProjects();

        /// <summary>
        /// Includes files in all projects in the solution when rendering the template.
        /// Note: Including all projects can have a large impact on performance in large solutions.
        /// </summary>
        public abstract Settings IncludeAllProjects();

        /// <summary>
        /// Filter files when rendering the template.
        /// </summary>
        public Predicate<string> FileNameFilter { get; set; } = fileName => true;

        /// <summary>
        /// Gets or sets the file extension for output files.
        /// </summary>
        public string OutputExtension { get; set; } = ".ts";

        /// <summary>
        /// Gets or sets a folder name factory for the template.
        /// The factory is called for each rendered file to determine the output folder
        /// </summary>
        public Func<File, DirectoryInfo> OutputFolderFactory { get; set; }

        /// <summary>
        /// Gets or sets a filename factory for the template.
        /// The factory is called for each rendered file to determine the output filename (including extension).
        /// Example: file => file.Classes.First().FullName + ".ts";
        /// </summary>
        public Func<File, string> OutputFilenameFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PartialRenderingMode PartialRenderingMode { get; set; } = PartialRenderingMode.Combined;

        /// <summary>
        /// function to change the script type name
        /// </summary>
        public Func<string, string> TypeScriptNameFunc { get; set; }

    }
}
