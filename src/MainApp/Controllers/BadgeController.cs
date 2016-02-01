using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Repositories;
using Io.GuessWhat.MainApp.ViewModels;
using System.Net;
using Io.GuessWhat.Tools.Web;

namespace Io.GuessWhat.MainApp.Controllers
{
    /**
    The BadgeController provides checklist result badges in .svg and .png format.
    **/
    [Route("badge")]
    public class BadgeController : Controller
    {
        public BadgeController(IChecklistRepository checklistRepository)
        {
            mChecklistRepository = checklistRepository;
        }

        /**
        Delivers a badge in .png format
        **/
        [Route("{id}.png")]
        public IActionResult Index(string id)
        {
            var svgRequest = WebRequest.CreateHttp("http://localhost:54730/badge/K1HsV7Jda0mTRYR-Y6BTOQ.svg");
            svgRequest.Method = "GET";
            return new SvgRenderer(svgRequest.GetResponse().GetResponseStream());
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
                return PartialView(viewModel);
            }
        }

        private IChecklistRepository mChecklistRepository;

    }
}
