using System;

namespace Io.GuessWhat.Tools.Web
{
    /**
    Provides functions that help with handling data that was received from form inputs.
    **/
    public static class FormInput
    {
        /**
        @brief Prepares and secures text that has been received by a form input.

        Current fixes:
        - Converts null to string.Empty
        - ensures that non-null strings are not longer than maxLength characters (not bytes).
        **/
        public static string PrepareText(string text, int maxLength)
        {
            string result = text ?? string.Empty;
            return result.Substring(0, Math.Min(maxLength, result.Length));
        }
    }
}
