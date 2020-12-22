using System;
using System.IO;
using System.Reflection;
using System.Threading;
using WebExpress.Config;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var app = new WebExpress.Program()
            {
                Name = Assembly.GetExecutingAssembly().GetName().Name
            };

            app.Execution(args);
        }
    }
}
