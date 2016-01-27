using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.Models.ChecklistItem
{
    public class FromTextTests
    {
        [Fact]
        public void ShouldReturnAnEmptyListWhenTextIsEmpty()
        {
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(string.Empty);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 0);
        }

        [Fact]
        public void ShouldReturnOneItemWhenTextIsOneLine()
        {
            const string demoText = "asdf";
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
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
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
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
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(3, items.Count);
            Assert.Contains(items, item => item.Title == "Lorem Ipsum");
            Assert.Contains(items, item => item.Title == "Foo Bar Baz");
            Assert.Contains(items, item => item.Title == "ASDF-HJKL");
        }

        [Trait("Category", "Test")]
        [Fact]
        public void ShouldTrimWhitespaceAtEndOfLines()
        {
            const string demoText = "\r\n" +
"Whitespace before\r\n" +
"Whitespace before and after\t  \r\n" +
"Whitespace after\r\n" +
"\t\t   ";
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 3);
            Assert.Contains(items, item => item.Title == "Whitespace before");
            Assert.Contains(items, item => item.Title == "Whitespace before and after");
            Assert.Contains(items, item => item.Title == "Whitespace after");
        }

        [Fact]
        public void ShouldIgnoreLeadingWhiteSpaceInFirstLineWhenSecondLineNotIndented()
        {
            const string demoText = @"      This is a parent
This is another top level node
";
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 2);
            Assert.Equal(items[0].Title, "This is a parent");
            Assert.Null(items[0].Items);
            Assert.Equal(items[1].Title, "This is another top level node");
            Assert.Null(items[1].Items);
        }

        [Fact]
        public void ShouldIgnoreLeadingWhiteSpaceInFirstLineWhenSecondLineIsIndented()
        {
            const string demoText = @"      This is a parent
 This is a child node
This is another top level node
";
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 2);
            Assert.Equal(items[0].Title, "This is a parent");
            Assert.Equal(items[0].Items.Count, 1);
            Assert.Equal(items[0].Items[0].Title, "This is a child node");
            Assert.Equal(items[1].Title, "This is another top level node");
        }

        [Fact]
        public void ShouldCreateChildItemsWhenLinesStartWithWhitespace()
        {
            const string demoText = @"This is a parent
 This is a child node
";
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 1);
            Assert.Equal(items[0].Title, "This is a parent");
            Assert.Equal(items[0].Items.Count, 1);
            Assert.Equal(items[0].Items[0].Title, "This is a child node");
        }

        [Fact]
        public void ShouldCreateChildrenForDifferentParents()
        {
            const string demoText = @"Item #1
   Item #1.1
   Item #1.2
Item #2
    Item #2.1
    Item #2.2
    Item #2.3
Item #3
";
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);

            Assert.Equal(items.Count, 3);

            Assert.Equal(items[0].Title, "Item #1");
            Assert.Equal(items[1].Title, "Item #2");
            Assert.Equal(items[2].Title, "Item #3");
            Assert.Equal(items[0].Items.Count, 2);

            Assert.Equal(items[0].Items[0].Title, "Item #1.1");
            Assert.Equal(items[0].Items[1].Title, "Item #1.2");

            Assert.Equal(items[1].Items[0].Title, "Item #2.1");
            Assert.Equal(items[1].Items[1].Title, "Item #2.2");
            Assert.Equal(items[1].Items[2].Title, "Item #2.3");
        }

        [Fact]
        public void ShouldCreateNestedChildItemsWhenFollowingLinesStartWithMoreWhitespace()
        {
            const string demoText = @"This is a parent
 This is a child node
          This is a grand-child node
           This is a grand-grand-child node
";
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 1);
            Assert.Equal(items[0].Title, "This is a parent");
            Assert.Equal(items[0].Items.Count, 1);
            Assert.Equal(items[0].Items[0].Title, "This is a child node");
            Assert.Equal(items[0].Items[0].Items.Count, 1);
            Assert.Equal(items[0].Items[0].Items [0].Title, "This is a grand-child node");
            Assert.Equal(items[0].Items[0].Items [0].Items.Count, 1);
            Assert.Equal(items[0].Items[0].Items [0].Items [0].Title, "This is a grand-grand-child node");
        }

        [Fact]
        public void ShouldReduuceNestingWhenFollowingLinesStartWithLessWhitespace()
        {
            const string demoText = @"This is a parent
 This is a child node
          This is a grand-child node
           This is a grand-grand-child node
           This is a second grand-grand-child node
    This is a second child node
 This is a third child node
This is a single top-level node
";
            var items = GuessWhat.MainApp.Models.ChecklistItem.FromText(demoText);
            Assert.NotNull(items);
            Assert.Equal(items.Count, 2);
            Assert.Equal(items[0].Title, "This is a parent");
            Assert.Equal(items[0].Items.Count, 3);
            Assert.Equal(items[0].Items[0].Title, "This is a child node");
            Assert.Equal(items[0].Items[0].Items.Count, 1);
            Assert.Equal(items[0].Items[0].Items[0].Title, "This is a grand-child node");
            Assert.Equal(items[0].Items[0].Items[0].Items.Count, 2);
            Assert.Equal(items[0].Items[0].Items[0].Items[0].Title, "This is a grand-grand-child node");
            Assert.Equal(items[0].Items[0].Items[0].Items[1].Title, "This is a second grand-grand-child node");
            Assert.Equal(items[0].Items[1].Title, "This is a second child node");
            Assert.Null(items[0].Items[1].Items);
            Assert.Equal(items[0].Items[2].Title, "This is a third child node");
            Assert.Null(items[0].Items[2].Items);
            Assert.Equal(items[1].Title, "This is a single top-level node");
        }

    }
}
