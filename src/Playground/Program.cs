using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;

namespace Io.GuessWhat.Playground
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Program
    {
        public static int Main(string[] args)
        {
            var svgRequest = WebRequest.CreateHttp("http://localhost:5000/badge/K1HsV7Jda0mTRYR-Y6BTOQ.svg");
            string svgBase64 = null;
            using (var mem = new MemoryStream()) {
                svgRequest.GetResponse().GetResponseStream().CopyTo(mem);
                svgBase64 = HttpUtility.UrlEncode(Convert.ToBase64String(mem.ToArray()));
            }
            if (svgBase64 != null)
            {
                using (var fileStream = new FileStream(@"C:\temp\foo.png", FileMode.Create))
                {
                    var convertRequest = WebRequest.CreateHttp(
                        String.Format(
                            @"https://api.cloudconvert.com/convert?apikey={0}&input=base64&filename=foo.svg&download=inline&inputformat=svg&outputformat=png&file={1}",
                            "gxdp32cNyYd6-1dq8Jdby9jyVSHmRQlyowvclreHg_l-8tklvISX1f-dHmhyZqJ8bYr-xXnE8zfBaWr-2vbj6w",
                            svgBase64));
                    convertRequest.GetResponse().GetResponseStream().CopyTo(fileStream);
                    fileStream.Flush();
                }

            }
            Console.ReadLine();
            return 0;
        }
    }
}
