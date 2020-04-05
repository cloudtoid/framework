# Cloudtoid Framework

Welcome to Cloudtoid's Framework project, which is a library of utility classes, targetting `.netstandard 2.1` used by all Cloudtoid applications.

The latest NuGet package can be found [here](https://www.nuget.org/packages/Cloudtoid.Framework/).

## Features

- A common set of code analysis settings.
- Banning a set of unsafe .NET APIs and providing alternatives to those APIs.
- A set of concurrency related extensions.
  - Execution of actions within a specified timeout: `Async.WithTimeout`
  - Automatic tracing if a task becomes faulted: `Async.TraceOnFaulted`
  - Ability to fire and forget a task but log its failure: `Async.FireAndForget`
  - Ability to create a `Task` that is completed if a `CancellationToken` is canceled: `Async.WhenCancelled`
- `AsyncLazy<T>` provides support for asynchronous lazy initialization.
- A set of code contracts to validate arguments and other states. See the `Contracts` static class.
- A set of collection extensions on `ICollection<T>`, `IReadOnlyCollection<T>`, `IList<T>`, `List<T>` and `T[]`.
- A set of extensions on `IEnumerable<T>`.
- A set of extensions on `Exception` to:
  - differentiate between a fatal extension and a non-fatal one
  - Whether a task was timed out or canceled.
- A set of extensions on `Microsoft.Extensions.Configuration.IConfiguration`.
- A set of extensions on `IServiceCollection`.
- A set of extensions on `StringBuilder`.
- A set of constant providers used for mocking:
  - `IGuidProvider` that can produce a stable `Guid` when needed.
  - `IDateTimeProvider` and `IDateTimeOffsetProvider` that can produce stable `DateTime` and `DateTimeOffset` when needed.
- A set of utility methods to help with `hashcode` creation and manipulation. See `HashUtil`.
- A set of utility methods to help with `hashcode` creation and manipulation. See `HashUtil`.
- A set of utility methods for `string` manipulation. See `StringUtil` and `ToStringExtensions`.
- A set of utility methods for HTTP related needs. See `HttpVersion` for HTTP protocol versioning, `HttpMethod` for codifying HTTP methods, and `HttpHeader` for validating HTTP header names.
