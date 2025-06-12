using System.Diagnostics;
using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Running;
using OrmPerf.TestBench;
using OrmPerf.TestBench.Scenarios.Abstract;

var scenariosAssembly = Assembly.GetExecutingAssembly();

var config = Debugger.IsAttached ? new DebugInProcessConfig() : DefaultConfig.Instance;

config.AddExporter(JsonExporter.Full);

Benchmark.ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword;Database=postgres;";

BenchmarkSwitcher
    .FromAssembly(scenariosAssembly)
    .Run(args, config);

BenchmarkMetadataStore.Export(config);