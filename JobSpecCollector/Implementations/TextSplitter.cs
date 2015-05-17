using System;
using System.Collections.Generic;
using System.Linq;
using JobSpecCollector.Interfaces;

namespace JobSpecCollector.Implementations
{
    public class TextSplitter : ITextSplitter
    {
        private static readonly char[] _separators = new char[] { ' ', '\r', '\n', ',', ':', ';', '(', ')', '/' };

        public IEnumerable<string> ToWords(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();
            var words = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
            var wl = new HashSet<string>();
            foreach (var word in words)
            {
                var w = FormatWord(word);
                if (!string.IsNullOrEmpty(w) && !wl.Contains(w)) wl.Add(w);
            }
            return wl;
        }

        private string FormatWord(string word)
        {
            var w = word.Trim();
            if (w.Last() == '.') w = w.Substring(0, w.Length - 1);
            return w.ToLower();
        }
    }
}
