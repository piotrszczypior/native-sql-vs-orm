using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectFilmsByRentalRate : QueryBenchmark<SelectFilmsByRentalRate, FilmEntity>
{
    private const decimal MinRentalRate = 2.99m;
    private const decimal MaxRentalRate = 4.99m;

    protected override IQueryable<FilmEntity> OrmQuery => DbContext.Films
        .Where(f => f.RentalRate >= MinRentalRate && f.RentalRate <= MaxRentalRate)
        .Include(f => f.Language)
        .OrderBy(f => f.RentalRate)
        .ThenBy(f => f.Title);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT f.*, l."Name" as LanguageName
                                          FROM "Films" f
                                          JOIN "Languages" l ON f."LanguageId" = l."Id"
                                          WHERE f."RentalRate" >= @MinRentalRate
                                            AND f."RentalRate" <= @MaxRentalRate
                                          ORDER BY f."RentalRate", f."Title"
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery, 
            new { MinRentalRate, MaxRentalRate });
    }
}