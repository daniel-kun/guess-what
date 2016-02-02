using Microsoft.AspNet.Mvc;
using System;
using System.Threading.Tasks;

namespace Io.GuessWhat.Tools.Web
{
    /**
    Helper-class to implement an IActionResult ad-hoc using a lambda.

    Example to deliver a raw file in a Controller:
    {{code}}
    public IActionResult Index()
    {
        return new CustomActionResult((ActionContext context) => {
            using (var stream = new FileStream("foo.txt", FileMode.Open)) {
                return stream.CopyToAsync(context.HttpContext.Response.Body);
            }
        });
    }
    {{code}}
    **/
    public class CustomActionResult : IActionResult
    {
        public CustomActionResult (Func<ActionContext, Task> executeResultAsync)
        {
            mExecuteResultAsync = executeResultAsync;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return mExecuteResultAsync(context);
        }

        private Func<ActionContext, Task> mExecuteResultAsync;

    }
}
