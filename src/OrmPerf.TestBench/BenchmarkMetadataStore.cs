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
    
    public static Dictionary<Type, SqlComparison> QueryComparisons { get; } = new();

    public static void Export(IConfig config)
    {
        foreach (var (type, sqlComparison) in QueryComparisons)
        {
            var json = JsonSerializer.Serialize(new ExportBenchmark
            {
                Namespace = type.Namespace!,
                Type = type.Name,
                Sql = sqlComparison
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
}

public class SqlComparison
{
    public string Raw { get; set; }
    public string Orm { get; set; }
}