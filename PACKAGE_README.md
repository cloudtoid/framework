# Cloudtoid.Framework

Utilities for .NET applications and libraries: asynchronous initialization, cancellation and task helpers, argument validation, collection extensions, and configuration helpers.

## Install

Requires .NET 10 or later.

```sh
dotnet add package Cloudtoid.Framework
```

## Example: initialize a value on first use

```csharp
using Cloudtoid;

var settings = new AsyncLazy<string>(
    () => File.ReadAllTextAsync("settings.json"));

string contents = await settings;
```

`AsyncLazy<T>` starts initialization when the value is first requested and shares the resulting task with subsequent callers.

## Included utilities

- Task timeout, cancellation, and fault-logging helpers.
- `Contract` argument and state checks.
- `ReadOnlyValueList<T>` and collection extensions.
- String, path, HTTP, and hash helpers.
- Configuration and dependency-injection extensions.
- Date/time and GUID provider interfaces for tests.

The package also includes build configuration for Cloudtoid's banned API list.

[API source and examples](https://github.com/cloudtoid/framework) · [Report an issue](https://github.com/cloudtoid/framework/issues) · [MIT license](https://github.com/cloudtoid/framework/blob/master/LICENSE)
