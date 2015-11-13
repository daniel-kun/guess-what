using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using guess_what2.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace guess_what2.Controllers
{
    /**
    The home page (aka landing page) consists of four full-screen parts:
    #1 _PageEnterEstimation.csthml
        Welcoming message and input to enter an estimation.
    #2 _PagePreconditions.csthml
        Assess how good or bad your preconditions are. Acceept with "Next questions"
    #3 The complexity (not yet implemented)
        Assess how complex the tasks are. Accept with "See results"
    #4 The results (not yet implemented)
        See your original estimation, the calculated factor and the "final" estimation.
    
    All pages are based on the model Models.HomeModel.
    **/
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData.Model = new HomeModel();
            return View();
        }
    }
}
