using System.Collections.Generic;
using System.IO;

namespace BookWorm
{
    public static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var ngramsList = GetAllNGrams(text);
            var ngFreq = GetFrequencyDictionary(ngramsList);
            var result = new Dictionary<string, string>();
            foreach (KeyValuePair<string, Dictionary<string, int>> pair in ngFreq)
            {
                string key = pair.Key;
                string value = "";
                Dictionary<string, int> keyValue = pair.Value;
                int entries = 0;
                foreach (var s in keyValue)
                {
                    if (s.Value > entries)
                    {
                        value = s.Key;
                        entries = s.Value;
                    }
                    else if (s.Value == entries && string.CompareOrdinal(s.Key, value) < 1)
                    {
                        value = s.Key;
                    }
                }
                result.Add(key,value);
            }
            return result;
        }

        public static List<string> GetAllNGrams(List<List<string>> text)
        {
            var tempList = new List<string>();
            foreach (var sentence in text)
                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    tempList.Add(sentence[i] + ' ' + sentence[i + 1]);
                    if (sentence.Count - i > 2)
                        tempList.Add(string.Format("{0} {1} {2}", sentence[i], sentence[i + 1], sentence[i + 2]));
                }
            return tempList;
        }

        public static Dictionary<string, Dictionary<string, int>> GetFrequencyDictionary(List<string> tempList)
        {
            var ngFreq = new Dictionary<string, Dictionary<string, int>>();
            foreach (var ngram in tempList)
            {
                string key = ngram.Substring(0, ngram.LastIndexOf(' '));
                string value = ngram.Substring(ngram.LastIndexOf(' ') + 1);
                var dict = new Dictionary<string, int>();
                if (!ngFreq.ContainsKey(key))
                {
                    dict.Add(value, 1);
                    ngFreq.Add(key, dict);
                }
                else
                {
                    if (!ngFreq[key].ContainsKey(value))
                        ngFreq[key].Add(value, 1);
                    else
                        ngFreq[key][value]++;
                }
            }
            return ngFreq;
        }
    }
}