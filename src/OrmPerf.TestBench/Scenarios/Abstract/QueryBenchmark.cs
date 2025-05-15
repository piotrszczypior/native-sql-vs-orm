using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;

namespace OrmPerf.TestBench.Scenarios.Abstract;

public abstract class QueryBenchmark<TInheritor, TEntity> : Benchmark<TInheritor>
    where TInheritor : QueryBenchmark<TInheritor, TEntity>
    where TEntity : KeylessEntity
{
    [GlobalCleanup]
    public async Task Cleanup()
    {
        var queryComparison = new SqlComparison
        {
            Raw = SqlQuery,
            Orm = OrmQuery.ToQueryString()
        };
        
        BenchmarkMetadataStore.QueryComparisons.Add(typeof(TInheritor), queryComparison);
    }

    protected DbSet<TEntity> OrmQuerySubject => DbContext.Set<TEntity>();
    protected abstract IQueryable<TEntity> OrmQuery { get; }

    protected abstract string SqlQuery { get; }
}