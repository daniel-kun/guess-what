using System;
using System.Text.RegularExpressions;

namespace Io.GuessWhat.MainApp.Services
{
    public class SpamDetectionService : ISpamDetectionService
    {
        public bool IsSpamDescription(string description)
        {
            return regexSpamDescription.Matches(description.Trim()).Count >= 1;
        }

        Regex regexSpamDescription = new Regex("^<a href=\"http://.*#\\d*\">.*</a>,$");
    }
}
