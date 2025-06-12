using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class DeleteOldRentals : ScalarBenchmark<DeleteOldRentals>
{
    private readonly DateTime _cutoffDate = DateTime.UtcNow.AddYears(-2);

    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var oldRentals = await DbContext.Rentals
            .Where(r => r.RentalStart < _cutoffDate)
            .ToListAsync();
        
        DbContext.Rentals.RemoveRange(oldRentals);
        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          DELETE FROM "Rentals"
                                          WHERE "RentalStart" < @CutoffDate
                                          """;

    protected override async Task SqlSubject()
    {
        await NpgsqlConnection.ExecuteAsync(SqlQuery, new { CutoffDate = _cutoffDate });
    }
}