using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WebExpress.WebApp.WebIndex;
using Xunit;
using Xunit.Abstractions;

namespace WebExpress.Test.Index
{
    public class UnitTestIndex : IClassFixture<UnitTestIndexFixture>
    {
        public ITestOutputHelper Output { get; private set; }
        protected UnitTestIndexFixture Fixture { get; set; }

        public UnitTestIndex(UnitTestIndexFixture fixture, ITestOutputHelper output)
        {
            Fixture = fixture;
            Output = output;
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
            Fixture.GetUsedMemory();

            var testData = UnitTestIndexTestDocumentA.GenerateTestData();

            Fixture.IndexManager.Register<UnitTestIndexTestDocumentA>();
            Fixture.IndexManager.ReIndex(testData);

            Fixture.GetUsedMemory();
        }

        [Fact]
        public void ReIndexTestDataB()
        {
            Fixture.GetUsedMemory();

            var testData = UnitTestIndexTestDocumentB.GenerateTestData();

            Fixture.IndexManager.Register<UnitTestIndexTestDocumentB>();
            Fixture.IndexManager.ReIndex(testData);

            Fixture.GetUsedMemory();
        }

        [Fact]
        public void ReIndexTestDataC()
        {
            var stopWatch = new Stopwatch();
            var itemCount = 1000;
            var wordCount = 1000;
            var vocabulary = 40000;
            var wordLength = 10;

            var testData = UnitTestIndexTestDocumentC.GenerateTestData(itemCount, wordCount, vocabulary, wordLength).ToList();

            Output.WriteLine($"ReIndex {itemCount.ToString("#,##0")} items, {vocabulary.ToString("#,##0")} vocabulary and {wordLength.ToString("#,##0")} word length");

            Fixture.IndexManager.Register<UnitTestIndexTestDocumentC>();

            // preparing for a measurement
            var begin = Fixture.GetUsedMemory();
            stopWatch.Start();

            Fixture.IndexManager.ReIndex(testData);

            // stop measurement
            stopWatch.Stop();
            var end = Fixture.GetUsedMemory();
            var elapsedReindex = stopWatch.Elapsed;
            var usedReindex = (end - begin) / 1024 / 1024; // in MB

            // preparing for a measurement
            begin = Fixture.GetUsedMemory();
            stopWatch.Start();
            Fixture.IndexManager.ExecuteWql<UnitTestIndexTestDocumentC>("Text ~ 'abcdaaaaaa'");

            // stop measurement
            stopWatch.Stop();
            end = Fixture.GetUsedMemory();
            var elapsedCollect = stopWatch.Elapsed;
            var usedCollect = (end - begin) / 1024 / 1024; // in MB

            Output.WriteLine($"ReIndex take: {elapsedReindex}");
            Output.WriteLine("ReIndex ram used: " + (Convert.ToDouble(usedReindex)).ToString("0.##") + " MB");

            Output.WriteLine($"Collect take: {elapsedCollect}");
            Output.WriteLine("Collect ram used: " + (Convert.ToDouble(usedCollect)).ToString("0.##") + " MB");
        }

        [Fact]
        public void ReIndexTestDataSeriesC()
        {
            var stopWatch = new Stopwatch();
            var file = File.CreateText("C:\\Users\\rene_\\OneDrive\\myindex-test.csv");

            var itemCount = Enumerable.Range(1, 1).Select(x => x * 1000);
            var wordCount = new int[] { 1000 };
            var vocabulary = new int[] { 40000 };
            var wordLength = new int[] { 10 };

            //var wordCount = new int[] { 100, 1000 };
            //var vocabulary = new int[] { 10000, 20000, 30000, 40000, 50000, 60000, 70000 };
            //var wordLength = new int[] { 10, 20, 30, 40, 50 };

            var heading = "item count;wordCount;vocabulary;wordLength;elapsed reindex;";
            Output.WriteLine(heading);
            file.WriteLine(heading);

            foreach (var w in wordCount)
            {
                foreach (var i in itemCount)
                {
                    foreach (var v in vocabulary)
                    {
                        foreach (var l in wordLength)
                        {
                            var testData = UnitTestIndexTestDocumentC.GenerateTestData(i, w, v, l);

                            Fixture.IndexManager.Register<UnitTestIndexTestDocumentC>((uint)i, IndexType.Storage);

                            // preparing for a measurement
                            stopWatch.Start();

                            //Fixture.IndexManager.Add(testData.FirstOrDefault());
                            Fixture.IndexManager.ReIndex(testData);

                            // stop measurement
                            var elapsedReindex = stopWatch.Elapsed;
                            stopWatch.Reset();

                            var output = $"{i};{w};{v};{l};{elapsedReindex.ToString(@"hh\:mm\:ss")};";

                            Output.WriteLine(output);
                            file.WriteLine(output);

                            Fixture.IndexManager.Remove<UnitTestIndexTestDocumentC>();

                            file.Flush();
                        }
                    }
                }
            }
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
            Assert.True(terms.Skip(5).First().Value == "aoueiu");
        }
    }
}
