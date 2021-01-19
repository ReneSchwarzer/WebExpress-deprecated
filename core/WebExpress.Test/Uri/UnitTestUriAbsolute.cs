using WebExpress.Uri;
using Xunit;

namespace WebExpress.Test.Uri
{
    public class UnitTestUriAbsolute
    {
        [Fact]
        public void New_0()
        {
            var uri = new UriAbsolute("http://user@example.com:8080/abc#a?b=1&c=2");

            Assert.True
            (
                uri.Scheme == UriScheme.Http,
                "Fehler in der Funktion New_0 der UriRelative"
            );
        }

       
    }
}
