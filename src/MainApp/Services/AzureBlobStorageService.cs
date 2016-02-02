using Microsoft.Extensions.OptionsModel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Io.GuessWhat.MainApp.Services
{
    /**
    @see IAzureBlobStorageService
    **/
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        public AzureBlobStorageService (IOptions<Settings> settings)
        {
            mSettings = settings.Value;
            if (mSettings.AzureBadgesStorageAccountConnectionString == "DEVELOPMENT")
            {
                mStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            }
            else
            {
                mStorageAccount = CloudStorageAccount.Parse(mSettings.AzureBadgesStorageAccountConnectionString);
            }
            mBlobClient = mStorageAccount.CreateCloudBlobClient();
        }

        public void UploadBlobFromStream(string container, string fileName, Stream content)
        {
            CloudBlobContainer cloudContainer = GetBlobContainer(container);
            var blob = cloudContainer.GetBlockBlobReference(fileName);
            blob.UploadFromStream(content);
        }

        public Task CopyBlobContentToStream(string container, string fileName, Stream output)
        {
            CloudBlobContainer cloudContainer = GetBlobContainer(container);
            CloudBlob blob = cloudContainer.GetBlobReference(fileName);
            if (blob.Exists())
            {
                return blob.DownloadToStreamAsync(output);
            }
            else
            {
                return null;
            }
        }

        /**
        Gets a BlobContainer for the given container within the current mStorageAccount.
        CloudBlobContainer objects are cached so that there are not multiple connections
        necessary.
        **/
        private CloudBlobContainer GetBlobContainer(string container)
        {
            if (!mContainers.ContainsKey(container))
            {
                mContainers[container] = mBlobClient.GetContainerReference(container);
            }
            return mContainers[container];
        }

        private CloudStorageAccount mStorageAccount;
        private CloudBlobClient mBlobClient;
        private Dictionary<string, CloudBlobContainer> mContainers = new Dictionary<string, CloudBlobContainer>();
        private Settings mSettings;

    }
}
