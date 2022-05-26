namespace FullTextIndex.Storage;

public interface IContentStorage
{
    void AddDocument(string document);
    string GetDocument(int id);
}