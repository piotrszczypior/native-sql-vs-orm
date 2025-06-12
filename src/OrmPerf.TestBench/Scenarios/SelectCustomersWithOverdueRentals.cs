using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectCustomersWithOverdueRentals : QueryBenchmark<SelectCustomersWithOverdueRentals, CustomerEntity>
{
    private static readonly DateTime OverdueThreshold = DateTime.UtcNow.AddDays(-7);

    protected override IQueryable<CustomerEntity> OrmQuery => DbContext.Customers
        .Where(c => c.Rentals.Any(r => r.RentalEnd == null && r.RentalStart < OverdueThreshold))
        .Include(c => c.Rentals);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT DISTINCT c.*
                                          FROM "Customers" c
                                          JOIN "Rentals" r ON c."Id" = r."CustomerId"
                                          WHERE r."RentalEnd" IS NULL
                                          AND r."RentalStart" < @OverdueThreshold
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<CustomerEntity>(SqlQuery, new { OverdueThreshold });
    }
}