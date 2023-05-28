using System;
using System.Globalization;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.WebComponent;
using Xunit;

namespace WebExpress.Test.Package
{
    public class UnitPackageManager
    {
        [Fact]
        public void Scan()
        {
            var context = new HttpServerContext(null, null, Path.Combine(Environment.CurrentDirectory, "test/packages"), null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);

            ComponentManager.Initialization(context);

            //PackageManager.Initialization(context);
            //PackageManager.Execute();

            //PackageManager.Scan();
        }
    }
}
