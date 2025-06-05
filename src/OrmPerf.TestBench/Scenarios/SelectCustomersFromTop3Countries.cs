using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectCustomersFromTop3Countries : QueryBenchmark<SelectCustomersFromTop3Countries, CustomerEntity>
{
    protected override IQueryable<CustomerEntity> OrmQuery => DbContext.Customers
        .Where(c => DbContext.Customers
            .GroupBy(c2 => c2.Address.City.CountryId)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => g.Key)
            .Contains(c.Address.City.CountryId));

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT cu.*
                                          FROM "Customers" cu
                                          JOIN "Addresses" a ON cu."AddressId" = a."Id"
                                          JOIN "Cities" ci ON a."CityId" = ci."Id"
                                          WHERE ci."CountryId" IN (
                                              SELECT "CountryId"
                                              FROM (
                                                  SELECT ci."CountryId", COUNT(*) AS CustomerCount
                                                  FROM "Customers" cu
                                                  JOIN "Addresses" a ON cu."AddressId" = a."Id"
                                                  JOIN "Cities" ci ON a."CityId" = ci."Id"
                                                  GROUP BY ci."CountryId"
                                                  ORDER BY COUNT(*) DESC
                                                  LIMIT 3
                                              ) AS TopCountries
                                          )
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<CustomerEntity>(SqlQuery);
    }
}