namespace FullTextIndex.Storage;

public class InMemoryContentStorage : IContentStorage
{
    private readonly List<string> _documents = new();
    
    public void AddDocument(string document) => _documents.Add(document);

    public string GetDocument(int id) => _documents[id];
}