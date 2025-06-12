using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectInventoryAvailability : QueryBenchmark<SelectInventoryAvailability, InventoryEntity>
{
    protected override IQueryable<InventoryEntity> OrmQuery => DbContext.Inventories
        .Where(i => !i.Rentals.Any(r => r.RentalEnd == null))
        .Include(i => i.Film)
        .Include(i => i.Store);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT i.*
                                          FROM "Inventories" i
                                          WHERE NOT EXISTS (
                                              SELECT 1 FROM "Rentals" r
                                              WHERE r."InventoryId" = i."Id"
                                              AND r."RentalEnd" IS NULL
                                          )
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<InventoryEntity>(SqlQuery);
    }
}