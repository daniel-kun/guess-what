using Xunit;

namespace Io.GuessWhat.UnitTests.Tools.Web.FormInput
{
    public class PrepareTextTests
    {
        [Fact]
        public void ShouldConvertNullToEmptyString()
        {
            Assert.NotNull(GuessWhat.Tools.Web.FormInput.PrepareText(null, 10));
            Assert.Equal(string.Empty, GuessWhat.Tools.Web.FormInput.PrepareText(null, 0));
            Assert.Equal(0, GuessWhat.Tools.Web.FormInput.PrepareText(null, int.MaxValue).Length);
        }

        [Fact]
        public void ShouldShortenToMaxLength()
        {
            Assert.Equal("abc", GuessWhat.Tools.Web.FormInput.PrepareText("abcdefg", 3));
        }

    }
}
