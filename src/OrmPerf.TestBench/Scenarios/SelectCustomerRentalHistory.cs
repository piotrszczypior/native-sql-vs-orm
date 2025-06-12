using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectCustomerRentalHistory : QueryBenchmark<SelectCustomerRentalHistory, RentalEntity>
{
    private const int CustomerId = 1;

    protected override IQueryable<RentalEntity> OrmQuery => DbContext.Rentals
        .Where(r => r.CustomerId == CustomerId)
        .Include(r => r.Inventory)
        .ThenInclude(i => i.Film)
        .Include(r => r.Staff)
        .OrderByDescending(r => r.RentalStart);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT r.*, f."Title", s."FirstName", s."LastName"
                                          FROM "Rentals" r
                                          JOIN "Inventories" i ON r."InventoryId" = i."Id"
                                          JOIN "Films" f ON i."FilmId" = f."Id"
                                          JOIN "Staff" s ON r."StaffId" = s."Id"
                                          WHERE r."CustomerId" = @CustomerId
                                          ORDER BY r."RentalStart" DESC
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<RentalEntity>(SqlQuery, new { CustomerId });
    }
}