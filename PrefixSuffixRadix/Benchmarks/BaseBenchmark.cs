using PrefixSuffixRadix.Searchers;
using PreparationToStream.FullText.Database;

namespace PrefixSuffixRadix.Benchmarks;

public abstract class BaseBenchmark
{
    protected readonly SimpleSearcher Searcher;
    protected readonly HashsetSearcher Hash;
    protected readonly SortedSearcher Sorted;
    protected readonly TreeSearcher Tree;

    protected readonly string[] WordsToSearch = {
        "adolescence",
        "sublimely",
        "mushy",
        "muckraker",
        "guardsmen",
        "vey",
        "tugboats",
        "cohn",
        "sichuan",
        "princeville",
        "blabla",
        "ololo",
        "alala",
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
    };
    
    public BaseBenchmark()
    {
        var ctx = new DatabaseContext();
        var arr = ctx.Words.Select(x => x.Word1).ToArray();
        Searcher = new SimpleSearcher(arr!);
        Hash = new HashsetSearcher(arr!);
        Tree = new TreeSearcher(arr!);
        Sorted = new SortedSearcher(arr!);
    }

    protected abstract void BenchmarkAction(string word, ISearcher searcher);

    protected void DoAction(ISearcher searcher)
    {
        foreach (var item in WordsToSearch)
            BenchmarkAction(item, searcher);
    }
}