using System;
using System.Collections;
using System.Collections.Generic;

namespace Typewriter.Tests.CodeModel.Support
{
    public class PropertyInfo
    {
        /// <summary>
        /// summary
        /// </summary>
        public string GetterOnly { get; }
        public string SetterOnly { set { } }
        public string PrivateGetter { private get; set; }
        public string PrivateSetter { get; private set; }

        // Primitive types
        [AttributeInfo]
        public bool Bool { get; set; }
        public char Char { get; set; }
        public string String { get; set; }
        public byte Byte { get; set; }
        public sbyte Sbyte { get; set; }
        public int Int { get; set; }
        public uint Uint { get; set; }
        public short Short { get; set; }
        public ushort Ushort { get; set; }
        public long Long { get; set; }
        public ulong Ulong { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }
        public decimal Decimal { get; set; }

        // Special types
        public DateTime DateTime { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public Guid Guid { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public object Object { get; set; }
        public dynamic Dynamic { get; set; }

        // Enums
        public ConsoleColor Enum { get; set; }
        public ConsoleColor? NullableEnum1 { get; set; }
        public Nullable<ConsoleColor> NullableEnum2 { get; set; }

        public Exception Exception { get; set; } // Class

        // Untyped collections
        public Array Array { get; set; }
        public IEnumerable Enumerable { get; set; }

        // Typed collections
        public string[] StringArray { get; set; }
        public IEnumerable<string> EnumerableString { get; set; }
        public List<string> ListString { get; set; }

        // Nullable
        public int? NullableInt1 { get; set; }
        public Nullable<int> NullableInt2 { get; set; }
        public IEnumerable<int> EnumerableInt { get; set; }
        public IEnumerable<int?> EnumerableNullableInt { get; set; }

        // Defined types
        public ClassInfo Class { get; set; }
        public BaseClassInfo BaseClass { get; set; }
        public GenericClassInfo<string> GenericClass { get; set; }
        public IInterfaceInfo Interface { get; set; }

        public ICollection<string> v0b13f6d2b4ff4d9ebce9aad3aa91a7e5;
        public ConsoleColor v06415270ddd9497297d517eaab5df02e;
        public ICollection<string> vb763b3c4460044a391724365a468aaa4;
        public ConsoleColor v3a74c221af1b4a3498b03711b44ff8a9;
        public ICollection<string> v496d1948d71643649820d83fd44eb6e9;
        public ConsoleColor va857603124fe44d697060f536ef60063;
        public ICollection<string> vb9dab6a387dd46efbbb29c86f073cc67;
        public ConsoleColor v6636f267d0284b05bed19de4085bb232;
        public ICollection<string> vb4ff69fdfb604752ad2054648f7119ed;
        public ConsoleColor v4451832794ec4ae8866f4311eb81f022;
        public ICollection<string> vc110d1788e6f433191f14177c867f8ad;
        public ConsoleColor v4e635f06c50c460ea099e7c287c77000;
        public ICollection<string> v24aad3b8f26f4b23b522e5adb5d573e1;
        public ConsoleColor v7fbc0914e5834f8aadd985d5614bfbe2;
        public ICollection<string> vf59fa4650cd14f7d82ee21b8420f70ee;
        public ConsoleColor vfb4d3a8a217245cca980162c40c020e5;
        public ICollection<string> v6ae00db3558b4fb2a71aa0cb09154d6b;
        public ConsoleColor v4f1c4ccc9fe54b9a86520500b3fb2c5f;
    }

    public class GenericPropertyInfo<T>
    {
        public T Generic { get; set; }
        public IEnumerable<T> EnumerableGeneric { get; set; }
    }
}
