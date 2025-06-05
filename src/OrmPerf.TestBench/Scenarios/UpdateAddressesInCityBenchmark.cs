using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class UpdateAddressesInCityBenchmark : ScalarBenchmark<UpdateAddressesInCityBenchmark>
{
    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var addresses = await DbContext.Addresses
            .Where(a => a.City.City == "Warsaw")
            .ToListAsync();

        foreach (var addr in addresses)
        {
            addr.District = "Updated District";
        }

        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          UPDATE "Addresses"
                                          SET "District" = 'Updated District'
                                          WHERE "CityId" IN (
                                              SELECT "Id" FROM "Cities" WHERE "City" = 'Warsaw'
                                          )
                                          """;

    protected override Task SqlSubject() => NpgsqlConnection.ExecuteAsync(SqlQuery);
}