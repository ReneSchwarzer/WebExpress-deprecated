using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownTransform
    {
        [Fact]
        public void Transform_1()
        {
            var markdown = "# Überschrift 1\nHallo Welt!";

            var md = new UI.Markdown.Markdown();
            var result = UI.Markdown.Markdown.Transform(markdown);

            Assert.True
            (
                false,
                "Fehler beim Transformieren von Markdontext in Html!"
            );
        }

        [Fact]
        public void Transform_2()
        {
            var markdown = File.ReadAllText("test\\WebExpress.md");

            var md = new UI.Markdown.Markdown();
            var result = UI.Markdown.Markdown.Transform(markdown);

            Assert.True
            (
                false,
                "Fehler beim Transformieren von Markdontext in Html!"
            );
        }

    }
}
