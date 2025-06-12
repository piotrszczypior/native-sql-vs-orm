using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectActiveCustomersByStore : QueryBenchmark<SelectActiveCustomersByStore, CustomerEntity>
{
    private const int StoreId = 1;

    protected override IQueryable<CustomerEntity> OrmQuery => DbContext.Customers
        .Where(c => c.StoreId == StoreId && c.IsActive)
        .Include(c => c.Address)
        .ThenInclude(a => a.City)
        .ThenInclude(city => city.Country)
        .OrderBy(c => c.LastName)
        .ThenBy(c => c.FirstName);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT c.*, a."Address", city."City", country."Country"
                                          FROM "Customers" c
                                          JOIN "Addresses" a ON c."AddressId" = a."Id"
                                          JOIN "Cities" city ON a."CityId" = city."Id"
                                          JOIN "Countries" country ON city."CountryId" = country."Id"
                                          WHERE c."StoreId" = @StoreId AND c."IsActive" = true
                                          ORDER BY c."LastName", c."FirstName"
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<CustomerEntity>(SqlQuery, new { StoreId });
    }
}