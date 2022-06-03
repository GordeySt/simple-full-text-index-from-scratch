using BenchmarkDotNet.Attributes;
using PrefixSuffixRadix.Searchers;

namespace PrefixSuffixRadix.Benchmarks;

[IterationCount(8)]
[WarmupCount(1)]
public class ContainsSubstringBenchmark : BaseBenchmark
{
    private readonly string[] _suffixes = {
        "orn",
        "rse",
        "ath",
        "yan",
        "rge",
        "rmi"
    };

    public ContainsSubstringBenchmark()
    {
        WordsToSearch = _suffixes;
    }

    protected override void BenchmarkAction(string word, ISearcher searcher)
    {
        foreach (var item in searcher.ContainsSubstring(word))
            ;
    }

    [Benchmark]
    public void Hashset() => DoAction(Hash);

    [Benchmark]
    public void Simple() => DoAction(Searcher);

    [Benchmark(Baseline = true)]
    public void SuffixTree() => DoAction(Tree);
}