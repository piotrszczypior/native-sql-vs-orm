using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectFilmsByCategory : QueryBenchmark<SelectFilmsByCategory, FilmEntity>
{
    private const string CategoryName = "Action";

    protected override IQueryable<FilmEntity> OrmQuery => DbContext.Films
        .Where(f => f.Categories.Any(fc => fc.Category.Name == CategoryName))
        .Include(f => f.Language)
        .Include(f => f.Categories)
        .ThenInclude(fc => fc.Category);

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT f.*, l."Name" as LanguageName, c."Name" as CategoryName
                                          FROM "Films" f
                                          JOIN "FilmCategories" fc ON f."Id" = fc."FilmId"
                                          JOIN "Categories" c ON fc."CategoryId" = c."Id"
                                          JOIN "Languages" l ON f."LanguageId" = l."Id"
                                          WHERE c."Name" = @CategoryName
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<FilmEntity>(SqlQuery, new { CategoryName });
    }
}