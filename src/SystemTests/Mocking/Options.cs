using Microsoft.Extensions.OptionsModel;

namespace Io.GuessWhat.SystemTests.Mocking
{
    /**
    Mock implementation of Microsoft.Extensions.OptionsModel.IOptions<T> for use in system tests.
    **/
    public class Options <TOptions> : IOptions <TOptions> where TOptions : class, new()
    {

        /// Create a new Options object with the given value.
        public Options (TOptions value)
        {
            Value = value;
        }

        public TOptions Value
        {
            get;
            private set;
        }
    }
}
