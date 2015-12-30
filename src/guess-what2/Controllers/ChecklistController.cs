using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using guess_what2.DataSources;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace guess_what2.Controllers
{
    [Route("c")]
    public class ChecklistController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var dataSource = new ChecklistDataSource();
            var checklistModel = dataSource.LoadChecklistModel("FAKE");
            return View(checklistModel);
        }
    }
}
