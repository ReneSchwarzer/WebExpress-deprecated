using System;
using System.Text;
using WebExpress.WebApp.WebIndex;

namespace WebExpress.Test.Index
{
    public abstract class UnitTestIndexTestDocument : IIndexItem
    {
        private static readonly string[] Words = new string[]
        {
            "lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit",
            "sed", "do", "eiusmod", "tempor", "incididunt", "ut", "labore", "et",
            "dolore", "magna", "aliqua", "phasellus", "fermentum", "malesuada", "phasellus",
            "netus", "dictum", "aenean", "placerat", "egestas", "amet", "ornare", "taciti",
            "semper", "tristique", "morbi", "sem", "leo", "tincidunt", "aliquet",
            "eu", "lectus", "scelerisque", "quis", "sagittis", "vivamus", "mollis",
            "nisi", "enim", "laoreet"
        };

        private static readonly Random Rand = new Random();

        public int Id { get; set; }

        protected static string GenerateLoremIpsum(int numWords)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < numWords; i++)
            {
                sb.Append(Words[Rand.Next(Words.Length)]);
                if (i < numWords - 1)
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        protected static string GenerateWord(int seed, int wordLength)
        {
            var sb = new StringBuilder();
            var characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            while (sb.Length < 10)
            {
                var index = seed % characters.Length;
                sb.Append(characters[index]);
                seed /= characters.Length;
                if (seed == 0)
                {
                    break;
                }
            }

            // If the resulting word has less than count characters,
            // we fill the rest with the first character of the character set
            while (sb.Length < wordLength)
            {
                sb.Append(characters[0]);
            }

            return sb.ToString();
        }

        protected static string GenerateWord(int wordLength)
        {
            var sb = new StringBuilder();

            for (int j = 0; j < wordLength; j++)
            {
                sb.Append((char)('a' + Rand.Next() % ('z' - 'a')));
            }

            return sb.ToString();
        }

        protected static string GenerateWords(int numWords, int vocabulary, int wordLength)
        {
            var sb = new StringBuilder();
            var rand = new Random();

            for (int i = 0; i < numWords; i++)
            {
                sb.Append(GenerateWord(rand.Next() % vocabulary, wordLength));

                if (i < numWords - 1)
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        protected static string GenerateWords(int numWords, int wordLength)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < numWords; i++)
            {
                var word = GenerateWord(wordLength);
                sb.Append(word);

                if (i < numWords - 1)
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        protected static string GenerateSreet(int index)
        {
            int rand = Rand.Next() % 5;

            return rand switch
            {
                0 => $"{index % 99} Elm St.",
                1 => $"{index % 99} Maple Ave.",
                2 => $"{index % 99} Oak Ave.",
                3 => $"{index % 99} Pine St.",
                _ => $"{index % 99} Main St."
            };
        }
    }
}
