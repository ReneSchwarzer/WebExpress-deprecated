using System.IO;
using WebExpress.Message;
using Xunit;

namespace WebExpress.Test.Message
{
    public class UnitTestPostRequest
    {
        [Fact]
        public void Post_TextPlain()
        {
            using var reader = new BinaryReader(new FileStream(Path.Combine("test", "contentTypeTextPlain.post"), FileMode.Open));
            var request = Request.Create(reader, "127.0.0.1");
            var param = request?.GetParamValue("submit_manufactor");

            Assert.True
            (
               param != null && param == "1",
               "Fehler in der Funktion Post_TextPlain"
            );
        }

        [Fact]
        public void Post_TextPlain_Umlaut()
        {
            using var reader = new BinaryReader(new FileStream(Path.Combine("test", "contentTypeTextPlain_Umlaut.post"), FileMode.Open));
            var request = Request.Create(reader, "127.0.0.1");
            var a = request?.GetParamValue("a");
            var b = request?.GetParamValue("b");
            var s = request?.GetParamValue("submit_");

            Assert.True
            (
               a != null && a == "ä" &&
               b != null && b == "ö ü" &&
               s != null && s == "1",
               "Fehler in der Funktion Post_TextPlain_Umlaut"
            );
        }

        [Fact]
        public void Post_Urlencoded()
        {
            using var reader = new BinaryReader(new FileStream(Path.Combine("test", "contentTypeXwwwFormUrlencoded.post"), FileMode.Open));
            var request = Request.Create(reader, "127.0.0.1");
            var param = request?.GetParamValue("submit_manufactor");

            Assert.True
            (
               param != null && param == "1",
               "Fehler in der Funktion Post_Urlencoded"
            );
        }

        [Fact]
        public void Post_Urlencoded_Umlaut()
        {
            using var reader = new BinaryReader(new FileStream(Path.Combine("test", "contentTypeXwwwFormUrlencoded_Umlaut.post"), FileMode.Open));
            var request = Request.Create(reader, "127.0.0.1");
            var a = request?.GetParamValue("a");
            var b = request?.GetParamValue("b");
            var s = request?.GetParamValue("submit_");

            Assert.True
            (
               a != null && a == "ä" &&
               b != null && b == "ö ü" &&
               s != null && s == "1",
               "Fehler in der Funktion Post_Urlencoded_Umlaut"
            );
        }

        [Fact]
        public void Post_Multipart1()
        {
            using var reader = new BinaryReader(new FileStream(Path.Combine("test", "contentTypeMultipartFormData1.post"), FileMode.Open));
            var request = Request.Create(reader, "127.0.0.1");
            var param = request?.GetParamValue("submit_manufactor");

            Assert.True
            (
               param != null && param == "1",
               "Fehler in der Funktion Post_Multipart1"
            );
        }

        [Fact]
        public void Post_Multipart2()
        {
            using var reader = new BinaryReader(new FileStream(Path.Combine("test", "contentTypeMultipartFormData2.post"), FileMode.Open));
            var request = Request.Create(reader, "127.0.0.1");
            var param = request?.GetParamValue("submit_manufactor");

            var file = request?.GetParam("image") as ParameterFile;

            var temp = Path.Combine(Path.GetTempPath(), file.Value);
            File.WriteAllBytes(temp, file.Data);

            Assert.True
            (
                file.Data.Length == 47788 &&
                param != null && param == "1",
                "Fehler in der Funktion Post_Multipart2"
            );
        }

        [Fact]
        public void Post_Multipart3()
        {
            using var reader = new BinaryReader(new FileStream(Path.Combine("test", "contentTypeMultipartFormData3.post"), FileMode.Open));
            var request = Request.Create(reader, "127.0.0.1");
            var param = request?.GetParamValue("submit_del");

            Assert.True
            (
                param != null && param == "1",
                "Fehler in der Funktion Post_Multipart3"
            );
        }

        [Fact]
        public void Post_Multipart_Umlaut()
        {
            using var reader = new BinaryReader(new FileStream(Path.Combine("test", "contentTypeMultipartFormData_Umlaut.post"), FileMode.Open));
            var request = Request.Create(reader, "127.0.0.1");
            var a = request?.GetParamValue("a");
            var b = request?.GetParamValue("b");
            var s = request?.GetParamValue("submit_");

            Assert.True
            (
                a != null && a == "ä" &&
                b != null && b == "ö ü" &&
                s != null && s == "1",
                "Fehler in der Funktion Post_Multipart_Umlaut"
            );
        }
    }
}
