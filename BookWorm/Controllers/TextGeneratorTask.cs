using System.Collections.Generic;

namespace BookWorm
{
    public static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            for (int i = 0; i < wordsCount; i++)
            {
                string[] phraseWords = phraseBeginning.Split();
                string lastKeyword = phraseWords[phraseWords.Length - 1].ToString();
                if (phraseWords.Length >= 2)
                {
                    string firstKeyword = phraseWords[phraseWords.Length - 2].ToString();
                    if (nextWords.ContainsKey(firstKeyword + " " + lastKeyword))
                        phraseBeginning += " " + nextWords[firstKeyword + " " + lastKeyword];
                    else if (nextWords.ContainsKey(lastKeyword))
                        phraseBeginning += " " + nextWords[lastKeyword];
                }
                else
                {
                    if (nextWords.ContainsKey(lastKeyword))
                        phraseBeginning += " " + nextWords[lastKeyword];
                }
            }
            return phraseBeginning;
        }
    }
}