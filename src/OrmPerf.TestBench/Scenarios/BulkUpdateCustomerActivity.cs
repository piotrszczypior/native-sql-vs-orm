using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class BulkUpdateCustomerActivity : ScalarBenchmark<BulkUpdateCustomerActivity>
{
    private static readonly DateTime ActivityThreshold = DateTime.UtcNow.AddMonths(-6);

    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var inactiveCustomers = await DbContext.Customers
            .Where(c => !c.Rentals.Any(r => r.RentalStart >= ActivityThreshold))
            .ToListAsync();

        foreach (var customer in inactiveCustomers)
        {
            customer.IsActive = false;
            customer.LastUpdate = DateTime.UtcNow;
        }

        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          UPDATE "Customers"
                                          SET "Active" = false, "LastUpdate" = @LastUpdate
                                          WHERE "Id" NOT IN (
                                              SELECT DISTINCT "CustomerId"
                                              FROM "Rentals"
                                              WHERE "RentalStart" >= @ActivityThreshold
                                          )
                                          AND "IdActive" = true
                                          """;

    protected override Task SqlSubject() =>
        NpgsqlConnection.ExecuteAsync(SqlQuery, new { ActivityThreshold, LastUpdate = DateTime.UtcNow });
}