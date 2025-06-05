using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectTop5MostRentedFilms : QueryBenchmark<SelectTop5MostRentedFilms, FilmEntity>
{
    protected override IQueryable<FilmEntity> OrmQuery => DbContext.Films
        .OrderByDescending(f => f.Inventories.SelectMany(i => i.Rentals).Count())
        .Take(5);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT f.*
                                          FROM "Films" f
                                          JOIN "Inventories" i ON f."Id" = i."FilmId"
                                          JOIN "Rentals" r ON i."Id" = r."InventoryId"
                                          GROUP BY f."Id"
                                          ORDER BY COUNT(r."Id") DESC
                                          LIMIT 5
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery);
    }
}
