using System.Diagnostics;
using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var scenariosAssembly = Assembly.GetExecutingAssembly();

var config = Debugger.IsAttached ? new DebugInProcessConfig() : null;

BenchmarkSwitcher
    .FromAssembly(scenariosAssembly)
    .RunAll(config);