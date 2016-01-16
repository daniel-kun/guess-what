using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Repositories;
using Io.GuessWhat.MainApp.Models;
using System;

namespace Io.GuessWhat.MainApp.Controllers
{
    [Route("c")]
    public class ChecklistController : Controller
    {
        public ChecklistController (IChecklistRepository checklistRepository)
        {
            mChecklistRepository = checklistRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var templates = mChecklistRepository.LoadChecklistBrowseItems();
            return View(templates);
        }

        [HttpGet("{id}")]
        public IActionResult FillOut(string id)
        {
            var checklistModel = mChecklistRepository.LoadChecklistModel(id);
            if (checklistModel == null)
            {
                return HttpNotFound(id);
            } else { 
                return View(checklistModel);
            }
        }

        [HttpGet("result/{id}")]
        public IActionResult Result(string id)
        {
            var checklistResult = mChecklistRepository.LoadChecklistResultModel(id);
            if (checklistResult == null)
            {
                return HttpNotFound(id);
            } else
            {
                return View(checklistResult);
            }
        }

        [HttpPost("result")]
        public IActionResult Result(ChecklistResultModel item)
        {
            item.CreationTime = DateTime.Now;
            ChecklistResultModel result = mChecklistRepository.SaveChecklistResultModel(item);
            return Json(result);
            // FIXME: This does not use a RESTful url such as /c/result/1234, but instead
            // redirects to /c/result?id=1234
            //return RedirectToAction("Result", new { id = id });
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("new")]
        public IActionResult New(ChecklistViewModel viewModel)
        {
            var model = ChecklistModel.FromViewModel(viewModel);
            mChecklistRepository.SaveChecklistModel(model);
            return Redirect($"/c/{model.Id}");
        }

        private IChecklistRepository mChecklistRepository;

    }
}
