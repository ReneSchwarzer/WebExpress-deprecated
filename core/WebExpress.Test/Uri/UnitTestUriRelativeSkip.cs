using WebExpress.Uri;
using Xunit;

namespace WebExpress.Test.Uri
{
    public class UnitTestUriRelativeSkip
    {
        private UriRelative Uri = new UriRelative("/a/b/c");

        [Fact]
        public void Skip_0()
        {
            var skip = Uri.Skip(0);

            Assert.True
            (
                skip.ToString().Equals("/a/b/c") && skip.Path.Count == 4,
                "Fehler in der Funktion Skip der UriRelative"
            );
        }

        [Fact]
        public void Skip_1()
        {
            var skip = Uri.Skip(1);

            Assert.True
            (
                skip.ToString().Equals("/a/b/c") && skip.Path.Count == 3,
                "Fehler in der Funktion Skip der UriRelative"
            );
        }

        [Fact]
        public void Skip_2()
        {
            var skip = Uri.Skip(2);

            Assert.True
            (
                skip.ToString().Equals("/b/c") && skip.Path.Count == 2,
                "Fehler in der Funktion Skip der UriRelative"
            );
        }

        [Fact]
        public void Skip_3()
        {
            var skip = Uri.Skip(3);

            Assert.True
            (
                skip.ToString().Equals("/c") && skip.Path.Count == 1,
                "Fehler in der Funktion Skip der UriRelative"
            );
        }

        [Fact]
        public void Skip_4()
        {
            var skip = Uri.Skip(4);

            Assert.True
            (
                skip == null,
                "Fehler in der Funktion Skip der UriRelative"
            );
        }

        [Fact]
        public void Skip_5()
        {
            var skip = new UriRelative().Skip(1);

            Assert.True
            (
                skip == null,
                "Fehler in der Funktion Skip der UriRelative"
            );
        }
    }
}
