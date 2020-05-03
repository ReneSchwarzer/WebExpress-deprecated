using System.Reflection;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownGetFragmentImageReference
    {
        [Fact]
        public void GetFragment_ImageReference_1()
        {
            var markdown = "![Alt-Text][Bild 1]";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownMorpheme.ImageReference &&
                result?.Text == "![Alt-Text][Bild 1]",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void GetFragment_ImageReference_2()
        {
            var markdown = "![Alt-Text](Assets/img/Logo.png \"Optionaler Teil\")";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("GetFragment", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { new MarkdownToken() { Text = markdown } };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownFragment;

            Assert.True
            (
                result?.Type == MarkdownMorpheme.ImageReference &&
                result?.Text == "![Alt-Text](Assets/img/Logo.png \"Optionaler Teil\")",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }
    }
}
