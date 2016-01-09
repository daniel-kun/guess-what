namespace guess_what2.Controllers
{
    using Microsoft.AspNet.Mvc;
    using System.Collections.Generic;
    using Models;

    [Route("test")]
    public class TestController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var resultModel = new ChecklistResultModel()
            {
                TemplateId = "3B0E78BEE31A401BB69D2F",
                Results = new List<ChecklistResultItem>
                {
                    new ChecklistResultItem ()
                    {
                        TemplateItemId = "A1EA346BAF1C46068C1EC7",
                        Result = ChecklistResult.CheckedAndOk,
                    },
                    new ChecklistResultItem ()
                    {
                        TemplateItemId = "FCF00786BBE747D980C5FA",
                        Result = ChecklistResult.CheckedAndNotOk,
                    },
                    new ChecklistResultItem ()
                    {
                        TemplateItemId = "13889AF6FFA44609940B31",
                        Result = ChecklistResult.NotChecked,
                    },
                }
            };

            return Json(resultModel);
        }
    }
}
