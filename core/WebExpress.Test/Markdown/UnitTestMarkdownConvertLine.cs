using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownConvertLine
    {
        [Fact]
        public void ConvertLine_1()
        {
            var markdown = "# Überschrift 1";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownLine;

            Assert.True
            (
                result?.Fragments[0].Type == MarkdownMorpheme.Headheadline1 &&
                result?.Fragments[1].Type == MarkdownMorpheme.Text &&
                result?.Fragments[0].Text == "#" &&
                result?.Fragments[1].Text == " Überschrift 1",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLine_2()
        {
            var markdown = "## Überschrift 2";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownLine;

            Assert.True
            (
                result?.Fragments[0].Type == MarkdownMorpheme.Headheadline2 &&
                result?.Fragments[1].Type == MarkdownMorpheme.Text &&
                result?.Fragments[0].Text == "##" &&
                result?.Fragments[1].Text == " Überschrift 2",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLine_3()
        {
            var markdown = "Hallo ***Welt***!";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownLine;

            Assert.True
            (
                result?.Fragments[0].Type == MarkdownMorpheme.Text &&
                result?.Fragments[1].Type == MarkdownMorpheme.Asterisk3 &&
                result?.Fragments[2].Type == MarkdownMorpheme.Text &&
                result?.Fragments[3].Type == MarkdownMorpheme.Asterisk3 &&
                result?.Fragments[4].Type == MarkdownMorpheme.Text &&
                result?.Fragments[0].Text == "Hallo " &&
                result?.Fragments[1].Text == "***" &&
                result?.Fragments[2].Text == "Welt" &&
                result?.Fragments[3].Text == "***" &&
                result?.Fragments[4].Text == "!",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLine_4()
        {
            var markdown = "Hallo ___Welt___!";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownLine;

            Assert.True
            (
                result?.Fragments[0].Type == MarkdownMorpheme.Text &&
                result?.Fragments[1].Type == MarkdownMorpheme.Underline3 &&
                result?.Fragments[2].Type == MarkdownMorpheme.Text &&
                result?.Fragments[3].Type == MarkdownMorpheme.Underline3 &&
                result?.Fragments[4].Type == MarkdownMorpheme.Text &&
                result?.Fragments[0].Text == "Hallo " &&
                result?.Fragments[1].Text == "___" &&
                result?.Fragments[2].Text == "Welt" &&
                result?.Fragments[3].Text == "___" &&
                result?.Fragments[4].Text == "!",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

    }
}
