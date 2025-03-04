using TypeFaster.ConsoleApp.ExtensionMethods;

namespace TypeFaster.ConsoleApp.Tests.ExtensionMethodTests
{
    public class StringExtensionTests
    {
        [Fact]
        public void TextWrap_Given_StringIsLessThanLength_ShouldReturn_SingleString()
        {
            var str = "string";

            var result = str.TextWrap(10);

            Assert.Single(result);
            Assert.Equal(str, result.First());
        }

        const bool Hyphenate = true;
        const bool DoNotHyphenate = false;

        public static IEnumerable<object[]> TextWrap_Given_String_Width_ShouldReturn_ExpectedOutput_TestCases =
        [
            ["Input Input Input", 15, Hyphenate, new string[] { "Input Input", "Input" }],
            ["InputInputInputInput InputInput", 15, Hyphenate, new string[] { "InputInputInpu-", "tInput", "InputInput" }],
            ["InputInputInputInput InputInput", 15, DoNotHyphenate, new string[] { "InputInputInput", "Input", "InputInput" }],
            ["InputInputInputInputInputInput", 15, Hyphenate, new string[] { "InputInputInpu-", "tInputInputInp-", "ut" }],
            ["InputInputInputInputInputInput", 15, DoNotHyphenate, new string[] { "InputInputInput", "InputInputInput" }],
            ["InputInputInputInput InputInputInput", 15, Hyphenate, new string[] { "InputInputInpu-","tInput", "InputInputInput" }],
        ];

        [MemberData(nameof(TextWrap_Given_String_Width_ShouldReturn_ExpectedOutput_TestCases))]
        [Theory]
        public void TextWrap_Given_Input_ShouldReturn_ExpectedOutput(string input, int width, bool hyphenate, IEnumerable<string> expectedOutput)
        {
            var result = input.TextWrap(width, hyphenate);

            Assert.Equal(expectedOutput, result);
        }
    }
}
