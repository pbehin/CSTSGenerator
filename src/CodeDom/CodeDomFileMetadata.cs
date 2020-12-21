using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.CodeDom
{
    public class CodeDomFileMetadata : IFileMetadata
    {
        private readonly ProjectItem projectItem;
        private readonly TypeFactory typeFactory;

        public CodeDomFileMetadata(ProjectItem projectItem)
        {
            this.projectItem = projectItem;
            this.typeFactory = new TypeFactory(projectItem);
        }

        private IEnumerable<CodeNamespace> Namespaces => projectItem.FileCodeModel.CodeElements.OfType<CodeNamespace>();

        public string Name => projectItem.Name;
        public string FullName => projectItem.FileNames[1];
        public IEnumerable<IClassMetadata> AllClasses =>
            CodeDomClassMetadata.FromAllCodeElements(projectItem.FileCodeModel.CodeElements, this)
            .Concat(Namespaces.SelectMany(n => CodeDomClassMetadata.FromAllCodeElements(n.Members, this)));

        public IEnumerable<IClassMetadata> Classes =>
            CodeDomClassMetadata.FromPublicCodeElements(projectItem.FileCodeModel.CodeElements, this)
            .Concat(Namespaces.SelectMany(n => CodeDomClassMetadata.FromPublicCodeElements(n.Members, this)));

        public IEnumerable<IDelegateMetadata> AllDelegates =>
                    CodeDomDelegateMetadata.FromAllCodeElements(projectItem.FileCodeModel.CodeElements, this)
                    .Concat(Namespaces.SelectMany(n => CodeDomDelegateMetadata.FromAllCodeElements(n.Members, this)));

        public IEnumerable<IDelegateMetadata> Delegates =>
                    CodeDomDelegateMetadata.FromPublicCodeElements(projectItem.FileCodeModel.CodeElements, this)
                    .Concat(Namespaces.SelectMany(n => CodeDomDelegateMetadata.FromPublicCodeElements(n.Members, this)));

        public IEnumerable<IEnumMetadata> Enums =>
            CodeDomEnumMetadata.FromCodeElements(projectItem.FileCodeModel.CodeElements, this)
            .Concat(Namespaces.SelectMany(n => CodeDomEnumMetadata.FromCodeElements(n.Members, this)));

        public IEnumerable<IInterfaceMetadata> Interfaces =>
            CodeDomInterfaceMetadata.FromCodeElements(projectItem.FileCodeModel.CodeElements, this)
            .Concat(Namespaces.SelectMany(n => CodeDomInterfaceMetadata.FromCodeElements(n.Members, this)));

        internal CodeType GetType(string fullName)
        {
            return typeFactory.GetType(fullName);
        }
    }
}