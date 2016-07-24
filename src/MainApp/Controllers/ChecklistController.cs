using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Repositories;
using Io.GuessWhat.MainApp.Models;
using System;
using Io.GuessWhat.MainApp.ViewModels;
using Io.GuessWhat.MainApp.Services;

namespace Io.GuessWhat.MainApp.Controllers
{
    [Route("c")]
    public class ChecklistController : Controller
    {
        public ChecklistController (IChecklistRepository checklistRepository, ISpamDetectionService spamDetectionService)
        {
            mChecklistRepository = checklistRepository;
            mSpamDetectionService = spamDetectionService;
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
                var viewModel = ChecklistResultViewModel.FromResult(checklistResult);
                viewModel.OriginatingHost = HttpContext.Request.Host.Value;
                return View(viewModel);
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
            return View(new ChecklistViewModel()
            {
                Title = string.Empty,
                Description = string.Empty,
                Items = @"Checklist Item #1
Checklist Item #2
Checklist Item #3
",
           });
        }

        [HttpGet("new/{id}")]
        public IActionResult New(string id)
        {
            var template = mChecklistRepository.LoadChecklistModel(id);
            var viewModel = ChecklistViewModel.FromModel(template);
            return View(viewModel);
        }

        [HttpPost("new")]
        public IActionResult New(ChecklistViewModel viewModel)
        {
            var model = ChecklistModel.FromViewModel(viewModel);
            if (!mSpamDetectionService.IsSpamDescription(model.Description))
            {
                // No Spam
                mChecklistRepository.SaveChecklistModel(model);
                return Redirect($"/c/{model.Id}");
            }
            else
            {
                // This is SPAM! Don't save the result and redirect
                // to some random result so that the spam-poster thinks
                // it was successful.
                return Redirect($"/c/UFlFEI2RY0CbyPhPvmoqAQ");
            }
        }

        private IChecklistRepository mChecklistRepository;
        private ISpamDetectionService mSpamDetectionService;
    }
}
