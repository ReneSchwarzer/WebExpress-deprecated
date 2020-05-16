using System.Reflection;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownGetFragmentImage
    {
        [Fact]
        public void GetFragment_Image_1()
        {
            var markdown = "[Bild 1]: Assets/img/Logo.png";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Image &&
                result?.Text == "[Bild 1]: Assets/img/Logo.png",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Image_2()
        {
            var markdown = "[Bild 1]: Assets/img/Logo.png \"Optionaler Teil\"";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Image &&
                result?.Text == "[Bild 1]: Assets/img/Logo.png \"Optionaler Teil\"",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }


        [Fact]
        public void GetFragment_Image_3()
        {
            var markdown = "[Bild 1]: Assets/img/Logo.png \"Optionaler Teil";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Text &&
                result?.Text == "[Bild 1]: Assets/img/Logo.png \"Optionaler Teil",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_Image_4()
        {
            var markdown = "[Hallo Welt]";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownFragmentState.Text &&
                result?.Text == "[Hallo Welt]",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }
    }
}
