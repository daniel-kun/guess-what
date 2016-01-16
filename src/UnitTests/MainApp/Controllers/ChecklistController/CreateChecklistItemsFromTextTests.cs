using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.Controllers.ChecklistController
{
    public class CreateChecklistItemsFromTextTests
    {
        [Fact]
        public void ShouldReturnAnEmptyListWhenTextIsEmpty()
        {
            var items = GuessWhat.MainApp.Controllers.ChecklistController.CreateChecklistItemsFromText(string.Empty);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 0);
        }

        [Fact]
        public void ShouldReturnOneItemWhenTextIsOneLine ()
        {
            const string demoText = "asdf";
            var items = GuessWhat.MainApp.Controllers.ChecklistController.CreateChecklistItemsFromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 1);
            Assert.Contains(items, item => item.Title == demoText);
        }

        [Fact]
        public void ShouldReturn3ItemsWhenTextIs3Lines()
        {
            const string demoText = @"Lorem Ipsum
Foo Bar Baz
ASDF-HJKL";
            var items = GuessWhat.MainApp.Controllers.ChecklistController.CreateChecklistItemsFromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 3);
            Assert.Contains(items, item => item.Title == "Lorem Ipsum");
            Assert.Contains(items, item => item.Title == "Foo Bar Baz");
            Assert.Contains(items, item => item.Title == "ASDF-HJKL");
        }

        [Fact]
        public void ShouldIgnoreLinesWithOnlyWhitespace()
        {
            const string demoText = "   \r\n" +
"Lorem Ipsum\r\n" +
"\t\r\n" +
"Foo Bar Baz\r\n" +
" \t\r\n" +
"ASDF-HJKL\r\n" +
"\t\t   \r\n";
            var items = GuessWhat.MainApp.Controllers.ChecklistController.CreateChecklistItemsFromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(3, items.Count);
            Assert.Contains(items, item => item.Title == "Lorem Ipsum");
            Assert.Contains(items, item => item.Title == "Foo Bar Baz");
            Assert.Contains(items, item => item.Title == "ASDF-HJKL");
        }

        [Fact]
        public void ShouldPreseveWhitespaceInLines()
        {
            const string demoText = "\r\n" +
"\t  Whitespace before\r\n" +
"\tWhitespace before and after\t  \r\n" +
"Whitespace after\t\t\r\n" +
"\t\t   ";
            var items = GuessWhat.MainApp.Controllers.ChecklistController.CreateChecklistItemsFromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 3);
            Assert.Contains(items, item => item.Title == "\t  Whitespace before");
            Assert.Contains(items, item => item.Title == "\tWhitespace before and after\t  ");
            Assert.Contains(items, item => item.Title == "Whitespace after\t\t");
        }

    }
}
