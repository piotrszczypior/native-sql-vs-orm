using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectAllFilmsQueryBenchmark : QueryBenchmark<SelectAllFilmsQueryBenchmark, FilmEntity>
{
    protected override IQueryable<FilmEntity> OrmQuery => OrmQuerySubject.AsQueryable();
    protected override async Task OrmSubject()
    {
        var films = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT *
                                          FROM "Films"
                                          """;
    protected override async Task SqlSubject()
    {
        var films = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery);
    }
}