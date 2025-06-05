using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectRecentRentingCustomers : QueryBenchmark<SelectRecentRentingCustomers, CustomerEntity>
{
    private static readonly DateTime DateThreshold = DateTime.UtcNow.AddDays(-30);

    protected override IQueryable<CustomerEntity> OrmQuery => DbContext.Customers
        .Where(c => c.Rentals.Any(r => r.RentalStart >= DateThreshold));

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT DISTINCT cu.*
                                          FROM "Customers" cu
                                          JOIN "Rentals" r ON cu."Id" = r."CustomerId"
                                          WHERE r."RentalStart" >= @DateThreshold
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<CustomerEntity>(SqlQuery, new { DateThreshold });
    }
}