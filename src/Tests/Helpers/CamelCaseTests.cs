using Should;
using Xunit;

namespace Typewriter.Tests.Helpers
{
    [Trait("Helpers", "CamelCase")]
    public class CamelCaseTests
    {
        [Fact]
        public void Expect_strings_to_be_camel_cased_correctly()
        {
            // Tests from Json.NET
            // https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json.Tests/Utilities/StringUtilsTests.cs
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("URLValue").ShouldEqual("urlValue");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("URL").ShouldEqual("url");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("ID").ShouldEqual("id");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("I").ShouldEqual("i");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("").ShouldEqual("");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase(null).ShouldEqual(null);
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("Person").ShouldEqual("person");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("iPhone").ShouldEqual("iPhone");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("IPhone").ShouldEqual("iPhone");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("I Phone").ShouldEqual("i Phone");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("I  Phone").ShouldEqual("i  Phone");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase(" IPhone").ShouldEqual(" IPhone");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase(" IPhone ").ShouldEqual(" IPhone ");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("IsCIA").ShouldEqual("isCIA");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("VmQ").ShouldEqual("vmQ");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("Xml2Json").ShouldEqual("xml2Json");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("SnAkEcAsE").ShouldEqual("snAkEcAsE");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("SnA__kEcAsE").ShouldEqual("snA__kEcAsE");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("SnA__ kEcAsE").ShouldEqual("snA__ kEcAsE");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("already_snake_case_ ").ShouldEqual("already_snake_case_ ");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("IsJSONProperty").ShouldEqual("isJSONProperty");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("SHOUTING_CASE").ShouldEqual("shoutinG_CASE");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("9999-12-31T23:59:59.9999999Z").ShouldEqual("9999-12-31T23:59:59.9999999Z");
            Typewriter.CodeModel.Extensions.StringHelpers.CamelCase("Hi!! This is text. Time to test.").ShouldEqual("hi!! This is text. Time to test.");
        }
    }
}
