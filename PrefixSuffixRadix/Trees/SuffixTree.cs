using System.Text;

namespace PrefixSuffixRadix.Trees;

public class SuffixTree
{
    private class TreeNode
    {
        public char C { get; set; }
        public Dictionary<char, TreeNode>? Children { get; set; }
        public HashSet<string>? Contains { get; set; }
        public bool IsWord { get; set; }
    }

    private readonly TreeNode _root;

    public SuffixTree(string[] words)
    {
        _root = new TreeNode();

        foreach (var word in words)
        {
            for (var i = 0; i < word.Length; i++)
                AddWord(i, word);
        }
    }

    public bool ContainsFullWord(string word) => GetNode(word)?.IsWord == true;

    private void AddWord(int index, string word)
    {
        var current = _root;
        var suffix = word.AsSpan(index);
        
        foreach (var t in suffix)
        {
            if (current.Children is not null && current.Children.TryGetValue(t, out var node))
                current = node;
            else
            {
                current.Children ??= new Dictionary<char, TreeNode>();
                current.Children.Add(t, current = new TreeNode() {C = t});
            }
        }

        if (index is 0) 
            current.IsWord = true;

        current.Contains ??= new HashSet<string>();
        
        current.Contains.Add(word);
    }

    public IEnumerable<string> ContainsString(string substr)
    {
        var node = GetNode(substr);

        return node is null ? Enumerable.Empty<string>() : GetContains(node).ToHashSet();
    }

    private IEnumerable<string> GetContains(TreeNode node)
    {
        if (node.Contains is not null)
            foreach (var item in node.Contains)
                yield return item;

        if (node.Children is null)
            yield break;

        foreach (var word in node.Children.SelectMany(child => GetContains(child.Value)))
            yield return word;
    }

    public IEnumerable<string> StartsWith(string prefix)
    {
        var node = GetNode(prefix);

        return node is null
            ? Enumerable.Empty<string>()
            : GetWords(new StringBuilder().Append(prefix.AsSpan(0, prefix.Length - 1)), node);
    }

    private TreeNode? GetNode(string prefix)
    {
        var current = _root;

        foreach (var t in prefix)
        {
            if (current.Children is not null && current.Children.TryGetValue(t, out var node))
                current = node;
            else
                return null;
        }

        return current;
    }

    private IEnumerable<string> GetWords(StringBuilder builder, TreeNode node)
    {
        builder.Append(node.C);

        if (node.IsWord)
            yield return builder.ToString();

        if (node.Children is null)
            yield break;

        foreach (var word in node.Children.Values.SelectMany(childNode => GetWords(builder, childNode)))
            yield return word;

        builder.Remove(builder.Length - 1, 1);
    }
}