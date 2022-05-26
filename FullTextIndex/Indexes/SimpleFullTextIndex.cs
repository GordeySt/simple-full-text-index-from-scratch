using FullTextIndex.Utils;

namespace FullTextIndex.Indexes;

// JSON example of how this index will be represented
// {
//   "monday": [20, 345, 1564, 3456]
// }
// where we have documentIds in our array.
public class SimpleFullTextIndex
{
    private readonly Dictionary<string, HashSet<int>> _index = new();
    private readonly List<string> _content = new();
    private readonly Lexer _lexer = new();
    private readonly SimpleSearcher _searcher = new();

    public void AddStringToIndex(string text)
    {
        var documentId = _content.Count;
        
        foreach (var (token, _) in _lexer.GetTokens(text))
        {
            if (_index.TryGetValue(token, out var set))
                set.Add(documentId);
            else
                _index.Add(token, new HashSet<int>() {documentId});
        }
        
        _content.Add(text);
    }

    public ISet<int> Search(string word) =>
        _index.TryGetValue(word.ToLowerInvariant(), out var set) ? set : new HashSet<int>();

    public IEnumerable<int> Search(IEnumerable<string> words)
    {
        var sets = words.Select(Search).ToArray();
        var result = sets[0].Intersect(sets[1]);

        for (var i = 2; i < sets.Length; i++)
            result = result.Intersect(sets[i]);

        return result;
    }

    public IEnumerable<string> SearchTest(string word)
    {
        var documentIds = Search(word);

        foreach (var documentId in documentIds)
        {
            foreach (var match in _searcher.Search(word, _content[documentId]))
                yield return match;
        }
    }
}