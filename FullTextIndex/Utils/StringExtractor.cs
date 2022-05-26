namespace FullTextIndex;

public class StringExtractor
{
    public static IEnumerable<string> ArticleSet() => ReadArticleSet("articles1.txt");

    private static IEnumerable<string> ReadArticleSet(string fileName)
    {
        using var reader = new StreamReader(Path.Combine(@"D:\dotnet-lessons\full-text-index\assets", fileName));

        while (reader.ReadLine() is { } line)
            yield return line;
    }
}