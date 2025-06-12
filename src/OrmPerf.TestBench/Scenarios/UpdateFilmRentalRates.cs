using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class UpdateFilmRentalRates : ScalarBenchmark<UpdateFilmRentalRates>
{
    private const decimal RateIncrease = 0.50m;

    protected override string OrmHookVerb => "UPDATE";

    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var films = await DbContext.Films
            .Where(f => f.RentalRate < 5.00m)
            .ToListAsync();

        foreach (var film in films)
        {
            film.RentalRate += RateIncrease;
            film.LastUpdate = DateTime.UtcNow;
        }

        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          UPDATE "Films"
                                          SET "RentalRate" = "RentalRate" + @RateIncrease,
                                              "LastUpdate" = @LastUpdate
                                          WHERE "RentalRate" < 5.00
                                          """;

    protected override Task SqlSubject() =>
        NpgsqlConnection.ExecuteAsync(SqlQuery, new { RateIncrease, LastUpdate = DateTime.UtcNow });
}