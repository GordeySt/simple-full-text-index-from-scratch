using BenchmarkDotNet.Attributes;
using PrefixSuffixRadix.Searchers;

namespace PrefixSuffixRadix.Benchmarks;

[IterationCount(8)]
[WarmupCount(1)]
public class ContainsFullWordBenchmark : BaseBenchmark
{
    protected override void BenchmarkAction(string word, ISearcher searcher)
    {
        searcher.ContainsFullWord(word);
    }

    [Benchmark]
    public void SimpleSearch() => DoAction(Searcher);

    [Benchmark(Baseline = true)]
    public void HashsetSearch() => DoAction(Hash);

    [Benchmark]
    public void TreeSearch() => DoAction(Tree);

    [Benchmark]
    public void SortedSetSearch() => DoAction(Sorted);

}