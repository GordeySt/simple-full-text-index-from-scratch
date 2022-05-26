using FullTextIndex.Utils;

namespace FullTextIndex.Indexes;

// JSON example of how this index will be represented
// {
//   "monday": [20: [1, 3, 17], 345: [8, 12, 16], 1564: [3, 4]]
// }
// where we have documentIds in our first array and positions of this word
// in the documents in our arrays inside the first array.
public class FullTextIndexWithPositions
{
    private readonly Dictionary<string, Dictionary<int, List<int>>> _index = new();
    private readonly List<string> _content = new();
    private readonly Lexer _lexer = new();
    private readonly SimpleSearcher _searcher = new();

    public void AddStringToIndex(string text)
    {
        var documentId = _content.Count;
        
        foreach (var (token, position) in _lexer.GetTokens(text))
        {
            if (_index.TryGetValue(token, out var dict))
            {
                if (dict.TryGetValue(documentId, out var positions))
                    positions.Add(position);
                else
                    dict.Add(documentId, new List<int>() { position });
            }
            else
                _index.Add(token, new Dictionary<int, List<int>>() 
                {
                    [documentId] = new() { position }
                });
        }
        
        _content.Add(text);
    }

    public Dictionary<int, List<int>> Search(string word) => 
        _index.TryGetValue(word.ToLowerInvariant(), out var set) ? set : new Dictionary<int, List<int>>();

    public IEnumerable<string> SearchTest(string word)
    {
        var documentIds = Search(word);

        foreach (var documentMatches in documentIds)
        {
            foreach (var match in documentMatches.Value)
               yield return _searcher.FormatMatchText(_content[documentMatches.Key], match);
        }
    }
}