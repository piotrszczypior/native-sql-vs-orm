using Dapper;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class BulkInsertPayments : ScalarBenchmark<BulkInsertPayments>
{
    private const int PaymentCount = 10;

    protected override string OrmHookVerb => "INSERT INTO";

    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var now = DateTime.UtcNow;
        var payments = Enumerable.Range(0, PaymentCount)
            .Select(i => new PaymentEntity
            {
                CustomerId = i,
                StaffId = 1,
                RentalId = 1,
                Amount = 4.99m + (i * 0.01m),
                PaymentDate = now.AddMinutes(-i)
            }).ToList();

        DbContext.Payments.AddRange(payments);
        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          INSERT INTO "Payments" ("CustomerId", "StaffId", "RentalId", "Amount", "PaymentDate")
                                          VALUES (@CustomerId, @StaffId, @RentalId, @Amount, @PaymentDate)
                                          """;

    protected override async Task SqlSubject()
    {
        var now = DateTime.UtcNow;
        var payments = Enumerable.Range(0, PaymentCount)
            .Select(i => new
            {
                CustomerId = i,
                StaffId = 1,
                RentalId = 1,
                Amount = 4.99m + (i * 0.01m),
                PaymentDate = now.AddMinutes(-i)
            });

        await NpgsqlConnection.ExecuteAsync(SqlQuery, payments);
    }
}