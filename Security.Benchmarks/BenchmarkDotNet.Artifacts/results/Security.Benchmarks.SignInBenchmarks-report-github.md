```

BenchmarkDotNet v0.13.12, Debian GNU/Linux 12 (bookworm) (container)
Intel Core i5-4690 CPU 3.50GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
.NET SDK 8.0.302
  [Host]     : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2
  Job-JMLZRO : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2
  Job-KBUFUR : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2
  Job-WWYEXG : .NET 8.0.6 (8.0.624.26715), X64 RyuJIT AVX2


```
| Method    | Job        | InvocationCount | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|---------- |----------- |---------------- |----------:|----------:|----------:|----------:|------:|--------:|--------:|--------:|--------:|----------:|------------:|
| FPSignin  | Job-JMLZRO | 1024            |  28.69 μs |  1.639 μs |  4.514 μs |  26.31 μs |  1.00 |    0.00 |  1.9531 |       - |       - |   7.96 KB |        1.00 |
| OOPSignin | Job-JMLZRO | 1024            | 164.17 μs | 26.165 μs | 77.147 μs | 175.64 μs |  5.47 |    2.84 | 11.7188 | 10.7422 | 10.7422 | 867.66 KB |      109.07 |
|           |            |                 |           |           |           |           |       |         |         |         |         |           |             |
| FPSignin  | Job-KBUFUR | 128             |  69.48 μs |  1.360 μs |  2.685 μs |  69.38 μs |  1.00 |    0.00 |       - |       - |       - |   7.96 KB |        1.00 |
| OOPSignin | Job-KBUFUR | 128             |  81.56 μs |  3.306 μs |  9.747 μs |  81.40 μs |  1.14 |    0.15 |  7.8125 |  7.8125 |  7.8125 |  115.2 KB |       14.47 |
|           |            |                 |           |           |           |           |       |         |         |         |         |           |             |
| FPSignin  | Job-WWYEXG | 512             |  47.65 μs |  5.102 μs | 15.043 μs |  43.04 μs |  1.00 |    0.00 |  1.9531 |       - |       - |   7.96 KB |        1.00 |
| OOPSignin | Job-WWYEXG | 512             |  89.09 μs |  6.441 μs | 18.892 μs |  82.94 μs |  2.17 |    1.12 |  9.7656 |  9.7656 |  9.7656 | 449.66 KB |       56.51 |
