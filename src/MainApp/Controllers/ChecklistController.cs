using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Repositories;
using Io.GuessWhat.MainApp.Models;

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
            var templates = mChecklistRepository.LoadChecklistModelCollection();
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
        public IActionResult Result(Models.ChecklistResultModel item)
        {
            ChecklistResultModel result = mChecklistRepository.SaveChecklistResultModel(item);
            return Json(result);
            // FIXME: This does not use a RESTful url such as /c/result/1234, but instead
            // redirects to /c/result?id=1234
            //return RedirectToAction("Result", new { id = id });
        }

        private IChecklistRepository mChecklistRepository;

    }
}
