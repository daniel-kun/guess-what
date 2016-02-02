using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Repositories;
using Io.GuessWhat.MainApp.ViewModels;
using Io.GuessWhat.MainApp.Services;

namespace Io.GuessWhat.MainApp.Controllers
{
    /**
    The BadgeController provides checklist result badges in .svg and .png format.
    **/
    [Route("badge")]
    public class BadgeController : Controller
    {
        public BadgeController(IChecklistRepository checklistRepository, ICloudConverterService cloudConverter)
        {
            mChecklistRepository = checklistRepository;
            mCloudConverter = cloudConverter;
        }

        /**
        Delivers a badge in .png format
        **/
        [Route("{id}.png")]
        public IActionResult Index(string id)
        {
            return new Tools.Web.CustomActionResult((ActionContext context) =>
            {
                return mCloudConverter.Convert(
                    $"http://{context.HttpContext.Request.Host}/badge/{id}.svg",
                    context.HttpContext.Response.Body);
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
    }
}
