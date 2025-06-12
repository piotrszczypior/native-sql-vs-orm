using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectOverdueRentals : QueryBenchmark<SelectOverdueRentals, RentalEntity>
{
    protected override IQueryable<RentalEntity> OrmQuery => DbContext.Rentals
        .Where(r => r.RentalEnd == null && 
                    (r.RentalStart - DateTime.UtcNow).Days > 
                    r.Inventory.Film.RentalDuration)
        .Include(r => r.Customer)
        .Include(r => r.Inventory)
        .ThenInclude(i => i.Film)
        .OrderBy(r => r.RentalStart);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT r.*, c."FirstName", c."LastName", f."Title", f."RentalDuration"
                                          FROM "Rentals" r
                                          JOIN "Customers" c ON r."CustomerId" = c."Id"
                                          JOIN "Inventories" i ON r."InventoryId" = i."Id"
                                          JOIN "Films" f ON i."FilmId" = f."Id"
                                          WHERE r."RentalEnd" IS NULL
                                            AND EXTRACT(DAY FROM (NOW() - r."RentalStart")) > f."RentalDuration"
                                          ORDER BY r."RentalStart"
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<RentalEntity>(SqlQuery);
    }
}