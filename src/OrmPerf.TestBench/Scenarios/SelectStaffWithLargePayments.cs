using Dapper;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using OrmPerf.TestBench.Scenarios.Abstract;

namespace OrmPerf.TestBench.Scenarios;

public class SelectStaffWithLargePayments : QueryBenchmark<SelectStaffWithLargePayments, StaffEntity>
{
    protected override IQueryable<StaffEntity> OrmQuery => DbContext.Staff
        .Where(s => s.Payments.Any(p => p.Amount > 100));

    protected override async Task OrmSubject()
    {
        var result = await OrmQuery.ToListAsync();
    }

    protected override string SqlQuery => """
                                          SELECT DISTINCT s.*
                                          FROM "Staff" s
                                          JOIN "Payments" p ON s."Id" = p."StaffId"
                                          WHERE p."Amount" > 100
                                          """;

    protected override async Task SqlSubject()
    {
        var result = await NpgsqlConnection.QueryAsync<StaffEntity>(SqlQuery);
    }
}