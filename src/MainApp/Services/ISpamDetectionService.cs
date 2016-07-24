namespace Io.GuessWhat.MainApp.Services
{
    /**
    The ISpamDetectionService contains algorithms to detect spam, so that the HTTP stack or
    individual controllers can act accordingly, like denying or ignoring requests.
    **/
    public interface ISpamDetectionService
    {
        /**
        Returns true when `description' contains known patterns of spam bots.
        **/
        bool IsSpamDescription(string description);
    }
}
