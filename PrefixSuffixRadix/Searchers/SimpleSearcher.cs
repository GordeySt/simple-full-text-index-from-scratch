namespace PrefixSuffixRadix.Searchers;

public class SimpleSearcher : ISearcher
{
    private readonly string[] _words;

    public SimpleSearcher(string[] words)
    {
        _words = words;
    }

    public bool ContainsFullWord(string word) => _words.Contains(word);

    public IEnumerable<string> ContainsSubstring(string suffix) => _words.Where(x => x.Contains(suffix));
    
    public IEnumerable<string> StartsWith(string prefix) => _words.Where(x => x.StartsWith(prefix));
}