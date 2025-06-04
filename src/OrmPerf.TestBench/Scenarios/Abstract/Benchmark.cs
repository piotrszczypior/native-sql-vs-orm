using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Npgsql;
using OrmPerf.TestBench.Utilities;
using Testcontainers.PostgreSql;
using DbContext = OrmPerf.Persistence.DbContext;

namespace OrmPerf.TestBench.Scenarios.Abstract;

[MemoryDiagnoser]
[ThreadingDiagnoser]
[ExceptionDiagnoser]
[JsonExporterAttribute.Full]
public abstract class Benchmark<TInheritor>
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

        var postgreSqlContainer = new PostgreSqlBuilder()
            .WithLogger(NullLogger.Instance)
            .WithName($"postgres-{typeof(TInheritor).Name.ToLowerInvariant()}-{Guid.NewGuid()}")
            .Build();
        
        AsyncUtilities.RunSync(() => postgreSqlContainer.StartAsync());
        
        var connectionString = postgreSqlContainer.GetConnectionString();
        
        serviceCollection.AddDbContext<DbContext>(builder =>
        {
            builder.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(log =>
                {
                    SqlLogger.AddLogLine(log);
                    Console.WriteLine(log);
                });
        });
        serviceCollection.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));

        ServiceProvider = serviceCollection.BuildServiceProvider();
        DbContext = ServiceProvider.GetRequiredService<DbContext>();
        NpgsqlConnection = ServiceProvider.GetRequiredService<NpgsqlConnection>();

        AsyncUtilities.RunSync(() => DbContext.Database.MigrateAsync());
        AsyncUtilities.RunSync(() => Seeder.SeedAsync(DbContext));
    }
    
    [Benchmark(Baseline = true)]
    public async Task Orm()
    {
        SqlLogger.Clear();
        await OrmSubject();
    }
    protected abstract Task OrmSubject();
    
    [Benchmark]
    public async Task Sql() => await SqlSubject();
    protected abstract Task SqlSubject();
}