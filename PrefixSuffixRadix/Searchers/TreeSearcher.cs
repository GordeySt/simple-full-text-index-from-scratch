namespace PrefixSuffixRadix.Searchers;

public class TreeSearcher : ISearcher
{
    private readonly PrefixTree _tree;

    public TreeSearcher(string[] words)
    {
        _tree = new PrefixTree(words);
    }

    public bool ContainsFullWord(string word) => _tree.ContainsFullWord(word);

    public IEnumerable<string> ContainsSubstring(string suffix)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<string> StartsWith(string prefix) => _tree.StartsWith(prefix);
}