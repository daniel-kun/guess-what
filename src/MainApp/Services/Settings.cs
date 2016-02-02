namespace Io.GuessWhat.MainApp.Services
{
    /**
    Global settings for the Services.
    **/
    public class Settings
    {
        /// The API key used to authenticate to the cloud converter API.
        public string CloudConvertApiKey
        {
            get;
            set;
        }


        /**
        The connection string for the azure blob storage account that
        is used to store the badges in PNG format.
        If it is "DEVELOPMENT", the development account will be used.
        **/
        public string AzureBadgesStorageAccountConnectionString
        {
            get;
            set;
        }
    }
}
