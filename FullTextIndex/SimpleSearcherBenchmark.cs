using BenchmarkDotNet.Attributes;

namespace FullTextIndex;

[MemoryDiagnoser]
[WarmupCount(1)]
[IterationCount(5)]
public class SimpleSearcherBenchmark
{
    private readonly string[] _dataset;
    
    public SimpleSearcherBenchmark()
    {
        _dataset = StringExtractor.ArticleSet().ToArray();
    }

    [Benchmark]
    public void SimpleSearch() => new SimpleSearcher().Search("the", _dataset).ToArray();
}