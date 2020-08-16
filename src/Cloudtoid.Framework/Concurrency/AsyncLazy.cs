using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudtoid
{
    /// <summary>
    /// Provides support for asynchronous lazy initialization.
    /// </summary>
    /// <remarks>
    /// Based on Stephen Toub's article:
    /// http://blogs.msdn.com/b/pfxteam/archive/2011/01/15/10116210.aspx
    /// </remarks>
    /// <typeparam name="T">
    /// The type of object that is being initialized.
    /// </typeparam>
    public sealed class AsyncLazy<T> : Lazy<Task<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class.
        /// </summary>
        /// <param name="factory">
        /// The delegate that is invoked to produce the lazily initialized
        /// value when it is needed.
        /// </param>
        public AsyncLazy(Func<T> factory)
            : base(
                  () => Task.Factory.StartNew(factory, default, TaskCreationOptions.None, TaskScheduler.Current),
                  LazyThreadSafetyMode.ExecutionAndPublication)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class.
        /// </summary>
        /// <param name="factory">
        /// The asynchronous delegate that is invoked to produce the lazily
        /// initialized value when it is needed.
        /// </param>
        public AsyncLazy(Func<Task<T>> factory)
            : base(
                  () => Task.Factory.StartNew(factory, default, TaskCreationOptions.None, TaskScheduler.Current).Unwrap(),
                  LazyThreadSafetyMode.ExecutionAndPublication)
        {
        }

        /// <summary>
        /// Returns the awaiter used to await the lazy initialized value.
        /// </summary>
        /// <returns>An awaiter instance.</returns>
        public TaskAwaiter<T> GetAwaiter()
        {
            return Value.GetAwaiter();
        }
    }
}
