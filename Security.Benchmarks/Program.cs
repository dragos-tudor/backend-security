
using BenchmarkDotNet.Running;

namespace Security.Benchmarks;

public class Program
{
  public static void Main(string[] args)
  {
    // BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    BenchmarkRunner.Run<SignInBenchmarks>();
  }
}


