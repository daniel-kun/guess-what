using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Repositories;
using Io.GuessWhat.MainApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Io.GuessWhat.MainApp.Controllers
{
    [Route("c")]
    public class ChecklistController : Controller
    {
        public ChecklistController (IChecklistRepository checklistRepository)
        {
            mChecklistRepository = checklistRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var templates = mChecklistRepository.LoadChecklistBrowseItems();
            return View(templates);
        }

        [Route("{id}")]
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

        [Route("result/{id}")]
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

        [HttpPost("new")]
        public IActionResult New(ChecklistViewModel viewModel)
        {
            var model = ChecklistModelFromViewModel(viewModel);
            mChecklistRepository.SaveChecklistModel(model);
            return Redirect($"/c/{model.Id}");
        }

        /**
        Creates a ChecklistModel object from a ChecklistViewModel. The multi-line text in
        viewModel.Items will be converted to a list of ChecklistItems in the resulting 
        ChecklistModel's Items.
        **/
        public static ChecklistModel ChecklistModelFromViewModel (ChecklistViewModel viewModel)
        {
            return new ChecklistModel()
            {
                Title = Tools.Web.FormInput.PrepareText (viewModel.Title, ChecklistModel.TitleMaxLength),
                Description = Tools.Web.FormInput.PrepareText(viewModel.Description, ChecklistModel.DescriptionMaxLength),
                Items = CreateChecklistItemsFromText(viewModel.Items)
            };
        }

        /**
        @brief Converts a string with 0-n lines of text into a list of ChecklistItems.
        The text in each line will be used as the ChecklistItem's titles.

        Lines that contain only whitespace will be ignored. Newline delimiter are \r and \n (in
        any order and combination).

        @param items The multiline text that will be converted to a list of ChecklistItems.
        @return A list of ChecklistItems with the Titles specified in items.
        **/
        public static List<ChecklistItem> CreateChecklistItemsFromText(string items)
        {
            string[] lines = items.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var result = lines.Select(
                line =>
           {
               if (line.Count(c => ! Char.IsWhiteSpace(c)) == 0)
               {
                   return null;
               }
               else
               {
                   return new ChecklistItem() { Title = line };
               }
           });
            var list = result.Where(line => line != null).ToList();
            return list;
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        private IChecklistRepository mChecklistRepository;

    }
}
