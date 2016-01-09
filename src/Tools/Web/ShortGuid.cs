using System;

namespace Io.GuessWhat.Tools.Web
{
    /**
    Provides functions to create short, url-friendly GUIDs.
    **/
    public static class ShortGuid
    {
        /**
        Creates a GUID of 22 character length via base64 decoding.
        **/
        public static string CreateGuid()
        {
            byte[] bytes = Guid.NewGuid().ToByteArray();
            string guidWithPadding = Convert.ToBase64String(bytes);
            string urlSafeGuid = Url.ReplaceUrlUnsafeChars(guidWithPadding);
            if (urlSafeGuid.EndsWith("==") && urlSafeGuid.Length == 24)
            {
                return urlSafeGuid.Substring(0, 22);
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
