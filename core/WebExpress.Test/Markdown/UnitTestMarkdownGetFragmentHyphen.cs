using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownGetFragmentHyphen
    {
        [Fact]
        public void GetFragment_HyphenkOne_1()
        {
            var markdown = "-";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen1 &&
                result?.Text == "-", 
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenOne_2()
        {
            var markdown = "- test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen1 &&
                result?.Text == "-",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenOne_3()
        {
            var markdown = "test - test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type != MarkdownFragmentState.Hyphen1,
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenOne_4()
        {
            var markdown = "- ";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen1 &&
                result?.Text == "-",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenTwo_1()
        {
            var markdown = "--";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen2 &&
                result?.Text == "--",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenTwo_2()
        {
            var markdown = "-- test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen2 &&
                result?.Text == "--",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenTwo_3()
        {
            var markdown = "test -- test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type != MarkdownFragmentState.Hyphen2,
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenThree_1()
        {
            var markdown = "---";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen3 &&
                result?.Text == "---",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenThree_2()
        {
            var markdown = "--- test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen3 &&
                result?.Text == "---",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenThree_3()
        {
            var markdown = "test --- test";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type != MarkdownFragmentState.Hyphen3,
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenLine_1()
        {
            var markdown = "----";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.HorizontalLine &&
                result?.Text == "----",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenLine_2()
        {
            var markdown = "---------";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.HorizontalLine &&
                result?.Text == "---------",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenLine_3()
        {
            var markdown = "- - -";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.HorizontalLine &&
                result?.Text == "- - -",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenLine_4()
        {
            var markdown = "-  -  -";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen1 &&
                result?.Text == "-",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_HyphenLine_5()
        {
            var markdown = "- -  - - ";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Hyphen1 &&
                result?.Text == "-",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }
    }
}
