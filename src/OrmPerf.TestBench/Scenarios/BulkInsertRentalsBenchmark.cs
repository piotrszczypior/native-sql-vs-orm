using Dapper;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class BulkInsertRentalsBenchmark : ScalarBenchmark<BulkInsertRentalsBenchmark>
{
    private const int RentalCount = 100;

    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var now = DateTime.UtcNow;
        var rentals = Enumerable.Range(0, RentalCount)
            .Select(_ => new RentalEntity
            {
                CustomerId = 1,
                InventoryId = 1,
                RentalStart = now,
                StaffId = 1,
                LastUpdate = now
            }).ToList();

        DbContext.Rentals.AddRange(rentals);
        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          INSERT INTO "Rentals" ("RentalStart", "InventoryId", "CustomerId", "StaffId", "LastUpdate")
                                          VALUES (@RentalStart, @InventoryId, @CustomerId, @StaffId, @LastUpdate)
                                          """;

    protected override async Task SqlSubject()
    {
        var now = DateTime.UtcNow;
        var rentals = Enumerable.Range(0, RentalCount)
            .Select(_ => new
            {
                RentalStart = now,
                InventoryId = 1,
                CustomerId = 1,
                StaffId = 1,
                LastUpdate = now
            });

        await NpgsqlConnection.ExecuteAsync(SqlQuery, rentals);
    }
}