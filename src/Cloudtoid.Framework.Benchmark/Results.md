# Performance Benchmark Analysis

## ReadOnlyValueList

**Conclusion**

Use `ReadOnlyValueList<T>` when you know in the majority of cases you will have at most 1 item in the list.

**Result**

Date updated: 8/24/2020

|                                                            Method |      Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------------------ |----------:|----------:|----------:|-------:|------:|------:|----------:|
|                         'Creation of list with 1 value-type item' | 13.831 ns | 0.3659 ns | 0.5803 ns | 0.0122 |     - |     - |      64 B |
| 'Creation of value list with 1 value-type item - requires boxing' |  4.045 ns | 0.1176 ns | 0.2929 ns | 0.0046 |     - |     - |      24 B |

|                                        Method |       Mean |     Error |    StdDev | Ratio |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------------- |-----------:|----------:|----------:|------:|-------:|------:|------:|----------:|
|       'Creation of list with 1 ref-type item' | 18.3459 ns | 0.4532 ns | 1.2931 ns | 1.000 | 0.0122 |     - |     - |      64 B |
| 'Creation of value list with 1 ref-type item' |  0.0000 ns | 0.0000 ns | 0.0000 ns | 0.000 |      - |     - |     - |         - |

|                                           Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|       'Creation of list with 2 value-type items' | 15.723 ns | 0.4002 ns | 0.8700 ns |  1.00 |    0.00 | 0.0122 |     - |     - |      64 B |
| 'Creation of value list with 2 value-type items' |  4.986 ns | 0.1359 ns | 0.3604 ns |  0.31 |    0.03 | 0.0061 |     - |     - |      32 B |

|                                         Method |     Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------------------------- |---------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|       'Creation of list with 2 ref-type items' | 20.98 ns | 0.507 ns | 0.623 ns |  1.00 |    0.00 | 0.0138 |     - |     - |      72 B |
| 'Creation of value list with 2 ref-type items' | 10.35 ns | 0.242 ns | 0.551 ns |  0.49 |    0.03 | 0.0076 |     - |     - |      40 B |

