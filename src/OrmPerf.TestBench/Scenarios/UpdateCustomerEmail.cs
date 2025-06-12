using Dapper;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class UpdateCustomerEmail : ScalarBenchmark<UpdateCustomerEmail>
{
    private const int CustomerId = 1;
    private readonly string _newEmail = $"updated_{DateTime.UtcNow.Ticks}@example.com";

    protected override Func<Task<int>> OrmExecuteFactory => async () =>
    {
        var customer = await DbContext.Customers.FindAsync(CustomerId);
        if (customer != null)
        {
            customer.Email = _newEmail;
            customer.LastUpdate = DateTime.UtcNow;
        }
        return await DbContext.SaveChangesAsync();
    };

    protected override Task OrmSubject() => OrmExecuteFactory();

    protected override string SqlQuery => """
                                          UPDATE "Customers"
                                          SET "Email" = @Email, "LastUpdate" = @LastUpdate
                                          WHERE "Id" = @CustomerId
                                          """;

    protected override async Task SqlSubject()
    {
        await NpgsqlConnection.ExecuteAsync(SqlQuery, new 
        { 
            Email = _newEmail, 
            LastUpdate = DateTime.UtcNow, 
            CustomerId 
        });
    }
}