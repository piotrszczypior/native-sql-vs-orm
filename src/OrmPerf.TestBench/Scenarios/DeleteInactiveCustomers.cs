using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class DeleteInactiveCustomers : ScalarBenchmark<DeleteInactiveCustomers>
{
    private static readonly DateTime InactivityThreshold = DateTime.UtcNow.AddYears(-2);

    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var inactiveCustomers = await DbContext.Customers
            .Where(c => !c.IsActive && c.LastUpdate < InactivityThreshold)
            .Where(c => !c.Rentals.Any())
            .ToListAsync();

        DbContext.Customers.RemoveRange(inactiveCustomers);
        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          DELETE FROM "Customers"
                                          WHERE "IsActive" = false
                                          AND "LastUpdate" < @InactivityThreshold
                                          AND "Id" NOT IN (SELECT DISTINCT "CustomerId" FROM "Rentals")
                                          """;

    protected override Task SqlSubject() =>
        NpgsqlConnection.ExecuteAsync(SqlQuery, new { InactivityThreshold });
}