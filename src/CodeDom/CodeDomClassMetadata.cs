using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Typewriter.Metadata.Interfaces;

namespace Typewriter.Metadata.CodeDom
{
    public class CodeDomClassMetadata : IClassMetadata
    {
        private readonly CodeClass2 codeClass;
        private readonly CodeDomFileMetadata file;

        private CodeDomClassMetadata(CodeClass2 codeClass, CodeDomFileMetadata file)
        {
            this.codeClass = codeClass;
            this.file = file;
        }

        public string DocComment => codeClass.DocComment;
        public string Name => codeClass.Name;
        public string FullName => codeClass.FullName;
        public string Namespace => GetNamespace();
        public ITypeMetadata Type => new LazyCodeDomTypeMetadata(codeClass.FullName, false, false, file);
        public bool IsAbstract => codeClass.IsAbstract;
        public bool IsGeneric => codeClass.IsGeneric;
        public IClassMetadata AllBaseClass => CodeDomClassMetadata.FromAllCodeElements(codeClass.Bases, file).FirstOrDefault();
        public IClassMetadata AllContainingClass => CodeDomClassMetadata.FromAllCodeClass(codeClass.Parent as CodeClass2, file);
        public IClassMetadata BaseClass => CodeDomClassMetadata.FromPublicCodeElements(codeClass.Bases, file).FirstOrDefault();
        public IClassMetadata ContainingClass => CodeDomClassMetadata.FromPublicCodeClass(codeClass.Parent as CodeClass2, file);
        public IEnumerable<IAttributeMetadata> Attributes => CodeDomAttributeMetadata.FromCodeElements(codeClass.Attributes);
        public IEnumerable<IConstantMetadata> Constants => CodeDomConstantMetadata.FromCodeElements(codeClass.Children, file);
        public IEnumerable<IDelegateMetadata> AllDelegates => CodeDomDelegateMetadata.FromAllCodeElements(codeClass.Children, file);
        public IEnumerable<IDelegateMetadata> Delegates => CodeDomDelegateMetadata.FromPublicCodeElements(codeClass.Children, file);
        public IEnumerable<IEventMetadata> Events => CodeDomEventMetadata.FromCodeElements(codeClass.Children, file);
        public IEnumerable<IFieldMetadata> Fields => CodeDomFieldMetadata.FromCodeElements(codeClass.Children, file);
        public IEnumerable<ITypeParameterMetadata> TypeParameters => CodeDomTypeParameterMetadata.FromFullName(codeClass.FullName);
        public IEnumerable<ITypeMetadata> TypeArguments => CodeDomTypeMetadata.LoadGenericTypeArguments(IsGeneric, FullName, file);
        public IEnumerable<IInterfaceMetadata> Interfaces => CodeDomInterfaceMetadata.FromCodeElements(codeClass.ImplementedInterfaces, file);
        public IEnumerable<IMethodMetadata> Methods => CodeDomMethodMetadata.FromCodeElements(codeClass.Children, file);
        public IEnumerable<IPropertyMetadata> Properties => CodeDomPropertyMetadata.FromCodeElements(codeClass.Children, file);
        public IEnumerable<IClassMetadata> AllNestedClasses => CodeDomClassMetadata.FromAllCodeElements(codeClass.Members, file);
        public IEnumerable<IClassMetadata> NestedClasses => CodeDomClassMetadata.FromPublicCodeElements(codeClass.Members, file);
        public IEnumerable<IEnumMetadata> NestedEnums => CodeDomEnumMetadata.FromCodeElements(codeClass.Members, file);
        public IEnumerable<IInterfaceMetadata> NestedInterfaces => CodeDomInterfaceMetadata.FromCodeElements(codeClass.Members, file);

        private string GetNamespace()
        {
            var parent = codeClass.Parent as CodeClass2;
            return parent != null ? parent.FullName : (codeClass.Namespace?.FullName ?? string.Empty);
        }

        internal static IEnumerable<IClassMetadata> FromPublicCodeElements(CodeElements codeElements, CodeDomFileMetadata file)
        {
            return FromCodeElements(codeElements, file, new[] { vsCMAccess.vsCMAccessPublic });
        }

        internal static IClassMetadata FromPublicCodeClass(CodeClass2 codeClass, CodeDomFileMetadata file)
        {
            return FromCodeClass(codeClass, file, new[] { vsCMAccess.vsCMAccessPublic });
        }

        internal static IEnumerable<IClassMetadata> FromAllCodeElements(CodeElements codeElements, CodeDomFileMetadata file)
        {
            var all = Enum.GetValues(typeof(vsCMAccess)).Cast<vsCMAccess>();
            return FromCodeElements(codeElements, file, all);
        }

        internal static IClassMetadata FromAllCodeClass(CodeClass2 codeClass, CodeDomFileMetadata file)
        {
            var all = Enum.GetValues(typeof(vsCMAccess)).Cast<vsCMAccess>();
            return FromCodeClass(codeClass, file, all);
        }

        internal static IEnumerable<IClassMetadata> FromCodeElements(CodeElements codeElements, CodeDomFileMetadata file, IEnumerable<vsCMAccess> vsCMAccess)
        {
            return codeElements.OfType<CodeClass2>().Where(c => vsCMAccess.Contains(c.Access) && c.FullName != "System.Object").Select(c => new CodeDomClassMetadata(c, file));
        }

        internal static IClassMetadata FromCodeClass(CodeClass2 codeClass, CodeDomFileMetadata file, IEnumerable<vsCMAccess> vsCMAccess)
        {
            return codeClass == null || !vsCMAccess.Contains(codeClass.Access) || codeClass.FullName == "System.Object" ? null : new CodeDomClassMetadata(codeClass, file);
        }
    }
}