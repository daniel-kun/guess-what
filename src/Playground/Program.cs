using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;

namespace Io.GuessWhat.Playground
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Program
    {
        public static int Main(string[] args)
        {
            var account = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(
                "DefaultEndpointsProtocol=https;AccountName=guesswhatbadges;AccountKey=KPAq/jZtjX3g4E7BAZVdip6lwKEmwlf083DNg0yVSGaYrst9TismGctP+iq34QIwy6CsfoBhH34hzEZANBdcGQ==;BlobEndpoint=https://guesswhatbadges.blob.core.windows.net/;TableEndpoint=https://guesswhatbadges.table.core.windows.net/;QueueEndpoint=https://guesswhatbadges.queue.core.windows.net/;FileEndpoint=https://guesswhatbadges.file.core.windows.net/");
            var blobClient = account.CreateCloudBlobClient();
            var badgesContainer = blobClient.GetContainerReference("badges");
            var fooPngBlob = badgesContainer.GetBlockBlobReference("foo.png");
            fooPngBlob.UploadFromFile(@"C:\Temp\foo.png", FileMode.Open);
            Console.WriteLine(fooPngBlob.Uri.ToString());
            Console.ReadLine();
            return 0;
        }
    }
}
