namespace FullTextIndex;

public class FullTextIndex
{
    private readonly Dictionary<string, HashSet<int>> _index = new();
    private readonly List<string> _content = new();
    private readonly Lexer _lexer = new();
    private readonly SimpleSearcher _searcher = new();

    public void AddStringToIndex(string text)
    {
        var documentId = _content.Count;
        
        foreach (var token in _lexer.GetTokens(text))
        {
            if (_index.TryGetValue(token, out var set))
                set.Add(documentId);
            else
                _index.Add(token, new HashSet<int>() {documentId});
        }
        
        _content.Add(text);
    }

    public IEnumerable<int> Search(string word) => 
        _index.TryGetValue(word.ToLowerInvariant(), out var set) ? set : Enumerable.Empty<int>();

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