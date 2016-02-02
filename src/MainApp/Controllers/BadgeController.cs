using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Repositories;
using Io.GuessWhat.MainApp.ViewModels;
using Io.GuessWhat.MainApp.Services;
using System.IO;

namespace Io.GuessWhat.MainApp.Controllers
{
    /**
    The BadgeController provides checklist result badges in .svg and .png format.
    **/
    [Route("badge")]
    public class BadgeController : Controller
    {
        public BadgeController(IChecklistRepository checklistRepository,
                               ICloudConverterService cloudConverter,
                               IAzureBlobStorageService blobStorageService)
        {
            mChecklistRepository = checklistRepository;
            mCloudConverter = cloudConverter;
            mBlobStorageService = blobStorageService;
        }

        /**
        @brief Delivers a badge in .png format for the result with the given id.

        The .png file is first looked up in the "badges" Azure Storage container and delivered
        from there, if it exists. If it does not exist there, the source .svg badge 
        (see Views/Badge/Svg.cshtml) is converted to png using cloudconvert.com, the result is 
        uploaded to an Azure Storage blob and then delivered in the response.
        **/
        [Route("{id}.png")]
        public IActionResult Index(string id)
        {
            return new Tools.Web.CustomActionResult((ActionContext context) =>
            {
                const string blobContainer = "badges";
                string blobFileName = $"{id}.png";
                var blobCopyTask = mBlobStorageService.CopyBlobContentToStream(blobContainer, blobFileName, context.HttpContext.Response.Body);
                if (blobCopyTask != null)
                {
                    return blobCopyTask;
                }
                else
                {
                    // This could be solved a bit more complex, but more efficiently,
                    // if the ResponseStream of the cloud-converter download would
                    // directly write into a pre-created CloudBlob stream.
                    using (var mem = new MemoryStream())
                    {
                        var convertTask = mCloudConverter.Convert(
                            $"http://{context.HttpContext.Request.Host}/badge/{id}.svg",
                            mem);
                        convertTask.Wait(10 * 1000); // wait a maximum of 10 seconds
                        mem.Position = 0;
                        mBlobStorageService.UploadBlobFromStream(blobContainer, blobFileName, mem);
                        mem.Position = 0;
                        return mem.CopyToAsync(context.HttpContext.Response.Body);
                    }
                }
            });
        }

        /**
        Delivers a badge in .svg format
        **/
        [Route("{id}.svg")]
        public IActionResult Svg(string id)
        {
            var resultModel = mChecklistRepository.LoadChecklistResultModel(id);
            if (resultModel == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewModel = ChecklistResultViewModel.FromResult(resultModel);
                HttpContext.Response.ContentType = "image/svg+xml";
                return PartialView(viewModel);
            }
        }

        private IChecklistRepository mChecklistRepository;
        private ICloudConverterService mCloudConverter;
        private IAzureBlobStorageService mBlobStorageService;
    }
}
