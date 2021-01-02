using System;
using System.Collections;
using System.Collections.Generic;

namespace Typewriter.Tests.CodeModel.Support
{
    public class TypeInfo
    {
        public ClassInfo Class { get; set; }
        public BaseClassInfo BaseClass { get; set; }
        public GenericClassInfo<string> GenericClass { get; set; }
        public InheritGenericClassInfo InheritGenericClass { get; set; }

        public string String { get; set; }
        public ICollection<ClassInfo> ClassCollection { get; set; }
        public ICollection<string> StringCollection { get; set; }

        public ClassInfo v03d7ad5a32cd49d4a4e1dba2ab5b0488;
        public string v86f7a36ec8a247b4a4b2132ae7aead74;
    }
}
