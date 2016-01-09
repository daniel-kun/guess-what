using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Io.GuessWhat.MainApp.Models;
using System.Diagnostics;
using System.Globalization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Io.GuessWhat.MainApp.Controllers
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
            var model = new HomeModel();
            ViewData.Model = model;
            if (HttpContext.Request.Query.ContainsKey ("e"))
            {
                var savedEstimateOldStyle = HttpContext.Request.Query ["e"];
                ParseOldStyleUrl(model, savedEstimateOldStyle);
            }
            return View();
        }

        /**
        Parses the URL savedEstimateOldStyle, which is in the old estimation save url
        and sets the according difficulties in model.PreconditionItems and 
        model.ComplexityItems and the estimate in model.Estimate.
        **/
        private static void ParseOldStyleUrl (HomeModel model, string savedEstimateOldStyle)
        {
            // The old estimate url schema is:
            var urlScheme = "nnnnPPPPPPPPCCCCCCCC";
            // Where n is the original zero-left-padded estimate value,
            // P is the selected precondition difficulty (0, A, B, C, D)
            // C is the selected complexity difficulty (0, A, B, C, D)
            // Preconditions and complexity have the same order as displayed.
            if (savedEstimateOldStyle.Length == "nnnnPPPPPPPPCCCCCCCC".Length)
            {
                int estimate;
                if (int.TryParse(savedEstimateOldStyle.Substring(0, urlScheme.Count(c => c == 'n')),
                                 NumberStyles.HexNumber,
                                 CultureInfo.InvariantCulture.NumberFormat,
                                 out estimate))
                {
                    model.Estimate = estimate;
                    int preconditionCount = urlScheme.Count(c => c == 'P');
                    Debug.Assert(preconditionCount == model.PreconditionItems.Count);
                    for (int i = 0; i < preconditionCount; ++i)
                    {
                        model.PreconditionItems[i].SelectedDifficulty = CodeToNumericDifficulty(savedEstimateOldStyle[4 + i]);
                    }
                    int complexityCount = urlScheme.Count(c => c == 'C');
                    Debug.Assert(complexityCount == model.ComplexityItems.Count);
                    for (int i = 0; i < complexityCount; ++i)
                    {
                        model.ComplexityItems[i].SelectedDifficulty = CodeToNumericDifficulty(savedEstimateOldStyle[4 + preconditionCount + i]);
                    }
                }
            }
        }

        /**
        Converts the code that encodes the difficulty in the old style url to the numeric
        value as it is stored in model.{PreconditionItems,ComplexityItems}.SelectedDifficulty.
        **/
        private static int CodeToNumericDifficulty(char theCode)
        {
            switch (theCode)
            {
                case '0':
                    return 0;
                case 'A':
                    return 1;
                case 'B':
                    return 2;
                case 'C':
                    return 3;
                case 'D':
                    return 4;
            }
            throw new ArgumentOutOfRangeException ("theCode");
        }
    }
}
