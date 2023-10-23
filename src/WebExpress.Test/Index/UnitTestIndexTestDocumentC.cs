using System.Collections.Generic;

namespace WebExpress.Test.Index
{
    public class UnitTestIndexTestDocumentC : UnitTestIndexTestDocument
    {
        public string Text { get; set; }

        public static IEnumerable<UnitTestIndexTestDocumentC> GenerateTestData(int itemCount, int wordCount, int vocabulary, int wordLength)
        {
            // Add more test data here
            for (int i = 0; i < itemCount; i++)
            {
                yield return new UnitTestIndexTestDocumentC
                {
                    Id = i,
                    Text = GenerateWords(wordCount, vocabulary, wordLength),
                };
            }

            yield break;
        }

        public override string ToString()
        {
            return $"{Id}: {Text}";
        }
    }
}
