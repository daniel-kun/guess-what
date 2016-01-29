using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.Models.ChecklistModel
{
    public class FromViewModelTests
    {
        [Fact]
        public void ShouldPreserveTitleAndDescriptionWhenTitleIsShorterThanDescription()
        {
            const string testTitle = "TitleShorter";
            const string testDescription = "DescriptionIsLongerThanTitle";
            var testData = new Io.GuessWhat.MainApp.ViewModels.ChecklistViewModel()
            {
                Title = testTitle,
                Description = testDescription,
                Items = string.Empty,
            };
            var result = GuessWhat.MainApp.Models.ChecklistModel.FromViewModel(testData);
            Assert.Equal(result.Title, testData.Title);
            Assert.Equal(result.Description, testData.Description);
        }

        [Fact]
        public void ShouldPreserveTitleAndDescriptionWhenTitleIsLongerThanDescription()
        {
            const string testTitle = "TitleIsLongerThanDesription";
            const string testDescription = "DescriptionShorter";
            var testData = new Io.GuessWhat.MainApp.ViewModels.ChecklistViewModel()
            {
                Title = testTitle,
                Description = testDescription,
                Items = string.Empty,
            };
            var result = GuessWhat.MainApp.Models.ChecklistModel.FromViewModel(testData);
            Assert.Equal(result.Title, testData.Title);
            Assert.Equal(result.Description, testData.Description);
        }

        [Fact]
        public void ShouldCreateAViewModelWithTitleAndDescriptionAndItems()
        {
            const string testTitle = "Hello, World!";
            const string testDescription = "Lorel ipsum";
            const string testItems = "Item1\r\nItem2";
            var testData = new Io.GuessWhat.MainApp.ViewModels.ChecklistViewModel()
            {
                Title = testTitle,
                Description = testDescription,
                Items = testItems,
            };
            var result = GuessWhat.MainApp.Models.ChecklistModel.FromViewModel(testData);
            Assert.Equal(result.Title, testData.Title);
            Assert.Equal(result.Description, testData.Description);
            Assert.Equal(result.Items.Count, 2);
            Assert.Equal(result.Items[0].Title, "Item1");
            Assert.Equal(result.Items[1].Title, "Item2");
        }

        [Fact]
        public void ShouldLimitTitleAndDescriptionToMaxSize()
        {
            string testTitle = new string('A', GuessWhat.MainApp.Models.ChecklistModel.TitleMaxLength + 20);
            string testDescription = new string('A', GuessWhat.MainApp.Models.ChecklistModel.DescriptionMaxLength + 20);
            var testData = new Io.GuessWhat.MainApp.ViewModels.ChecklistViewModel()
            {
                Title = testTitle,
                Description = testDescription,
                Items = string.Empty,
            };
            var result = GuessWhat.MainApp.Models.ChecklistModel.FromViewModel(testData);
            Assert.True(result.Title.Length < testData.Title.Length);
            Assert.True(result.Description.Length < testData.Description.Length);
            Assert.Equal(result.Title.Length, GuessWhat.MainApp.Models.ChecklistModel.TitleMaxLength);
            Assert.Equal(result.Description.Length, GuessWhat.MainApp.Models.ChecklistModel.DescriptionMaxLength);
            Assert.Equal(result.Title, testData.Title.Substring(0, GuessWhat.MainApp.Models.ChecklistModel.TitleMaxLength));
            Assert.Equal(result.Description, testData.Description.Substring(0, GuessWhat.MainApp.Models.ChecklistModel.DescriptionMaxLength));
        }
    }
}
