using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.IO;

namespace Io.GuessWhat.Tools.Web
{
    /**
    Renders a raw .svg file from a stream in .png format to the response body.
    The respone's ContentType will be changed to image/png.
    **/
    public class SvgRenderer : IActionResult
    {
        public SvgRenderer(Stream svgStream)
        {
            mSvgStream = svgStream;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "image/png";
            return Graphics.SvgToPng.ConvertSvgToPng(mSvgStream, context.HttpContext.Response.Body);
        }

        private Stream mSvgStream;
    }

}
