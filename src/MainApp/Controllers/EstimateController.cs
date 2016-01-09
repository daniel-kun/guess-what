using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Io.GuessWhat.MainApp.Controllers
{
    [Route("e")]
    public class EstimateController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            return View(id);
        }

        [HttpPost]
        public IActionResult Post(EstimateModel item)
        {
            return RedirectToAction("Index", new { id = item.estimate });
        }

    }
}
