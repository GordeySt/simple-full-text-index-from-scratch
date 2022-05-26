namespace FullTextIndex.Storage;

public class ContentStorage
{
    private readonly string _path;
    private readonly List<int> _documentPositions = new();

    private string ContentFile => Path.Combine(_path, ".content");
    private string HeaderFile => Path.Combine(_path, ".hcontent");

    public ContentStorage(string path)
    {
        _path = path;
        Initialize();
    }

    public void AddDocument(string document)
    {
        using var writer = new BinaryWriter(File.Open(ContentFile, FileMode.Append));

        var pos = (int) writer.BaseStream.Position;
        _documentPositions.Add(pos);
        writer.Write(document);

        using var headerWriter = new BinaryWriter(File.Open(HeaderFile, FileMode.Append));
        headerWriter.Write(pos);
    }

    public string GetDocument(int id)
    {
        var pos = _documentPositions[id];
        
        using var reader = new BinaryReader(File.OpenRead(ContentFile));
        reader.BaseStream.Position = pos;

        return reader.ReadString();
    }

    private void Initialize()
    {
        if (!File.Exists(HeaderFile))
        {
            using var file = File.Create(HeaderFile);
            return;
        }

        using var reader = new BinaryReader(File.OpenRead(HeaderFile));
        
        while (reader.BaseStream.Position < reader.BaseStream.Length)
            _documentPositions.Add(reader.ReadInt32());
    }
}