namespace Io.GuessWhat.Tools.Web
{
    /**
    Provides utility functions for handling URLs.
    **/
    public static class Url
    {
        /**
        Replaces all url-unsafe characters from a base64 encoding into url-safe characters.
        This means "+" becomes "-" and "/" becomes "_".

        **/
        public static string ReplaceUrlUnsafeChars(string base64EncodedString)
        {
            return base64EncodedString.Replace('+', '-').Replace('/', '_');
        }

    }
}
