using BenchmarkDotNet.Running;
using FullTextIndex;

/*
var list = StringExtractor.ArticleSet().ToArray();

var searcher = new SimpleSearcher();
var list1 = searcher.Search("Russia", list);

foreach (var item in list1)
    Console.WriteLine(item);
    */

BenchmarkRunner.Run<SearcherBenchmark>();