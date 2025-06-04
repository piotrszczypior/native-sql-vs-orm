using System.Text.RegularExpressions;

namespace OrmPerf.TestBench.Utilities;

public static class SqlLogger
{
    private static readonly List<string> _capturedSql = new();
    private static readonly Regex SqlCommandRegex = new(
        @"DbCommand.*\]\s+((INSERT|SELECT|UPDATE|DELETE)[\s\S]+?;)", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static IReadOnlyList<string> CapturedSql => _capturedSql.AsReadOnly();

    public static void AddLogLine(string logLine)
    {
        var match = SqlCommandRegex.Match(logLine);
        if (match.Success)
        {
            lock (_capturedSql)
            {
                _capturedSql.Add(match.Groups[1].Value.Trim());
            }
        }
    }

    public static void Clear()
    {
        lock (_capturedSql)
        {
            _capturedSql.Clear();
        }
    }
}