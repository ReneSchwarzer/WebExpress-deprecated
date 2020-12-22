using System.IO;
using WebExpress.Message;
using Xunit;

namespace WebExpress.Test.Message
{
    public class UnitTestGetRequest
    {
        [Fact]
        public void Get_General()
        {
            using var reader = new FileStream(Path.Combine("test", "general.get"), FileMode.Open);
            var request = Request.Create(reader, "127.0.0.1");

            Assert.True
            (
                request.URL == "/abc/xyz/A7BCCCA9-4C7E-4117-9EE2-ECC3381B605A",
                "Fehler in der Funktion Get_General"
            );
        }

        [Fact]
        public void Get_Less()
        {
            using var reader = new FileStream(Path.Combine("test", "less.get"), FileMode.Open);
            var request = Request.Create(reader, "127.0.0.1");

            Assert.True
            (
                request.URL == "/abc/xyz/A7BCCCA9-4C7E-4117-9EE2-ECC3381B605A",
                "Fehler in der Funktion Get_Less"
            );
        }

        [Fact]
        public void Get_Massive()
        {
            using var reader = new FileStream(Path.Combine("test", "massive.get"), FileMode.Open);
            var request = Request.Create(reader, "127.0.0.1");

            Assert.True
            (
                request.URL == "/abc/xyz/A7BCCCA9-4C7E-4117-9EE2-ECC3381B605A",
                "Fehler in der Funktion Get_Massive"
            );
        }

        [Fact]
        public void Get_Param()
        {
            using var reader = new FileStream(Path.Combine("test", "param.get"), FileMode.Open);
            var request = Request.Create(reader, "127.0.0.1");
            var param = request?.GetParamValue("a");

            Assert.True
            (
                request.URL == "/abc/xyz/A7BCCCA9-4C7E-4117-9EE2-ECC3381B605A" &&
                param != null && param == "1",
                "Fehler in der Funktion Get_Param"
            );
        }
    }
}
