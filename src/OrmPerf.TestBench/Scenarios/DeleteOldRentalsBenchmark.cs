using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class DeleteOldRentalsBenchmark : ScalarBenchmark<DeleteOldRentalsBenchmark>
{
    private static readonly DateTime Threshold = DateTime.UtcNow.AddYears(-5);

    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var oldRentals = await DbContext.Rentals
            .Where(r => r.RentalEnd < Threshold)
            .ToListAsync();

        DbContext.Rentals.RemoveRange(oldRentals);
        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          DELETE FROM "Rentals"
                                          WHERE "RentalEnd" < @Threshold
                                          """;

    protected override Task SqlSubject() =>
        NpgsqlConnection.ExecuteAsync(SqlQuery, new { Threshold });
}