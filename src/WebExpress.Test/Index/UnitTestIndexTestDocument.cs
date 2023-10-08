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
                    sb.Append(" ");
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
