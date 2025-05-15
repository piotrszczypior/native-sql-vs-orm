using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OrmPerf.TestBench.Scenarios.Abstract;

public abstract class ScalarBenchmark<TInheritor> : Benchmark<TInheritor>
    where TInheritor : ScalarBenchmark<TInheritor>
{
    [GlobalCleanup]
    public async Task Cleanup()
    {
        var queryComparison = new SqlComparison
        {
            Raw = SqlQuery,
            Orm = SqlQuery
        };
        BenchmarkMetadataStore.QueryComparisons.Add(typeof(TInheritor), queryComparison);
    }
    
    protected DatabaseFacade OrmQuerySubject => DbContext.Database;
    protected abstract Func<Task<int>> OrmExecuteFactory { get; }
    
    protected abstract string SqlQuery { get; }
}