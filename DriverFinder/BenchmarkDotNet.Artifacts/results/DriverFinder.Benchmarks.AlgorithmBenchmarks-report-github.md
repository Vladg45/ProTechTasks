```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26100.7171/24H2/2024Update/HudsonValley)
AMD Ryzen 5 6600H with Radeon Graphics 3.30GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.204
  [Host]     : .NET 6.0.36 (6.0.36, 6.0.3624.51421), X64 RyuJIT x86-64-v3
  Job-WRIKEU : .NET 6.0.36 (6.0.36, 6.0.3624.51421), X64 RyuJIT x86-64-v3

IterationCount=5  LaunchCount=1  WarmupCount=2  

```
| Method                 | Count | Mean      | Error     | StdDev    | Gen0   | Allocated |
|----------------------- |------ |----------:|----------:|----------:|-------:|----------:|
| **BruteForceNearest**      | **5**     |  **8.639 μs** | **0.4662 μs** | **0.0721 μs** | **0.1526** |   **16400 B** |
| PriorityQueueNearest   | 5     |  8.173 μs | 0.1830 μs | 0.0475 μs |      - |     528 B |
| QuickSelectNearest     | 5     |  2.750 μs | 0.0442 μs | 0.0115 μs | 0.0801 |    8624 B |
| RadiusExpansionNearest | 5     | 31.109 μs | 0.8052 μs | 0.2091 μs | 0.2441 |   31336 B |
| **BruteForceNearest**      | **10**    |  **8.761 μs** | **0.0924 μs** | **0.0240 μs** | **0.1526** |   **16440 B** |
| PriorityQueueNearest   | 10    |  9.646 μs | 0.1389 μs | 0.0361 μs |      - |     960 B |
| QuickSelectNearest     | 10    |  2.673 μs | 0.0559 μs | 0.0145 μs | 0.0801 |    8736 B |
| RadiusExpansionNearest | 10    | 32.710 μs | 0.7013 μs | 0.1821 μs | 0.2441 |   31528 B |
| **BruteForceNearest**      | **20**    |  **9.537 μs** | **0.1221 μs** | **0.0317 μs** | **0.1526** |   **16520 B** |
| PriorityQueueNearest   | 20    | 12.365 μs | 0.6200 μs | 0.0959 μs | 0.0153 |    1776 B |
| QuickSelectNearest     | 20    |  3.173 μs | 0.0847 μs | 0.0220 μs | 0.0839 |    8976 B |
| RadiusExpansionNearest | 20    | 33.256 μs | 1.2932 μs | 0.3358 μs | 0.2441 |   31888 B |
