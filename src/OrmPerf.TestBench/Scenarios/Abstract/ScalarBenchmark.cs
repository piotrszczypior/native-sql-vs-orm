using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OrmPerf.TestBench.Utilities;

namespace OrmPerf.TestBench.Scenarios.Abstract;

public abstract class ScalarBenchmark<TInheritor> : Benchmark<TInheritor>
    where TInheritor : ScalarBenchmark<TInheritor>
{
    [GlobalCleanup]
    public async Task Cleanup()
    {
        BenchmarkMetadataStore.Types.Add(typeof(TInheritor));

        var ormQueryString = SqlLogger.CapturedSql;
        
        var queryComparison = new SqlComparison
        {
            Raw = SqlQuery,
            Orm = ormQueryString
        };
        BenchmarkMetadataStore.QueryComparisons.Add(typeof(TInheritor), queryComparison);

        var analyzeComparison = new SqlComparison
        {
            Raw = Explain("ANALYZE", SqlQuery),
            Orm = Explain("ANALYZE", ormQueryString)
        };
        BenchmarkMetadataStore.AnalyzeComparisons.Add(typeof(TInheritor), analyzeComparison);

        var verboseComparison = new SqlComparison
        {
            Raw = Explain("VERBOSE", SqlQuery),
            Orm = Explain("VERBOSE", ormQueryString)
        };
        BenchmarkMetadataStore.VerboseComparisons.Add(typeof(TInheritor), verboseComparison);

        var costsComparison = new SqlComparison
        {
            Raw = Explain("COSTS", SqlQuery),
            Orm = Explain("COSTS", ormQueryString)
        };
        BenchmarkMetadataStore.CostsComparisons.Add(typeof(TInheritor), costsComparison);

        var settingsComparison = new SqlComparison
        {
            Raw = Explain("SETTINGS", SqlQuery),
            Orm = Explain("SETTINGS", ormQueryString)
        };
        BenchmarkMetadataStore.SettingsComparisons.Add(typeof(TInheritor), settingsComparison);

        var genericPlanComparison = new SqlComparison
        {
            Raw = Explain("GENERIC_PLAN", SqlQuery),
            Orm = Explain("GENERIC_PLAN", ormQueryString)
        };
        BenchmarkMetadataStore.GenericPlanComparisons.Add(typeof(TInheritor), genericPlanComparison);
        
        var buffersComparison = new SqlComparison
        {
            Raw = Explain("BUFFERS", SqlQuery),
            Orm = Explain("BUFFERS", ormQueryString)
        };
        BenchmarkMetadataStore.BuffersComparisons.Add(typeof(TInheritor), buffersComparison);
        
        var walComparison = new SqlComparison
        {
            Raw = Explain("WAL", SqlQuery),
            Orm = Explain("WAL", ormQueryString)
        };
        BenchmarkMetadataStore.WalComparisons.Add(typeof(TInheritor), walComparison);
        
        var timingComparison = new SqlComparison
        {
            Raw = Explain("TIMING", SqlQuery),
            Orm = Explain("TIMING", ormQueryString)
        };
        BenchmarkMetadataStore.TimingComparisons.Add(typeof(TInheritor), timingComparison);
        
        var summaryComparison = new SqlComparison
        {
            Raw = Explain("SUMMARY", SqlQuery),
            Orm = Explain("SUMMARY", ormQueryString)
        };
        BenchmarkMetadataStore.SummaryComparisons.Add(typeof(TInheritor), summaryComparison);
        
        var memoryComparison = new SqlComparison
        {
            Raw = Explain("MEMORY", SqlQuery),
            Orm = Explain("MEMORY", ormQueryString)
        };
        BenchmarkMetadataStore.MemoryComparisons.Add(typeof(TInheritor), memoryComparison);
    }
    
    protected DatabaseFacade OrmQuerySubject => DbContext.Database;
    protected abstract Func<Task<int>> OrmExecuteFactory { get; }
    
    protected abstract string SqlQuery { get; }
}