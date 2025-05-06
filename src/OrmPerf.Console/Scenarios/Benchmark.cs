using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OrmPerf.Persistence.Entities;
using DbContext = OrmPerf.Persistence.DbContext;

namespace OrmPerf.Console.Scenarios;

[MemoryDiagnoser]
[ThreadingDiagnoser]
[ExceptionDiagnoser]
public abstract class Benchmark<TInheritor, TEntity>
    where TInheritor : Benchmark<TInheritor, TEntity>
    where TEntity : KeylessEntity
{
    protected IServiceProvider ServiceProvider { get; private set; }
    protected DbContext DbContext { get; private set; }
    protected NpgsqlConnection NpgsqlConnection { get; private set; }
    
    [GlobalSetup]
    public async Task Setup()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        var serviceCollection = new ServiceCollection();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        serviceCollection.AddDbContext<DbContext>(builder =>
        {
            builder.UseNpgsql(connectionString);
        });
        serviceCollection.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));

        ServiceProvider = serviceCollection.BuildServiceProvider();
        DbContext = ServiceProvider.GetRequiredService<DbContext>();
        NpgsqlConnection = ServiceProvider.GetRequiredService<NpgsqlConnection>();
    }

    [GlobalCleanup]
    public async Task Cleanup()
    {
        var queryComparison = new QueryComparison
        {
            Sql = SqlQuery,
            Orm = OrmQuery.ToQueryString()
        };
        BenchmarkMetadataStore.BenchmarkQueryComparison.Add(typeof(TInheritor), queryComparison);

    }
    
    [Benchmark(Baseline = true)]
    public async Task Orm() => await OrmSubject();


    protected DbSet<TEntity> OrmQuerySubject => DbContext.Set<TEntity>();
    protected abstract IQueryable<FilmEntity> OrmQuery { get; }
    protected abstract Task OrmSubject();
    
    [Benchmark]
    public async Task Sql() => await SqlSubject();

    protected abstract string SqlQuery { get; }
    protected abstract Task SqlSubject();

}