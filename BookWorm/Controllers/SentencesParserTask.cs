using System.Collections.Generic;

namespace BookWorm
{
    public static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            text = text.Trim().ToLower();
            List<string> sentences = new List<string>();
            List<List<string>> words = new List<List<string>>();
            char[] delimeters = new char[] { '.', '!', '?', ';', ':', '(', ')' };
            sentences.AddRange(text.Trim().ToLower().
                                    Split(delimeters,
                                    System.StringSplitOptions.RemoveEmptyEntries));
            for (int i = 0; i < sentences.Count; i++)
            {
                List<string> temp = new List<string>();
                string word = "";
                foreach (var c in sentences[i])
                {
                    if (char.IsLetter(c) || c == '\'')
                        word += c;
                    else word += ' ';
                }
                temp.AddRange(word.Split(new char[] { ' ' },
                                         System.StringSplitOptions.RemoveEmptyEntries));
                if (temp.Count != 0) words.Add(temp);
            }
            return words;
        }
    }
}