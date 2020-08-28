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

|                       Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |---------:|---------:|---------:|------:|--------:|------:|------:|------:|----------:|
|                'Pass a list' | 14.82 us | 0.280 us | 0.393 us |  1.00 |    0.00 |     - |     - |     - |         - |
| 'Pass a value list by value' | 18.05 us | 0.320 us | 0.449 us |  1.22 |    0.05 |     - |     - |     - |         - |
|   'Pass a value list by ref' | 36.41 us | 0.718 us | 1.257 us |  2.46 |    0.12 |     - |     - |     - |         - |

|                                                       Method |     Mean |    Error |   StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------------- |---------:|---------:|---------:|------:|--------:|------:|------:|------:|----------:|
|       'Enumerate a list of value-type items (100,000 items)' | 236.4 us |  4.24 us |  3.76 us |  1.00 |    0.00 |     - |     - |     - |         - |
| 'Enumerate a value list of value-type items (100,000 items)' | 763.5 us | 15.18 us | 20.27 us |  3.25 |    0.09 |     - |     - |     - |         - |

|                                                     Method |       Mean |    Error |   StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------------------------------------- |-----------:|---------:|---------:|------:|--------:|------:|------:|------:|----------:|
|       'Enumerate a list of ref-type items (100,000 items)' |   432.0 us |  8.59 us | 15.72 us |  1.00 |    0.00 |     - |     - |     - |         - |
| 'Enumerate a value list of ref-type items (100,000 items)' | 1,152.5 us | 22.80 us | 37.46 us |  2.67 |    0.13 |     - |     - |     - |         - |

