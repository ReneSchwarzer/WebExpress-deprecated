using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.WebApp.WebIndex;
using WebExpress.WebComponent;

namespace WebExpress.Test.Index
{
    public class UnitTestIndexFixture
    {
        public IndexManager IndexManager { get; private set; }
        public IEnumerable<UnitTestIndexTestDocumentA> TestDataA { get; } = UnitTestIndexTestDocumentA.GenerateTestData();
        public IEnumerable<UnitTestIndexTestDocumentB> TestDataB { get; } = UnitTestIndexTestDocumentB.GenerateTestData();

        public UnitTestIndexFixture()
        {
            var context = new HttpServerContext(null, null, Path.Combine(Environment.CurrentDirectory, "test/packages"), null, null, null, null, CultureInfo.CurrentCulture, Log.Current, null);
            var ctor = typeof(IndexManager)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(c => !c.GetParameters().Any());

            ComponentManager.Initialization(context);
            IndexManager = (IndexManager)ctor.Invoke(Array.Empty<object>());
        }

        public virtual void Dispose()
        {

        }
    }
}
