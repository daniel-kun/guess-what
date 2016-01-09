using Xunit;

namespace Io.GuessWhat.UnitTests.Tools.Web
{
    public class ShortGuidTests
    {
        [Fact]
        public void GuidIs22CharactersLong()
        {
            // Example GUID:
            // NngwB6vmEEWhTTijOdIvtw
            string guid = GuessWhat.Tools.Web.ShortGuid.CreateGuid();
            Assert.DoesNotContain(guid, "+");
            Assert.DoesNotContain(guid, "/");
            Assert.Equal(guid.Length, 22);
        }

    }
}
