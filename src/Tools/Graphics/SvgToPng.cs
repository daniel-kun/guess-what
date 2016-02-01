using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Io.GuessWhat.Tools.Graphics
{
    public class SvgToPng
    {
        public static Task ConvertSvgToPng(Stream input, Stream output)
        {
            Svg.SvgDocument doc = Svg.SvgDocument.Open<Svg.SvgDocument>(input);
            using (Bitmap bmp = doc.Draw())
            {
                using (var mem = new MemoryStream())
                {
                    bmp.Save(mem, ImageFormat.Png);
                    mem.Position = 0;
                    return mem.CopyToAsync(output);
                }
            }
        }
    }
}
