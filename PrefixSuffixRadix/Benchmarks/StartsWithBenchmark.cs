using BenchmarkDotNet.Attributes;
using PrefixSuffixRadix.Searchers;

namespace PrefixSuffixRadix.Benchmarks;

[IterationCount(8)]
[WarmupCount(1)]
public class StartsWithBenchmark : ContainsFullWordBenchmark
{
    protected override void BenchmarkAction(string word, ISearcher searcher)
    {
        foreach (var item in searcher.StartsWith(word))
            ;
    }
}