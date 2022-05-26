namespace FullTextIndex.Utils;

public class Lexer
{
    public IEnumerable<string> GetTokens(string text)
    {
        var start = -1;

        for (var i = 0; i < text.Length; i++)
        {
            if (char.IsLetterOrDigit(text[i]))
            {
                if (start == -1)
                    start = i;
            }
            else
            {
                if (start >= 0)
                {
                    yield return GetToken(text, i, start);
                    start = -1;
                }
            }
        }
    }

    private string GetToken(string text, int i, int start) => 
        text.Substring(start, i - start).Normalize().ToLowerInvariant();
}