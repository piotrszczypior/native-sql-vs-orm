using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectFilmsTranslatedToPolish : QueryBenchmark<SelectFilmsTranslatedToPolish, FilmEntity>
{
    protected override IQueryable<FilmEntity> OrmQuery => OrmQuerySubject
        .Where(f => f.Language.Name == "Polish");
    protected override async Task OrmSubject()
    {
        var films = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT *
                                          FROM "Films"
                                          JOIN "Languages"
                                          ON "Films"."LanguageId" = "Languages"."Id"
                                          WHERE "Languages"."Name" = 'Polish'
                                          """;
    protected override async Task SqlSubject()
    {
        var films = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery);
    }
}