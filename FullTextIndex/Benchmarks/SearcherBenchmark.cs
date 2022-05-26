using BenchmarkDotNet.Attributes;
using FullTextIndex.Indexes;

namespace FullTextIndex.Benchmarks;

[MemoryDiagnoser]
[WarmupCount(1)]
[IterationCount(5)]
public class SearcherBenchmark
{
    private readonly string[] _dataset;
    private readonly SimpleFullTextIndex _index;
    private readonly FullTextIndexWithPositions _indexWithPositions;

    [Params("lot", "Russia", "the")]
    public string Query;
    
    public SearcherBenchmark()
    {
        _dataset = StringExtractor.ArticleSet().ToArray();
        _index = new SimpleFullTextIndex();
        _indexWithPositions = new FullTextIndexWithPositions();

        foreach (var item in _dataset)
        {
            _index.AddStringToIndex(item);
            _indexWithPositions.AddStringToIndex(item);
        }
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
    
    [Benchmark]
    public void FullTextIndexedWithPositionsSearch()
    {
        _indexWithPositions.SearchTest(Query).ToArray();
    }
}