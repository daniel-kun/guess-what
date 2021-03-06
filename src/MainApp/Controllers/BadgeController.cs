﻿using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Repositories;
using Io.GuessWhat.MainApp.ViewModels;
using Io.GuessWhat.MainApp.Services;
using System.IO;
using Microsoft.AspNet.Http;
using System;

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

        /**
        @brief Delivers a badge in .png format for the result with the given id.

        The .png file is first looked up in the "badges" Azure Storage container and delivered
        from there, if it exists. If it does not exist there, the source .svg badge 
        (see Views/Badge/Svg.cshtml) is converted to png using cloudconvert.com, the result is 
        uploaded to an Azure Storage blob and then delivered in the response.
        **/
        [Route("{id}.png")]
        public IActionResult Png(string id)
        {
            return new Tools.Web.CustomActionResult((ActionContext context) =>
            {
                const string blobContainer = "badges";
                string blobFileName = $"{id}.png";
                var blobCopyTask = mBlobStorageService.CopyBlobContentToStream(blobContainer, blobFileName, context.HttpContext.Response.Body);
                if (blobCopyTask != null)
                {
                    HttpContext.Response.ContentType = "image/png";
                    return blobCopyTask;
                }
                else
                {
                    // This could be solved a bit more complex, but more efficiently,
                    // if the ResponseStream of the cloud-converter download would
                    // directly write into a pre-created CloudBlob stream.
                    using (var mem = new MemoryStream())
                    {
                        string host = ApplyHostNameWorkaround(context.HttpContext.Request.Host.Value);
                        var convertTask = mCloudConverter.Convert(
                            $"http://{host}/badge/{id}.svg",
                            id,
                            mem);
                        convertTask.Wait(10 * 1000); // wait a maximum of 10 seconds
                        mem.Position = 0;
                        mBlobStorageService.UploadBlobFromStream(blobContainer, blobFileName, mem);
                        mem.Position = 0;
                        HttpContext.Response.ContentType = "image/png";
                        return mem.CopyToAsync(context.HttpContext.Response.Body);
                    }
                }
            });
        }

        /**
        This is a workaround for Web Apps running on Azure.
        
        It seems that within the Azure Web App, the custom host name for this app is not
        resolved to the correct IP, hence a WebRequest to that custom host will end up at a different
        or no HTTP server at all.

        Since guess-what.io is the custom host name, it can not be used to download the SVG badge
        because of this issue.

        Hence, when host is guess-what.io, the alternative host name guess-what.azurewebsites.net
        is returned. If host is anything else, it will be returned unaltered.
        **/
        private static string ApplyHostNameWorkaround(string host)
        {
            if (host.ToUpper() == "guess-what.io".ToUpper())
            {
                return "guess-what.azurewebsites.net";
            }
            else
            {
                return host;
            }
        }

        private IChecklistRepository mChecklistRepository;
        private ICloudConverterService mCloudConverter;
        private IAzureBlobStorageService mBlobStorageService;
    }
}
