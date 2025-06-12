using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectFilmsWithLongRentalDuration : QueryBenchmark<SelectFilmsWithLongRentalDuration, FilmEntity>
{
    private const int MinRentalDuration = 6;

    protected override IQueryable<FilmEntity> OrmQuery => DbContext.Films
        .Where(f => f.RentalDuration >= MinRentalDuration)
        .OrderBy(f => f.Title)
        .Include(f => f.Language);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT f.*
                                          FROM "Films" f
                                          WHERE f."RentalDuration" >= @MinRentalDuration
                                          ORDER BY f."Title"
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery, new { MinRentalDuration });
    }
}