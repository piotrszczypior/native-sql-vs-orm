using System.Diagnostics;
using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Running;
using OrmPerf.TestBench;

var scenariosAssembly = Assembly.GetExecutingAssembly();

var config = Debugger.IsAttached ? new DebugInProcessConfig() : DefaultConfig.Instance;

config.AddExporter(JsonExporter.Full);

BenchmarkSwitcher
    .FromAssembly(scenariosAssembly)
    .Run(args, config);

BenchmarkMetadataStore.Export(config);