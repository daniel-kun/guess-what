using Xunit;

namespace guess_what2_tests
{
    public class ChecklistDataSourceTest
    {
        [Fact]
        public void GuidIs22CharactersLong ()
        {
            // Example GUID:
            // NngwB6vmEEWhTTijOdIvtw
            string guid = guess_what2.DataSources.ChecklistDataSource.CreateGuid();
            Assert.DoesNotContain(guid, "+");
            Assert.DoesNotContain(guid, "/");
            Assert.Equal(guid.Length, 22);
        }

        [Fact]
        public void ReplaceUrlUnsafeCharsShouldReplacePlusAndSlash()
        {
            Assert.DoesNotContain(guess_what2.DataSources.ChecklistDataSource.ReplaceUrlUnsafeChars("+"), "+");
            Assert.DoesNotContain(guess_what2.DataSources.ChecklistDataSource.ReplaceUrlUnsafeChars("/"), "/");
        }
    }
}
