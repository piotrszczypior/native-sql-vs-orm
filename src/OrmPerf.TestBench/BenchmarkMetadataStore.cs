using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Configs;

namespace OrmPerf.TestBench;

public static class BenchmarkMetadataStore
{
    private static JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private static string DefaultArtifactsPath = Path.Join(Directory.GetCurrentDirectory(), "BenchmarkDotNet.Artifacts");

    public static List<Type> Types { get; } = new();
    public static Dictionary<Type, SqlComparison> QueryComparisons { get; } = new();
    public static Dictionary<Type, SqlComparison> AnalyzeComparisons { get; } = new();
    public static Dictionary<Type, SqlComparison> VerboseComparisons { get; set; } = new();
    public static Dictionary<Type, SqlComparison> CostsComparisons { get; set; } = new();
    public static Dictionary<Type, SqlComparison> SettingsComparisons { get; set; } = new();
    public static Dictionary<Type, SqlComparison> GenericPlanComparisons { get; set; } = new();
    public static Dictionary<Type, SqlComparison> BuffersComparisons { get; set; } = new();
    public static Dictionary<Type, SqlComparison> WalComparisons { get; set; } = new();
    public static Dictionary<Type, SqlComparison> TimingComparisons { get; set; } = new();
    public static Dictionary<Type, SqlComparison> SummaryComparisons { get; set; } = new();
    public static Dictionary<Type, SqlComparison> MemoryComparisons { get; set; } = new();

    public static void Export(IConfig config)
    {
        foreach (var type in Types)
        {
            var json = JsonSerializer.Serialize(new ExportBenchmark
            {
                Namespace = type.Namespace!,
                Type = type.Name,
                Sql = QueryComparisons.GetValueOrDefault(type, new SqlComparison()),
                Analyze = AnalyzeComparisons.GetValueOrDefault(type, new SqlComparison()),
                Verbose = VerboseComparisons.GetValueOrDefault(type, new SqlComparison()),
                Costs = CostsComparisons.GetValueOrDefault(type, new SqlComparison()),
                Settings = SettingsComparisons.GetValueOrDefault(type, new SqlComparison()),
                GenericPlan = GenericPlanComparisons.GetValueOrDefault(type, new SqlComparison()),
                Buffers = BuffersComparisons.GetValueOrDefault(type, new SqlComparison()),
                Wal = WalComparisons.GetValueOrDefault(type, new SqlComparison()),
                Timing = TimingComparisons.GetValueOrDefault(type, new SqlComparison()),
                Summary = SummaryComparisons.GetValueOrDefault(type, new SqlComparison()),
                Memory = MemoryComparisons.GetValueOrDefault(type, new SqlComparison())
            }, _serializerOptions);
            var path = Path.Join(config.ArtifactsPath ?? DefaultArtifactsPath, "results", $"{type.FullName}-report-metadata.json");
            File.WriteAllText(path, json);
        }
    }
}

file class ExportRoot
{
    public IEnumerable<ExportBenchmark> Benchmarks { get; set; } 
}

file class ExportBenchmark
{
    public string Namespace { get; set; }
    public string Type { get; set; }
    public SqlComparison Sql { get; set; }
    public SqlComparison Analyze { get; set; }
    public SqlComparison Verbose { get; set; }
    public SqlComparison Costs { get; set; }
    public SqlComparison Settings { get; set; }
    public SqlComparison GenericPlan { get; set; }
    public SqlComparison Buffers { get; set; }
    public SqlComparison Wal { get; set; }
    public SqlComparison Timing { get; set; }
    public SqlComparison Summary { get; set; }
    public SqlComparison Memory { get; set; }
}

public class SqlComparison
{
    public string Raw { get; set; }
    public string Orm { get; set; }
}