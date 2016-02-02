using System.IO;
using System.Threading.Tasks;

namespace Io.GuessWhat.MainApp.Services
{
    /**
    Service to upload and download files from an Azure Storage Account.
    @see Settings.AzureBadgesStorageAccountConnectionString
    **/
    public interface IAzureBlobStorageService
    {
        /**
        Uploads a stream as a Azure Blob Storage blob in the given container, with the given fileName.
        If the container does not exist, an exception is thrown.
        If a blob with the fileName already exists in the same container, the content is overwritten.
        **/
        void UploadBlobFromStream(string container, string fileName, Stream content);

        /**
        Copies the raw contents of the blob fileName in the given container to the output
        stream and returns a Task that tracks the progress.
        Returns null when a blob named fileName does not exist in the given container.
        **/
        Task CopyBlobContentToStream(string container, string fileName, Stream output);
    }
}
