using Dapper;
using Docker.DotNet.Models;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class InsertActorBenchmark : ScalarBenchmark<InsertActorBenchmark>
{
    protected override Func<Task<int>> OrmExecuteFactory => () =>
    {
        DbContext.Actors.Add(BuildActor());
        return DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject()
    {
        return OrmExecuteFactory();
    }

    protected override string SqlQuery =>
        """
        INSERT INTO "Actors" ("FirstName", "LastName", "LastUpdate") VALUES (@FirstName, @LastName, @LastUpdate)
        """;

    protected override Task SqlSubject()
    {
        var actor = BuildActor();

        return NpgsqlConnection.ExecuteAsync(SqlQuery, new
        {
            actor.FirstName,
            actor.LastName,
            actor.LastUpdate
        });
    }

    private static ActorEntity BuildActor()
    {
        return new ActorEntity
        {
            FirstName = "John",
            LastName = "Doe",
            LastUpdate = DateTime.UtcNow
        };
    }
}