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
        public void ConvertLineH1_1()
        {
            var markdown = "# Überschrift 1";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline1 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 1",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH1_2()
        {
            var markdown = "# Überschrift 1 # mit #";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline1 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 1 # mit #",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH1_3()
        {
            var markdown = "# Überschrift 1 *mit* *";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline1 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 1" &&
                result?.Morphemes[1].Type == MarkdownMorphemeState.Italic &&
                result?.Morphemes[1].Text.ToString() == "mit" &&
                result?.Morphemes[2].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[2].Text.ToString() == "*",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH1_4()
        {
            var markdown = "# Überschrift 1 **mit** **";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline1 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 1" &&
                result?.Morphemes[1].Type == MarkdownMorphemeState.Bold &&
                result?.Morphemes[1].Text.ToString() == "mit" &&
                result?.Morphemes[2].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[2].Text.ToString() == "**",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH1_5()
        {
            var markdown = "# Überschrift 1 ***mit*** ***";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline1 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 1" &&
                result?.Morphemes[1].Type == MarkdownMorphemeState.BoldItalic &&
                result?.Morphemes[1].Text.ToString() == "mit" &&
                result?.Morphemes[2].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[2].Text.ToString() == "***",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH1_6()
        {
            var markdown = "# Überschrift 1 ****mit*** ****";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline1 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 1 ***mit*** ***",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }


        [Fact]
        public void ConvertLineH1_7()
        {
            var markdown = "# Überschrift 1 _mit_ _";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline1 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 1" &&
                result?.Morphemes[1].Type == MarkdownMorphemeState.Italic &&
                result?.Morphemes[1].Text.ToString() == "mit" && 
                result?.Morphemes[2].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[2].Text.ToString() == "*",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        

        [Fact]
        public void ConvertLineH2_1()
        {
            var markdown = "## Überschrift 2";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline2 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 2",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH2_2()
        {
            var markdown = "## Überschrift 2 # mit #";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline2 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 2 # mit #",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH3_1()
        {
            var markdown = "### Überschrift 3";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline3 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 3",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH3_2()
        {
            var markdown = "### Überschrift 3 # mit #";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline3 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 3 # mit #",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH4_1()
        {
            var markdown = "#### Überschrift 4";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline4 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 4",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH4_2()
        {
            var markdown = "#### Überschrift 4 # mit #";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline4 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 4 # mit #",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH5_1()
        {
            var markdown = "##### Überschrift 5";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline5 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 5",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH5_2()
        {
            var markdown = "##### Überschrift 5 # mit #";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline5 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 5 # mit #",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH6_1()
        {
            var markdown = "###### Überschrift 6";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline6 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 6",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLineH6_2()
        {
            var markdown = "###### Überschrift 6 # mit #";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Headheadline6 &&
                result?.Morphemes[0].Text.ToString() == "Überschrift 6 # mit #",
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLine_7()
        {
            var markdown = "Hallo ***Welt***!";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[1].Type == MarkdownMorphemeState.Asterisk3 &&
                result?.Morphemes[2].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[3].Type == MarkdownMorphemeState.Asterisk3 /*&&
                result?.Morphemes[4].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[0].Text == "Hallo " &&
                result?.Morphemes[1].Text == "***" &&
                result?.Morphemes[2].Text == "Welt" &&
                result?.Morphemes[3].Text == "***" &&
                result?.Morphemes[4].Text == "!"*/,
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

        [Fact]
        public void ConvertLine_8()
        {
            var markdown = "Hallo ___Welt___!";

            var obj = new UI.Markdown.Markdown();
            var methodInfo = typeof(UI.Markdown.Markdown).GetMethod("ConvertLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var parameters = new[] { markdown };
            var result = methodInfo.Invoke(obj, parameters) as MarkdownMorphemes;

            Assert.True
            (
                result?.Morphemes[0].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[1].Type == MarkdownMorphemeState.Underline3 &&
                result?.Morphemes[2].Type == MarkdownMorphemeState.Text &&
                result?.Morphemes[3].Type == MarkdownMorphemeState.Underline3 &&
                result?.Morphemes[4].Type == MarkdownMorphemeState.Text /*&&
                result?.Morphemes[0].Text == "Hallo " &&
                result?.Morphemes[1].Text == "___" &&
                result?.Morphemes[2].Text == "Welt" &&
                result?.Morphemes[3].Text == "___" &&
                result?.Morphemes[4].Text == "!"*/,
                "Fehler beim Ermitteln eines Fragments!"
            );
        }

    }
}
