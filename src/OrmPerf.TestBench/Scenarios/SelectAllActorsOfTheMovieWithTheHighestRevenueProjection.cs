using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectAllActorsOfTheMovieWithTheHighestRevenueProjection : QueryBenchmark<SelectAllActorsOfTheMovieWithTheHighestRevenueProjection, ActorEntity>
{
    protected override IQueryable<ActorEntity> OrmQuery => DbContext.Films
        .OrderByDescending(f => f.RevenueProjection)
        .SelectMany(f => f.Actors)
        .Select(fa => fa.Actor);
    protected override async Task OrmSubject()
    {
        var actors = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT *
                                          FROM "Actors"
                                          WHERE "Actors"."Id" IN (
                                              SELECT "FilmsActors"."ActorId"
                                              FROM "FilmsActors"
                                              WHERE "FilmsActors"."FilmId" IN (
                                                  SELECT "MaxFilm"."Id"
                                                  FROM (
                                                      SELECT "Films"."Id", MAX("Films"."RevenueProjection")
                                                      FROM "Films"
                                                  ) "MaxFilm"
                                              )
                                          )
                                          """;
    protected override async Task SqlSubject()
    {
        var actors = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery);
    }
}