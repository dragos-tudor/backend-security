```

BenchmarkDotNet v0.13.12, Debian GNU/Linux 12 (bookworm) (container)
Intel Core i5-4690 CPU 3.50GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
.NET SDK 8.0.303
  [Host]     : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2
  Job-XYOFWI : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2
  Job-AGDKZZ : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2
  Job-ZUSNON : .NET 8.0.7 (8.0.724.31311), X64 RyuJIT AVX2


```
| Method    | Job        | InvocationCount | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|---------- |----------- |---------------- |----------:|----------:|----------:|----------:|------:|--------:|--------:|--------:|--------:|----------:|------------:|
| FPSignin  | Job-XYOFWI | 1024            |  30.97 μs |  1.672 μs |  4.576 μs |  28.81 μs |  1.00 |    0.00 |  1.9531 |       - |       - |   7.98 KB |        1.00 |
| OOPSignin | Job-XYOFWI | 1024            | 187.71 μs | 28.649 μs | 84.472 μs | 206.00 μs |  5.80 |    2.89 | 14.6484 | 13.6719 | 13.6719 | 915.67 KB |      114.77 |
|           |            |                 |           |           |           |           |       |         |         |         |         |           |             |
| FPSignin  | Job-AGDKZZ | 128             |  75.27 μs |  1.505 μs |  3.397 μs |  75.97 μs |  1.00 |    0.00 |       - |       - |       - |   7.99 KB |        1.00 |
| OOPSignin | Job-AGDKZZ | 128             |  88.43 μs |  3.488 μs | 10.119 μs |  88.96 μs |  1.14 |    0.12 |  7.8125 |  7.8125 |  7.8125 | 116.21 KB |       14.55 |
|           |            |                 |           |           |           |           |       |         |         |         |         |           |             |
| FPSignin  | Job-ZUSNON | 512             |  49.77 μs |  5.119 μs | 15.094 μs |  44.99 μs |  1.00 |    0.00 |  1.9531 |       - |       - |   7.98 KB |        1.00 |
| OOPSignin | Job-ZUSNON | 512             |  99.94 μs |  7.590 μs | 22.260 μs |  90.24 μs |  2.31 |    1.17 |  9.7656 |  9.7656 |  9.7656 | 461.68 KB |       57.85 |
