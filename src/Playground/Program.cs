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
            var html = CommonMark.CommonMarkConverter.Convert("*Foobar* **Bold!** [Google](http://www.google.com)");
            Console.WriteLine(html);
            Console.ReadLine();
            return 0;
        }
    }
}
