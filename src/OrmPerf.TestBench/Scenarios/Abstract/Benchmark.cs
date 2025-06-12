using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Npgsql;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Utilities;
using Testcontainers.PostgreSql;
using DbContext = OrmPerf.Persistence.DbContext;

namespace OrmPerf.TestBench.Scenarios.Abstract;

public abstract class Benchmark
{
    public static string? ConnectionString { get; set; }
}

[MemoryDiagnoser]
[ThreadingDiagnoser]
[ExceptionDiagnoser]
[JsonExporterAttribute.Full]
public abstract class Benchmark<TInheritor> : Benchmark
    where TInheritor : Benchmark<TInheritor>
{
    protected IServiceProvider ServiceProvider { get; private set; }
    protected DbContext DbContext { get; private set; }
    protected NpgsqlConnection NpgsqlConnection { get; private set; }
    
    [GlobalSetup]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        var serviceCollection = new ServiceCollection();

        string connectionString;
        if (ConnectionString is null)
        {
            var postgreSqlContainer = new PostgreSqlBuilder()
                .WithLogger(NullLogger.Instance)
                .WithName($"postgres-{typeof(TInheritor).Name.ToLowerInvariant()}-{Guid.NewGuid()}")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .Build();
        
            AsyncUtilities.RunSync(() => postgreSqlContainer.StartAsync());
            
            connectionString = postgreSqlContainer.GetConnectionString();
        }
        else
        {
            connectionString = ConnectionString;
        }
        
        serviceCollection.AddDbContext<DbContext>(builder =>
        {
            builder.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(log =>
                {
                    SqlLogger.AddLogLine(log);
                    // Console.WriteLine(log);
                });
        });
        serviceCollection.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));

        ServiceProvider = serviceCollection.BuildServiceProvider();
        DbContext = ServiceProvider.GetRequiredService<DbContext>();
        NpgsqlConnection = ServiceProvider.GetRequiredService<NpgsqlConnection>();

        DbContext.Database.Migrate();
        
        if (DbContext.Films.Any() is false)
        {
            AsyncUtilities.RunSync(() => Seeder.SeedAsync(DbContext));
        }
    }
    
    [Benchmark(Baseline = true)]
    public async Task Orm()
    {
        try
        {
            SqlLogger.Capture = true;
            SqlLogger.HookVerb = OrmHookVerb;
            await OrmSubject();
        }
        finally
        {
            SqlLogger.Capture = false;
        }
    }

    protected virtual string? OrmHookVerb => null;

    protected abstract Task OrmSubject();
    
    [Benchmark]
    public async Task Sql() => await SqlSubject();
    protected abstract Task SqlSubject();
    
    protected string Explain(string mode, string query)
    {
        try
        {
            return NpgsqlConnection.ExecuteScalar($"EXPLAIN {mode} {query}").ToString();
        }
        catch
        {
            return string.Empty;
        }
    }
}