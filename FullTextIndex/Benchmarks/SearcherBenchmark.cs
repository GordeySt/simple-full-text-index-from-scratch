using BenchmarkDotNet.Attributes;

namespace FullTextIndex;

[MemoryDiagnoser]
[WarmupCount(1)]
[IterationCount(5)]
public class SearcherBenchmark
{
    private readonly string[] _dataset;
    private readonly SimpleFullTextIndex _index;

    [Params("lot", "Russia", "the")]
    public string Query;
    
    public SearcherBenchmark()
    {
        _dataset = StringExtractor.ArticleSet().ToArray();
        _index = new SimpleFullTextIndex();

        foreach (var item in _dataset)
            _index.AddStringToIndex(item);
    }

    [Benchmark(Baseline = true)]
    public void SimpleSearch()
    { 
        new SimpleSearcher().Search(Query, _dataset).ToArray();  
    }

    [Benchmark]
    public void FullTextIndexedSearch()
    {
        _index.SearchTest(Query).ToArray();
    }
}