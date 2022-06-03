﻿using BenchmarkDotNet.Running;
using PrefixSuffixRadix;
using PrefixSuffixRadix.Benchmarks;
using PrefixSuffixRadix.Trees;
using PreparationToStream.FullText.Database;

/*var ctx = new DatabaseContext();
var arr = ctx.Words.Select(x => x.Word1).ToArray();
var prefix = new PrefixTree(arr!);
var result = prefix.StartsWith("like").ToArray();

foreach (var item in result)
    Console.WriteLine(item);*/

/*
var suffixTree = new SuffixTree(new[]
{
    "hello",
    "hell",
    "ololo"
});

Console.WriteLine();
*/

BenchmarkRunner.Run<ContainsSubstringBenchmark>();