namespace PrefixSuffixRadix.Searchers;

public interface ISearcher
{
    bool ContainsFullWord(string word);
    
    IEnumerable<string> StartsWith(string prefix);

    IEnumerable<string> ContainsSubstring(string suffix);
}