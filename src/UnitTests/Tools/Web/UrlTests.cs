using Xunit;

namespace Io.GuessWhat.UnitTests.Tools.Web
{
    public class UrlTests
    {
        [Fact]
        public void ReplaceUrlUnsafeCharsShouldReplacePlusAndSlash()
        {
            Assert.DoesNotContain(GuessWhat.Tools.Web.Url.ReplaceUrlUnsafeChars("+"), "+");
            Assert.DoesNotContain(GuessWhat.Tools.Web.Url.ReplaceUrlUnsafeChars("/"), "/");
        }
    }
}
