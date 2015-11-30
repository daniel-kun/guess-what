using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using guess_what2.Models;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace guess_what2.Controllers
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
