using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectTopRentedFilms : QueryBenchmark<SelectTopRentedFilms, FilmEntity>
{
    private const int TopCount = 10;

    protected override IQueryable<FilmEntity> OrmQuery => DbContext.Films
        .Select(f => new
        {
            Film = f,
            RentalCount = f.Inventories.SelectMany(i => i.Rentals).Count()
        })
        .OrderByDescending(x => x.RentalCount)
        .Take(TopCount)
        .Select(x => x.Film);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT f.*, COUNT(r."Id") as RentalCount
                                          FROM "Films" f
                                          JOIN "Inventories" i ON f."Id" = i."FilmId"
                                          JOIN "Rentals" r ON i."Id" = r."InventoryId"
                                          GROUP BY f."Id"
                                          ORDER BY COUNT(r."Id") DESC
                                          LIMIT @TopCount
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery, new { TopCount });
    }
}