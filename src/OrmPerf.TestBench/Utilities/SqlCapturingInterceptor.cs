using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace OrmPerf.TestBench.Utilities;

public class SqlCapturingInterceptor : DbCommandInterceptor
{
    public string CapturedOrmSql { get; private set; } = string.Empty;

    public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        CapturedOrmSql = command.CommandText;
        return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
    }
}