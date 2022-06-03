using PrefixSuffixRadix.Trees;

namespace PrefixSuffixRadix.Searchers;

public class TreeSearcher : ISearcher
{
    private readonly PrefixTree _tree;
    private readonly SuffixTree _suffixTree;

    public TreeSearcher(string[] words)
    {
        _tree = new PrefixTree(words);
        _suffixTree = new SuffixTree(words);
    }

    public bool ContainsFullWord(string word) => _tree.ContainsFullWord(word);

    public IEnumerable<string> ContainsSubstring(string suffix) => _suffixTree.ContainsString(suffix);
    
    public IEnumerable<string> StartsWith(string prefix) => _tree.StartsWith(prefix);
}