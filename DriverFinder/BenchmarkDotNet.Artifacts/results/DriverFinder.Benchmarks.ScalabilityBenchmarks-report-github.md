```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26100.7171/24H2/2024Update/HudsonValley)
AMD Ryzen 5 6600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.204
  [Host]     : .NET 6.0.36 (6.0.36, 6.0.3624.51421), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 6.0.36 (6.0.36, 6.0.3624.51421), X64 RyuJIT x86-64-v3


```
| Method                      | DriverCount | Mean       | Error     | StdDev     | Gen0    | Gen1    | Gen2    | Allocated |
|---------------------------- |------------ |-----------:|----------:|-----------:|--------:|--------:|--------:|----------:|
| **BruteForce_Scalability**      | **100**         |   **1.425 μs** | **0.0089 μs** |  **0.0083 μs** |  **0.0191** |       **-** |       **-** |    **2040 B** |
| PriorityQueue_Scalability   | 100         |   2.061 μs | 0.0171 μs |  0.0160 μs |  0.0076 |       - |       - |     960 B |
| QuickSelect_Scalability     | 100         |   1.268 μs | 0.0072 μs |  0.0064 μs |  0.0134 |       - |       - |    1536 B |
| RadiusExpansion_Scalability | 100         |  17.114 μs | 0.0984 μs |  0.0920 μs |  0.0305 |       - |       - |    3640 B |
| **BruteForce_Scalability**      | **1000**        |   **8.812 μs** | **0.0394 μs** |  **0.0368 μs** |  **0.1526** |       **-** |       **-** |   **16440 B** |
| PriorityQueue_Scalability   | 1000        |  10.119 μs | 0.0685 μs |  0.0607 μs |       - |       - |       - |     960 B |
| QuickSelect_Scalability     | 1000        |   2.668 μs | 0.0097 μs |  0.0090 μs |  0.0801 |       - |       - |    8736 B |
| RadiusExpansion_Scalability | 1000        |  32.494 μs | 0.1671 μs |  0.1563 μs |  0.2441 |       - |       - |   31528 B |
| **BruteForce_Scalability**      | **10000**       | **151.560 μs** | **0.9914 μs** |  **0.8789 μs** |  **1.4648** |       **-** |       **-** |  **160440 B** |
| PriorityQueue_Scalability   | 10000       |  80.991 μs | 1.6154 μs |  1.3490 μs |       - |       - |       - |     960 B |
| QuickSelect_Scalability     | 10000       |  45.024 μs | 0.4966 μs |  0.4147 μs |  0.7935 |       - |       - |   80736 B |
| RadiusExpansion_Scalability | 10000       | 493.369 μs | 9.7760 μs | 15.5058 μs | 10.7422 | 10.7422 | 10.7422 |  283598 B |
