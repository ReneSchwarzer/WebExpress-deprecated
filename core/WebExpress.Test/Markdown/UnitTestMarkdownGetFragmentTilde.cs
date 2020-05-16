using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownGetFragmentTilde
    {
        [Fact]
        public void GetFragment_TildeOne_1()
        {
            var markdown = "~";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde1 &&
                result?.Text == "~", 
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeOne_2()
        {
            var markdown = "~ test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde1 &&
                result?.Text == "~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeOne_3()
        {
            var markdown = "test ~ test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type != MarkdownFragmentState.Tilde1,
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeOne_4()
        {
            var markdown = "~ ";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde1 &&
                result?.Text == "~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeTwo_1()
        {
            var markdown = "~~";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde2 &&
                result?.Text == "~~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeTwo_2()
        {
            var markdown = "~~ test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde2 &&
                result?.Text == "~~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeTwo_3()
        {
            var markdown = "test ~~ test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type != MarkdownFragmentState.Tilde2,
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeThree_1()
        {
            var markdown = "~~~";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde3 &&
                result?.Text == "~~~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeThree_2()
        {
            var markdown = "~~~ test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde3 &&
                result?.Text == "~~~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeThree_3()
        {
            var markdown = "test ~~~ test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type != MarkdownFragmentState.Tilde3,
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeLine_1()
        {
            var markdown = "~~~~";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.HorizontalLine &&
                result?.Text == "~~~~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeLine_2()
        {
            var markdown = "~~~~~~~~~~~~";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.HorizontalLine &&
                result?.Text == "~~~~~~~~~~~~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeLine_3()
        {
            var markdown = "~ ~ ~";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.HorizontalLine &&
                result?.Text == "~ ~ ~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeLine_4()
        {
            var markdown = "~  ~  ~";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde1 &&
                result?.Text == "~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_TildeLine_5()
        {
            var markdown = "~ ~  ~ ~ ";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Tilde1 &&
                result?.Text == "~",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }
    }
}
