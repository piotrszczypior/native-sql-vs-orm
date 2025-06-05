using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectInventoryByStore : QueryBenchmark<SelectInventoryByStore, InventoryEntity>
{
    private const int StoreId = 1;

    protected override IQueryable<InventoryEntity> OrmQuery => DbContext.Inventories
        .Where(i => i.StoreId == StoreId);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT *
                                          FROM "Inventories"
                                          WHERE "StoreId" = @StoreId
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<InventoryEntity>(SqlQuery, new { StoreId });
    }
}