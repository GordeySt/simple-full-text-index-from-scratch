namespace PrefixSuffixRadix.Searchers;

public class HashsetSearcher : ISearcher
{
    private readonly HashSet<string> _hashset;
 
    public HashsetSearcher(string[] words)
    {
        _hashset = words.ToHashSet()!;
    }

    public bool ContainsFullWord(string word) => _hashset.Contains(word);
    
    public IEnumerable<string> StartsWith(string prefix) => _hashset.Where(x => x.StartsWith(prefix));

    public IEnumerable<string> ContainsSubstring(string suffix) => _hashset.Where(x => x.Contains(suffix));
}