using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectActorsByFilmCount : QueryBenchmark<SelectActorsByFilmCount, ActorEntity>
{
    private const int MinFilmCount = 10;

    protected override IQueryable<ActorEntity> OrmQuery => DbContext.Actors
        .Where(a => a.Films.Count >= MinFilmCount)
        .OrderByDescending(a => a.Films.Count)
        .Include(a => a.Films);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT a.*, COUNT(fa."FilmId") as FilmCount
                                          FROM "Actors" a
                                          JOIN "FilmsActors" fa ON a."Id" = fa."ActorId"
                                          GROUP BY a."Id"
                                          HAVING COUNT(fa."FilmId") >= @MinFilmCount
                                          ORDER BY COUNT(fa."FilmId") DESC
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<ActorEntity>(SqlQuery, new { MinFilmCount });
    }
}