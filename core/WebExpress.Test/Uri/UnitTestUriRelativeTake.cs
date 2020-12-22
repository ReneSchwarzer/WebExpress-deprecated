using WebExpress.Uri;
using Xunit;

namespace WebExpress.Test.Uri
{
    public class UnitTestUriRelativeTake
    {
        private UriRelative Uri = new UriRelative("/a/b/c");

        [Fact]
        public void Take_0()
        {
            var take = Uri.Take(0);

            Assert.True
            (
                take.ToString().Equals("/") && take.Path.Count == 0,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_1()
        {
            var take = Uri.Take(1);

            Assert.True
            (
                take.ToString().Equals("/") && take.Path.Count == 1,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_2()
        {
            var take = Uri.Take(2);

            Assert.True
            (
                take.ToString().Equals("/a") && take.Path.Count == 2,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_3()
        {
            var take = Uri.Take(3);

            Assert.True
            (
                take.ToString().Equals("/a/b") && take.Path.Count == 3,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_4()
        {
            var take = Uri.Take(4);

            Assert.True
            (
                take.ToString().Equals("/a/b/c") && take.Path.Count == 4,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_5()
        {
            var take = Uri.Take(5);

            Assert.True
            (
                take.ToString().Equals("/a/b/c") && take.Path.Count == 4,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_6()
        {
            var take = Uri.Take(-1);

            Assert.True
            (
                take.ToString().Equals("/a/b") && take.Path.Count == 3,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_7()
        {
            var take = Uri.Take(-2);

            Assert.True
            (
                take.ToString().Equals("/a") && take.Path.Count == 2,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_8()
        {
            var take = Uri.Take(-3);

            Assert.True
            (
                take.ToString().Equals("/") && take.Path.Count == 1,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_9()
        {
            var take = Uri.Take(-4);

            Assert.True
            (
                take == null,
                "Fehler in der Funktion Take der UriRelative"
            );
        }

        [Fact]
        public void Take_10()
        {
            var take = Uri.Take(-5);

            Assert.True
            (
                take == null,
                "Fehler in der Funktion Take der UriRelative"
            );
        }
    }
}
