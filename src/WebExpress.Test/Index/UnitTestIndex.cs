using System.Linq;
using WebExpress.WebApp.WebIndex;
using Xunit;

namespace WebExpress.Test.Index
{
    public class UnitTestIndex : IClassFixture<UnitTestIndexFixture>
    {
        protected UnitTestIndexFixture Fixture { get; set; }

        public UnitTestIndex(UnitTestIndexFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void Register()
        {
            Fixture.IndexManager.Register<UnitTestIndexTestDocumentA>();
            Fixture.IndexManager.Register<UnitTestIndexTestDocumentB>();

            Assert.True(Fixture.IndexManager.Documents.Count == 2);
        }

        [Fact]
        public void ReIndexTestDataA()
        {
            if (Fixture.IndexManager.Documents.Count == 0)
            {
                Register();
            }

            Fixture.IndexManager.ReIndex(Fixture.TestDataA);
        }

        [Fact]
        public void ReIndexTestDataB()
        {
            if (Fixture.IndexManager.Documents.Count == 0)
            {
                Register();
            }

            Fixture.IndexManager.ReIndex(Fixture.TestDataB);
        }

        [Fact]
        public void Tokenize()
        {
            var input = "abc def, ghi jkl mno-p.";
            var tokens = IndexTermTokenizer.Tokenize(input);

            Assert.True(tokens.Count() == 5);
            Assert.True(tokens.First().Position == 0);
            Assert.True(tokens.First().Value == "abc");
            Assert.True(tokens.Skip(1).First().Position == 1);
            Assert.True(tokens.Skip(1).First().Value == "def,");
            Assert.True(tokens.Skip(2).First().Position == 2);
            Assert.True(tokens.Skip(2).First().Value == "ghi");
            Assert.True(tokens.Skip(3).First().Position == 3);
            Assert.True(tokens.Skip(3).First().Value == "jkl");
            Assert.True(tokens.Skip(4).First().Position == 4);
            Assert.True(tokens.Skip(4).First().Value == "mno-p.");
        }

        [Fact]
        public void Normalize()
        {
            var input = "abc def, ghi jkl mno-p. äöüéíú";
            var tokens = IndexTermTokenizer.Tokenize(input);
            var terms = IndexTermNormalizer.Normalize(tokens);

            Assert.True(terms.Count() == 6);
            Assert.True(terms.First().Position == 0);
            Assert.True(terms.First().Value == "abc");
            Assert.True(terms.Skip(1).First().Position == 1);
            Assert.True(terms.Skip(1).First().Value == "def,");
            Assert.True(terms.Skip(2).First().Position == 2);
            Assert.True(terms.Skip(2).First().Value == "ghi");
            Assert.True(terms.Skip(3).First().Position == 3);
            Assert.True(terms.Skip(3).First().Value == "jkl");
            Assert.True(terms.Skip(4).First().Position == 4);
            Assert.True(terms.Skip(4).First().Value == "mno-p.");
            Assert.True(terms.Skip(5).First().Position == 5);
            Assert.True(terms.Skip(5).First().Value == "aeoeueeiu");
        }
    }
}
