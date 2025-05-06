namespace OrmPerf.Console;

public static class BenchmarkMetadataStore
{
    public static Dictionary<Type, QueryComparison> BenchmarkQueryComparison { get; } = new(); 
}

public class QueryComparison
{
    public string Sql { get; set; }
    public string Orm { get; set; }
}