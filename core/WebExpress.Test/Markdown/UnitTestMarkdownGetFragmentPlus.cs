using System.Reflection;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownGetFragmentPlus
    {
        [Fact]
        public void GetFragment_Plus_1()
        {
            var markdown = "+";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Plus_2()
        {
            var markdown = "++";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Plus_3()
        {
            var markdown = "+ ";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Plus_4()
        {
            var markdown = "+*";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Plus_5()
        {
            var markdown = "+_";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Plus_6()
        {
            var markdown = "+#";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Plus_7()
        {
            var markdown = "+A";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Plus_8()
        {
            var markdown = "+-";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Plus_9()
        {
            var markdown = "+=";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }
        
        [Fact]
        public void GetFragment_Plus_10()
        {
            var markdown = "+>";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Plus &&
                result?.Text == "+",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }
    }
}
