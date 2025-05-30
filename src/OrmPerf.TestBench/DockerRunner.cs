using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;

namespace OrmPerf.TestBench;

public class DockerRunner
{
    public static async Task RunDockerPostgres()
    {
        var postgres = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("postgres")
            .WithName("postgres")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        await postgres.StartAsync();
    }
}