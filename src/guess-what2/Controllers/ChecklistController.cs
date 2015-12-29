using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace guess_what2.Controllers
{
    [Route("c")]
    public class ChecklistController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new List<string>() {
                            "Lorem ipsum dolor sit amet, consetetur sadipscing elitr",
                            "ed diam nonumy eirmod tempor invidunt ut labore et dolore",
                            "magna aliquyam erat, sed diam voluptua. At vero eos et accusam et",
                            "justo duo dolores et ea rebum. Stet clita kasd gubergren",
                            "no sea takimata sanctus est Lorem ipsum dolor sit amet",
                            "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy",
                            "eirmod tempor invidunt ut labore et dolore magna aliquyam erat",
                        });
        }
    }
}
