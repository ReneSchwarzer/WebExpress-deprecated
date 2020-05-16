using System.Reflection;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownGetFragmentEquals
    {
        [Fact]
        public void GetFragment_Equals_1()
        {
            var markdown = "==";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "==",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_2()
        {
            var markdown = "===";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "===",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_3()
        {
            var markdown = "====";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "====",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_4()
        {
            var markdown = "=== ";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "===",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_5()
        {
            var markdown = "=";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Text &&
                result?.Text == "=",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_6()
        {
            var markdown = "===*";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "===",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_7()
        {
            var markdown = "===+";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "===",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_8()
        {
            var markdown = "===-";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "===",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_9()
        {
            var markdown = "===_";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "===",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Equals_10()
        {
            var markdown = "===#";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Headheadline1Marker &&
                result?.Text == "===",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }
    }
}
