using System.Text.RegularExpressions;

namespace OrmPerf.TestBench.Utilities;

public static class SqlLogger
{
    private static readonly Dictionary<string, int> _capturedSql = new();
    
    private static readonly Regex SqlCommandRegex = new(
        @"DbCommand.*\]\s+((INSERT|SELECT|UPDATE|DELETE)[\s\S]+?;)", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static string CapturedSql => _capturedSql.Where(kv => HookVerb is null || kv.Key.StartsWith(HookVerb))
                                                    .MaxBy(kv => kv.Value).Key;
    public static string? HookVerb { get; set; }
    public static bool Capture { get; set; }

    public static void AddLogLine(string logLine)
    {
        if (Capture is false)
        {
            return;
        }
        
        var match = SqlCommandRegex.Match(logLine);
        if (match.Success)
        {
            var sql = match.Groups[1].Value.Trim();

            _capturedSql.TryAdd(sql, 0);
            _capturedSql[sql]++;
        }
    }
    public static void Clear()
    {
        _capturedSql.Clear();
    }
}