using Microsoft.Extensions.OptionsModel;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Io.GuessWhat.MainApp.Services
{
    public class CloudConverterService : ICloudConverterService
    {
        public CloudConverterService(IOptions<Settings> settings)
        {
            mSettings = settings.Value;
        }

        public Task Convert(string svgSourceUrl, Stream output)
        {
            var svgRequest = WebRequest.CreateHttp(svgSourceUrl);
            string svgBase64 = null;
            using (var mem = new MemoryStream())
            {
                svgRequest.GetResponse().GetResponseStream().CopyTo(mem);
                svgBase64 = WebUtility.UrlEncode(System.Convert.ToBase64String(mem.ToArray()));
            }
            if (svgBase64 != null)
            {
                var convertRequest = WebRequest.CreateHttp(
                    String.Format(
                        @"https://api.cloudconvert.com/convert?apikey={0}&input=base64&filename=foo.svg&download=inline&inputformat=svg&outputformat=png&file={1}",
                        mSettings.CloudConvertApiKey,
                        svgBase64));
                return convertRequest.GetResponse().GetResponseStream().CopyToAsync(output);
            } else
            {
                throw new FileNotFoundException($"File not found: {svgSourceUrl}", svgSourceUrl);
            }
        }

        private Settings mSettings;
    }
}
