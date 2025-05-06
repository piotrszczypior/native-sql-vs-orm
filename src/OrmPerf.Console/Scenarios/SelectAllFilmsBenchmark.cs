using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;

namespace OrmPerf.Console.Scenarios;

public class SelectAllFilmsBenchmark : Benchmark<SelectAllFilmsBenchmark, FilmEntity>
{
    protected override IQueryable<FilmEntity> OrmQuery => OrmQuerySubject.AsQueryable();
    
    protected override async Task OrmSubject()
    {
        var films = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT *
                                          FROM "Films";
                                          """;
    protected override async Task SqlSubject()
    {
        var films = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery);
    }
}