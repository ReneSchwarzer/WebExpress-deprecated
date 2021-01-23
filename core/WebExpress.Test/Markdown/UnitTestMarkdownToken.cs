using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WebExpress.UI.Markdown;
using Xunit;

namespace WebExpress.Test.Markdown
{
    public class UnitTestMarkdownToken
    {
        [Fact]
        public void HasNext_1()
        {
            var token = new MarkdownToken()
            {
                 Position = 6,
                 Text = "ABCDEFG"
            };

            Assert.True
            (
                !token.HasNext, 
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }

        [Fact]
        public void HasNext_2()
        {
            var token = new MarkdownToken()
            {
                Position = 5,
                Text = "ABCDEFG"
            };

            Assert.True
            (
                token.HasNext,
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }

        [Fact]
        public void HasNext_3()
        {
            var token = new MarkdownToken()
            {
                Position = 7,
                Text = "ABCDEFG"
            };

            Assert.True
            (
                !token.HasNext,
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }

        [Fact]
        public void EoL_1()
        {
            var token = new MarkdownToken()
            {
                Position = 7,
                Text = "ABCDEFG"
            };

            Assert.True
            (
                token.EoL,
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }

        [Fact]
        public void EoL_2()
        {
            var token = new MarkdownToken()
            {
                Position = 6,
                Text = "ABCDEFG"
            };

            Assert.True
            (
                !token.EoL,
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }

        [Fact]
        public void EoL_3()
        {
            var token = new MarkdownToken()
            {
                Position = 5,
                Text = "ABCDEFG"
            };

            Assert.True
            (
                !token.EoL,
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }

        [Fact]
        public void Empty_1()
        {
            var token = new MarkdownToken()
            {
                Position = 5,
                Text = "ABCDEFG"
            };

            Assert.True
            (
                !token.Empty,
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }

        [Fact]
        public void Empty_2()
        {
            var token = new MarkdownToken()
            {
                Position = 0,
                Text = ""
            };

            Assert.True
            (
                token.Empty,
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }

        [Fact]
        public void Empty_3()
        {
            var token = new MarkdownToken()
            {
                Position = 0,
                Text = null
            };

            Assert.True
            (
                token.Empty,
                "Fehler beim Ermitteln des Tokenendes!"
            );
        }
    }
}
