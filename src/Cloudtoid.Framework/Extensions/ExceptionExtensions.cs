using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static Cloudtoid.Contract;

namespace Cloudtoid
{
    [DebuggerStepThrough]
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Checks if <paramref name="exception"/> is considered a fatal exception such as <see cref="ThreadAbortException"/>,
        /// <see cref="AccessViolationException"/>, <see cref="SEHException"/>, <see cref="StackOverflowException"/>,
        /// <see cref="TypeInitializationException"/>, or <see cref="OutOfMemoryException"/> but not
        /// <see cref="InsufficientMemoryException"/>
        /// </summary>
        public static bool IsFatal(this Exception exception)
        {
            CheckValue(exception, nameof(exception));

            var ex = (Exception?)exception;
            while (ex != null)
            {
                // Unlike OutOfMemoryException, InsufficientMemoryException is thrown before starting an operation, and
                // thus does not imply state corruption. An application can catch this exception, throttle back its
                // memory usage, and avoid actual out of memory conditions and their potential for corrupting program state.
                if (ex is OutOfMemoryException && !(ex is InsufficientMemoryException))
                    return true;

                if (ex is ThreadAbortException
                    || ex is AccessViolationException
                    || ex is SEHException
                    || ex is StackOverflowException
                    || ex is TypeInitializationException)
                {
                    return true;
                }

                // These exceptions aren't fatal in themselves, but the CLR uses them
                // to wrap other exceptions, so we want to look deeper
                if (ex is TargetInvocationException tie)
                {
                    ex = tie.InnerException;
                }
                else if (ex is AggregateException aex)
                {
                    // AggregateException can contain other AggregateExceptions in its InnerExceptions list so we
                    // flatten it first. That will essentially create a list of exceptions from the AggregateException's
                    // InnerExceptions property in such a way that any exception other than AggregateException is put
                    // into this list. If there is an AggregateException then exceptions from it's InnerExceptions list are
                    // put into this new list etc. Then a new instance of AggregateException with this flattened list is returned.
                    //
                    // AggregateException InnerExceptions list is immutable after creation and the walk happens only for
                    // the InnerExceptions property of AggregateException and not InnerException of the specific exceptions.
                    // This means that the only way to have a circular referencing here is through reflection and forward-
                    // reference assignment which would be insane. In such case we would also run into stack overflow
                    // when tracing out the exception since AggregateException's ToString does not have any protection there.
                    //
                    // On that note that's another reason why we want to flatten here as opposed to just let recursion do its magic
                    // since in an unlikely case there is a circle we'll get OutOfMemory here instead of StackOverflow which is
                    // a lesser of the two evils.
                    var faex = aex.Flatten();
                    var iexs = faex.InnerExceptions;
                    if (iexs != null && iexs.Any(IsFatal))
                        return true;

                    ex = ex.InnerException;
                }
                else
                {
                    break;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if <paramref name="exception"/> is of type <see cref="OperationCanceledException"/>
        /// or <see cref="TaskCanceledException"/>.
        /// </summary>
        public static bool IsCancelOrTimeout(this Exception exception)
            => exception is OperationCanceledException || exception is TaskCanceledException;

        /// <summary>
        /// Checks if <paramref name="exception"/> is considered a fatal exception such as <see cref="ThreadAbortException"/> or
        /// is of type <see cref="OperationCanceledException"/> or <see cref="TaskCanceledException"/>.
        /// </summary>
        public static bool IsFatalOrCancelOrTimeout(this Exception exception)
            => IsFatal(exception) || IsCancelOrTimeout(exception);
    }
}
