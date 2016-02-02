using System.IO;
using System.Threading.Tasks;

namespace Io.GuessWhat.MainApp.Services
{
    /**
    The CloudConverterService converts SVG files to PNG via cloudconvert.com.
    **/
    public interface ICloudConverterService
    {
        Task Convert(string svgSourceUrl, Stream output);
    }
}
