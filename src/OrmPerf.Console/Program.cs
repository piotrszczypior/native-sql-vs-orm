using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var scenariosAssembly = Assembly.GetExecutingAssembly();
BenchmarkSwitcher
    .FromAssembly(scenariosAssembly)
    .RunAll(new DebugInProcessConfig());