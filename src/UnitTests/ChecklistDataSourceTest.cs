using Xunit;

namespace Io.GuessWhat.UnitTests
{
    public class ChecklistDataSourceTest
    {
        [Fact]
        public void GuidIs22CharactersLong ()
        {
            // Example GUID:
            // NngwB6vmEEWhTTijOdIvtw
            string guid = MainApp.DataSources.ChecklistDataSource.CreateGuid();
            Assert.DoesNotContain(guid, "+");
            Assert.DoesNotContain(guid, "/");
            Assert.Equal(guid.Length, 22);
        }

        [Fact]
        public void ReplaceUrlUnsafeCharsShouldReplacePlusAndSlash()
        {
            Assert.DoesNotContain(MainApp.DataSources.ChecklistDataSource.ReplaceUrlUnsafeChars("+"), "+");
            Assert.DoesNotContain(MainApp.DataSources.ChecklistDataSource.ReplaceUrlUnsafeChars("/"), "/");
        }
    }
}
