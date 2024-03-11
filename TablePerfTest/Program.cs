using BenchmarkDotNet.Running;
using TablePerfTest;


const bool runPerf = true;

// ReSharper disable once ConditionIsAlwaysTrueOrFalse
if (runPerf)
{
    var _ = BenchmarkRunner.Run<TablePerfBenchmark>();
}
else
{
    // Sanity check to ensure the tables are ok.
    var x = new TablePerfBenchmark();
    x.amount = 1000;
    x.SetupData();

    var y = x.FillColumnBasedTable();

    Console.WriteLine(y);
}
