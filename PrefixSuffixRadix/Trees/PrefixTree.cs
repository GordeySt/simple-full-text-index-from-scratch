using System.Text;

namespace PrefixSuffixRadix.Trees;

public class PrefixTree
{
    private class TreeNode
    {
        public char C { get; set; }
        public Dictionary<char, TreeNode> Children { get; set; }
        public bool IsWord { get; set; }
    }

    private readonly TreeNode _root;

    public PrefixTree(string[] words)
    {
        _root = new TreeNode();

        foreach (var word in words)
        {
            AddWord(word);
        }
    }

    public bool ContainsFullWord(string word) => GetNode(word)?.IsWord == true;

    private void AddWord(string word)
    {
        var current = _root;

        foreach (var t in word)
        {
            if (current.Children is not null && current.Children.TryGetValue(t, out var node))
                current = node;
            else
            {
                current.Children ??= new Dictionary<char, TreeNode>();
                current.Children.Add(t, current = new TreeNode() {C = t});
            }
        }

        current.IsWord = true;
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