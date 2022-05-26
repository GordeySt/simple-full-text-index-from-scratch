using BenchmarkDotNet.Running;
using FullTextIndex.Benchmarks;

/*var list = StringExtractor.ArticleSet().ToArray();

var searcher = new SimpleSearcher();
var list1 = searcher.Search("lot", list);

foreach (var item in list1)
    Console.WriteLine(item);*/

BenchmarkRunner.Run<SearcherBenchmark>();