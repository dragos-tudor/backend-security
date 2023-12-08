
using BenchmarkDotNet.Running;
using Security.Benchmarks;

// BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
BenchmarkRunner.Run<SignInBenchmarks>();


