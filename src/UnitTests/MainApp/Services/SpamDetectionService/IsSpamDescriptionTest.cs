using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.Services.SpamDetectionService
{
    public class IsSpamDetectionTest
    {
        [Fact]
        public void ShouldNotReportNormalDescriptionAsSpam()
        {
            var sut = new Io.GuessWhat.MainApp.Services.SpamDetectionService();
            Assert.False(sut.IsSpamDescription("This is just some regular description <a href=\"http://www.google.de\">with a link</a>."));
        }

        [Fact]
        public void ShouldReportFirstSpamDescriptionAsSpam()
        {
            var sut = new Io.GuessWhat.MainApp.Services.SpamDetectionService();
            Assert.True(sut.IsSpamDescription("<a href=\"http://singulair10mg.top/#2898\">singulair</a>,"));
        }

        [Fact]
        public void ShouldReportFirstSpamDescriptionAsSpamWhenItIsSurroundedByWhitespace()
        {
            var sut = new Io.GuessWhat.MainApp.Services.SpamDetectionService();
            Assert.True(sut.IsSpamDescription("\t\r\n   <a href=\"http://singulair10mg.top/#2898\">singulair</a>,  \t"));
        }

        [Fact]
        public void ShouldNotReportSimilarToFirstDescriptionAsSpam()
        {
            var sut = new Io.GuessWhat.MainApp.Services.SpamDetectionService();
            Assert.False(sut.IsSpamDescription("Hello! <a href=\"http://singulair10mg.top/#289\">singulair</a>."));
        }
    }
}
