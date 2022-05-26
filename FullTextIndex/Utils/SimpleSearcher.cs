using System;

namespace FullTextIndex;

public class SimpleSearcher
{
    public IEnumerable<string> Search(string word, string item)
    {
        var pos = 0;
        while (true)
        {
            pos = item.IndexOf(word, pos, StringComparison.InvariantCulture);

            if (pos >= 0)
                yield return FormatMatchText(item, pos);
            else 
                break;
                
            pos++;
        }
    }
    
    public IEnumerable<string> Search(string word, IEnumerable<string> stringsToSearch)
    {
        foreach (var item in stringsToSearch)
        {
            foreach (var match in Search(word, item))
                yield return match;
        }
    }

    public string FormatMatchText(string text, int pos)
    {
        var start = Math.Max(0, pos - 50);
        var end = Math.Min(start + 100, text.Length - 1);
        
        return string.Concat(
            start == 0 ? "" : "...",
            text.AsSpan(start, end - start), 
            end == text.Length - 1 ? "" : "...");
    }
}