using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectActionFilms : QueryBenchmark<SelectActionFilms, FilmEntity>
{
    protected override IQueryable<FilmEntity> OrmQuery => DbContext.Films
        .Where(f => f.Categories.Any(fc => fc.Category.Name == "Action"));

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT DISTINCT f.*
                                          FROM "Films" f
                                          JOIN "FilmsCategories" fc ON f."Id" = fc."FilmId"
                                          JOIN "Categories" c ON fc."CategoryId" = c."Id"
                                          WHERE c."Name" = 'Action'
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery);
    }
}
