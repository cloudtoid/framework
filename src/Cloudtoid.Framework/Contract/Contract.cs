using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudtoid
{
    /// <summary>
    /// Execution contract assertions also known as Code Contracts
    /// </summary>
    [DebuggerStepThrough]
    public static class Contract
    {
        /// <summary>
        /// General purpose condition check.
        /// </summary>
        /// <exception cref="Exception"> is thrown on failure.</exception>
        /// <param name="condition">The condition to check.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static bool Check(bool condition, string format, params object?[] args)
        {
            if (!condition)
                throw ExceptCheck(format, args);

            return condition;
        }

        /// <summary>
        /// Used for general argument validation not covered by other Check methods.
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckParam(bool condition, string paramName)
            => CheckParam(condition, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Used for general argument validation not covered by other Check methods.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <param name="condition">The condition to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static bool CheckParam(bool condition, string paramName, string? format, params object?[] args)
        {
            if (!condition)
                throw ExceptParam(paramName, format, args);

            return condition;
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckValue<T>(T? val, string paramName) where T : class
            => CheckValue(val, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <typeparam name="TArg">The type of the argument for message format.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 1 argument only.</param>
        /// <param name="arg">The argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckValue<T, TArg>(T? val, string paramName, string format, TArg arg) where T : class
            => val ?? throw ExceptValue(paramName, format, new object?[] { arg });

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <typeparam name="TArg0">The type of the first argument for message format.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument for message format.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 2 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckValue<T, TArg0, TArg1>(T? val, string paramName, string format, TArg0 arg0, TArg1 arg1) where T : class
            => val ?? throw ExceptValue(paramName, format, new object?[] { arg0, arg1 });

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <typeparam name="TArg0">The type of the first argument for message format.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument for message format.</typeparam>
        /// <typeparam name="TArg2">The type of the third argument for message format.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 3 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        /// <param name="arg2">The third argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckValue<T, TArg0, TArg1, TArg2>(T? val, string paramName, string format, TArg0 arg0, TArg1 arg1, TArg2 arg2) where T : class
            => val ?? throw ExceptValue(paramName, format, new object?[] { arg0, arg1, arg2 });

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckValue<T>(T? val, string paramName, string? format, params object?[] args) where T : class
            => val ?? throw ExceptValue(paramName, format, args);

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckValue<T>(in T? val, string paramName) where T : struct
            => CheckValue(val, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <typeparam name="TArg">The type of the argument for message format.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 1 argument only.</param>
        /// <param name="arg">The argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckValue<T, TArg>(in T? val, string paramName, string format, TArg arg) where T : struct
            => val ?? throw ExceptValue(paramName, format, new object?[] { arg });

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <typeparam name="TArg0">The type of the first argument for message format.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument for message format.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 2 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckValue<T, TArg0, TArg1>(in T? val, string paramName, string format, TArg0 arg0, TArg1 arg1) where T : struct
            => val ?? throw ExceptValue(paramName, format, new object?[] { arg0, arg1 });

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <typeparam name="TArg0">The type of the first argument for message format.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument for message format.</typeparam>
        /// <typeparam name="TArg2">The type of the third argument for message format.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 3 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        /// <param name="arg2">The third argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckValue<T, TArg0, TArg1, TArg2>(in T? val, string paramName, string format, TArg0 arg0, TArg1 arg1, TArg2 arg2) where T : struct
            => val ?? throw ExceptValue(paramName, format, new object?[] { arg0, arg1, arg2 });

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckValue<T>(T? val, string paramName, string? format, params object?[] args) where T : struct
        {
            if (val is null)
                throw ExceptValue(paramName, format, args);

            return val.Value;
        }

#pragma warning disable VSTHRD200

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="task">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckValue<T>(Task<T>? task, string paramName)
        {
            if (task is null)
                throw ExceptValue(paramName, null, null);
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the <see cref="Task"/> being tested.</typeparam>
        /// <typeparam name="TArg">The type of the argument for message format.</typeparam>
        /// <param name="task">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 1 argument only.</param>
        /// <param name="arg">The argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckValue<T, TArg>(Task<T>? task, string paramName, string format, TArg arg)
        {
            if (task is null)
                throw ExceptValue(paramName, format, new object?[] { arg });
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the <see cref="Task"/> being tested.</typeparam>
        /// <typeparam name="TArg0">The type of the first argument for message format.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument for message format.</typeparam>
        /// <param name="task">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 2 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckValue<T, TArg0, TArg1>(Task<T>? task, string paramName, string format, TArg0 arg0, TArg1 arg1)
        {
            if (task is null)
                throw ExceptValue(paramName, format, new object?[] { arg0, arg1 });
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the <see cref="Task"/> being tested.</typeparam>
        /// <typeparam name="TArg0">The type of the first argument for message format.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument for message format.</typeparam>
        /// <typeparam name="TArg2">The type of the third argument for message format.</typeparam>
        /// <param name="task">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 3 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        /// <param name="arg2">The third argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckValue<T, TArg0, TArg1, TArg2>(Task<T>? task, string paramName, string format, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            if (task is null)
                throw ExceptValue(paramName, format, new object?[] { arg0, arg1, arg2 });
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="task">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static void CheckValue<T>(Task<T>? task, string paramName, string? format, params object?[] args)
        {
            if (task is null)
                throw ExceptValue(paramName, format, args);
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckValue(Task? val, string paramName)
            => CheckValue(val, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 1 argument only.</param>
        /// <param name="arg">The argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckValue<TArg>(Task? val, string paramName, string format, TArg arg)
        {
            if (val is null)
                throw ExceptValue(paramName, format, new object?[] { arg });
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 2 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckValue<TArg0, TArg1>(Task? val, string paramName, string format, TArg0 arg0, TArg1 arg1)
        {
            if (val is null)
                throw ExceptValue(paramName, format, new object?[] { arg0, arg1 });
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 3 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        /// <param name="arg2">The third argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckValue<TArg0, TArg1, TArg2>(Task? val, string paramName, string format, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            if (val is null)
                throw ExceptValue(paramName, format, new object?[] { arg0, arg1, arg2 });
        }

        /// <summary>
        /// Validates that the value is non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static void CheckValue(Task? val, string paramName, string? format, params object?[] args)
        {
            if (val is null)
                throw ExceptValue(paramName, format, args);
        }

#pragma warning restore VSTHRD200

        /// <summary>
        /// Validates that the value is null.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? CheckNonValue<T>(T? val, string paramName) where T : class
            => CheckNonValue(val, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value is null.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T? CheckNonValue<T>(T? val, string paramName, string? format, params object?[] args) where T : class
        {
            if (val is null)
                return null;

            throw ExceptNonValue(paramName, format, args);
        }

        /// <summary>
        /// Validates that a string is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="s"/> is null.</exception>
        /// <param name="s">The string to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CheckNonEmpty(string? s, string paramName)
            => CheckNonEmpty(s, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that a string is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="s"/> is null.</exception>
        /// <param name="s">The string to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 1 argument only.</param>
        /// <param name="arg">The argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CheckNonEmpty<TArg>(string? s, string paramName, string format, TArg arg)
        {
            CheckValue(s, paramName, format, arg);

            if (s!.Length == 0)
                throw ExceptNonEmpty(paramName, format, new object?[] { arg });

            return s;
        }

        /// <summary>
        /// Validates that a string is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="s"/> is null.</exception>
        /// <param name="s">The string to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 2 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CheckNonEmpty<TArg0, TArg1>(string? s, string paramName, string format, TArg0 arg0, TArg1 arg1)
        {
            CheckValue(s, paramName, format, arg0, arg1);

            if (s!.Length == 0)
                throw ExceptNonEmpty(paramName, format, new object?[] { arg0, arg1 });

            return s;
        }

        /// <summary>
        /// Validates that a string is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="s"/> is null.</exception>
        /// <param name="s">The string to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message format with 3 arguments.</param>
        /// <param name="arg0">The first argument for message format.</param>
        /// <param name="arg1">The second argument for message format.</param>
        /// <param name="arg2">The third argument for message format.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CheckNonEmpty<TArg0, TArg1, TArg2>(string? s, string paramName, string format, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            CheckValue(s, paramName, format, arg0, arg1, arg2);

            if (s!.Length == 0)
                throw ExceptNonEmpty(paramName, format, new object?[] { arg0, arg1, arg2 });

            return s;
        }

        /// <summary>
        /// Validates that a string is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="s"/> is null.</exception>
        /// <param name="s">The string to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static string CheckNonEmpty(string? s, string paramName, string? format, params object?[] args)
        {
            CheckValue(s, paramName, format, args);

            if (s!.Length == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return s;
        }

        /// <summary>
        /// Verify whether Guid is Empty.
        /// Note: This is needed because .NET Guid is not a class, it's a value type
        /// and hence the CheckValue generic of class type does not cover this.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <param name="val">The Guid to test.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid CheckNonEmpty(Guid val, string paramName)
            => CheckNonEmpty(val, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Verify whether Guid is Empty.
        /// Note: This is needed because .NET Guid is not a class, it's a value type
        /// and hence the CheckValue generic of class type does not cover this.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <param name="val">The Guid to test.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static Guid CheckNonEmpty(Guid val, string paramName, string? format, params object?[] args)
        {
            if (val.CompareTo(Guid.Empty) == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return val;
        }

        /// <summary>
        /// Verify whether a <see cref="CancellationToken"/> is <see cref="CancellationToken.None"/> or default./>.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <param name="val">The <see cref="CancellationToken"/> to test.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CA1068 // CancellationToken parameters must come last
        public static CancellationToken CheckNonEmpty(CancellationToken val, string paramName)
            => CheckNonEmpty(val, paramName, null, Array.Empty<object>());
#pragma warning restore CA1068 // CancellationToken parameters must come last

        /// <summary>
        /// Verify whether a <see cref="CancellationToken"/> is <see cref="CancellationToken.None"/> or default./>.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <param name="val">The <see cref="CancellationToken"/> to test.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
#pragma warning disable CA1068 // CancellationToken parameters must come last
        public static CancellationToken CheckNonEmpty(CancellationToken val, string paramName, string? format, params object?[] args)
#pragma warning restore CA1068 // CancellationToken parameters must come last
        {
            if (val == default || val == CancellationToken.None)
                throw ExceptNonEmpty(paramName, format, args);

            return val;
        }

        /// <summary>
        /// Validates that a collection is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICollection<T> CheckNonEmpty<T>(ICollection<T>? arguments, string paramName)
            => CheckNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that a collection is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static ICollection<T> CheckNonEmpty<T>(ICollection<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            if (items.Count == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return items;
        }

        /// <summary>
        /// Validates that a collection is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<T> CheckNonEmpty<T>(IReadOnlyCollection<T>? arguments, string paramName)
            => CheckNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that a collection is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IReadOnlyCollection<T> CheckNonEmpty<T>(IReadOnlyCollection<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            if (items.Count == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return items;
        }

        /// <summary>
        /// Validates that a list is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> CheckNonEmpty<T>(IList<T>? arguments, string paramName)
            => CheckNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that a list is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IList<T> CheckNonEmpty<T>(IList<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            if (items.Count == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return items;
        }

        /// <summary>
        /// Validates that a list is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> CheckNonEmpty<T>(IReadOnlyList<T>? arguments, string paramName)
            => CheckNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that a list is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IReadOnlyList<T> CheckNonEmpty<T>(IReadOnlyList<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            if (items.Count == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return items;
        }

        /// <summary>
        /// Validates that an array is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] CheckNonEmpty<T>(T[]? arguments, string paramName)
            => CheckNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that an array is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="T">The type of the items being tested.</typeparam>
        /// <param name="arguments">The collection being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T[] CheckNonEmpty<T>(T[]? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            if (items.Length == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return items;
        }

        /// <summary>
        /// Validates that a dictionary is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="TKey">The type of dictionary entry key being tested.</typeparam>
        /// <typeparam name="TValue">The type of dictionary entry value being tested.</typeparam>
        /// <param name="arguments">The dictionary being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TKey, TValue> CheckNonEmpty<TKey, TValue>(IDictionary<TKey, TValue>? arguments, string paramName) where TKey : notnull
            => CheckNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that a dictionary is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="TKey">The type of dictionary entry key being tested.</typeparam>
        /// <typeparam name="TValue">The type of dictionary entry value being tested.</typeparam>
        /// <param name="arguments">The dictionary being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IDictionary<TKey, TValue> CheckNonEmpty<TKey, TValue>(IDictionary<TKey, TValue>? arguments, string paramName, string? format, params object?[] args) where TKey : notnull
        {
            var items = CheckValue(arguments, paramName, format, args);

            if (items.Count == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return items;
        }

        /// <summary>
        /// Validates that a read only dictionary is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="TKey">The type of dictionary entry key being tested.</typeparam>
        /// <typeparam name="TValue">The type of dictionary entry value being tested.</typeparam>
        /// <param name="arguments">The dictionary being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<TKey, TValue> CheckNonEmpty<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>? arguments, string paramName) where TKey : notnull
            => CheckNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that a read only dictionary is non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <typeparam name="TKey">The type of dictionary entry key being tested.</typeparam>
        /// <typeparam name="TValue">The type of dictionary entry value being tested.</typeparam>
        /// <param name="arguments">The dictionary being tested.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IReadOnlyDictionary<TKey, TValue> CheckNonEmpty<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>? arguments, string paramName, string? format, params object?[] args) where TKey : notnull
        {
            var items = CheckValue(arguments, paramName, format, args);

            if (items.Count == 0)
                throw ExceptNonEmpty(paramName, format, args);

            return items;
        }

        /// <summary>
        /// Validates that a string is non-null, non-empty, and non-whitespace.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown if <paramref name="s"/> is empty or white-space.</exception>
        /// <exception cref="ArgumentNullException"> is thrown if <paramref name="s"/> is null.</exception>
        /// /// <param name="s">The string to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CheckNonWhitespace(string? s, string paramName)
            => CheckNonWhitespace(s, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that a string is non-null, non-empty, and non-whitespace.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown if <paramref name="s"/> is empty.</exception>
        /// <exception cref="ArgumentNullException"> is thrown if <paramref name="s"/> is null.</exception>
        /// <param name="s">The string to check.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static string CheckNonWhitespace(string? s, string paramName, string? format, params object?[] args)
        {
            CheckValue(s, paramName, format, args);

            if (string.IsNullOrWhiteSpace(s))
                throw ExceptNonEmpty(paramName, format, args);

            return s!;
        }

        /// <summary>
        /// Validates numeric ranges or relationships (like that an array index is within
        /// the bounds of the array).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="min">The absolute minimum - inclusive.</param>
        /// <param name="max">The absolute maximum - inclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckRange<T>(T value, T min, T max, string paramName) where T : IComparable<T>
            => CheckRange(value, min, max, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates numeric ranges or relationships (like that an array index is within
        /// the bounds of the array).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="min">The absolute minimum - inclusive.</param>
        /// <param name="max">The absolute maximum - inclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckRange<T>(T value, T min, T max, string paramName, string? format, params object?[] args) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
                throw ExceptRange(paramName, min, max, format, args);

            return value;
        }

        /// <summary>
        /// Validates that the value is non-negative.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CheckNonNegative(int value, string paramName)
            => CheckGreaterThanOrEqual(value, 0, paramName);

        /// <summary>
        /// Validates that the value is non-negative.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static int CheckNonNegative(int value, string paramName, string? format, params object?[] args)
            => CheckGreaterThanOrEqual(value, 0, paramName, format, args);

        /// <summary>
        /// Validates that the value is greater than or equal to the min value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="min">The absolute minimum - inclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckGreaterThanOrEqual<T>(T value, T min, string paramName) where T : IComparable<T>
            => CheckGreaterThanOrEqual(value, min, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value is greater than or equal to the min value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="min">The absolute minimum - inclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckGreaterThanOrEqual<T>(T value, T min, string paramName, string? format, params object?[] args) where T : IComparable<T>
            => value.CompareTo(min) >= 0 ? value : throw ExceptGreaterThanOrEqual(paramName, min, format, args);

        /// <summary>
        /// Validates that the value is less than or equal to the max value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="max">The absolute maximum - inclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckLessThanOrEqual<T>(T value, T max, string paramName) where T : IComparable<T>
            => CheckLessThanOrEqual(value, max, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value is less than or equal to the max value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="max">The absolute maximum - inclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckLessThanOrEqual<T>(T value, T max, string paramName, string? format, params object?[] args) where T : IComparable<T>
            => value.CompareTo(max) <= 0 ? value : throw ExceptLessThanOrEqual(paramName, max, format, args);

        /// <summary>
        /// Validates that the value is greater than the min value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="min">The absolute minimum - exclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckGreaterThan<T>(T value, T min, string paramName) where T : IComparable<T>
            => CheckGreaterThan(value, min, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value is greater than the min value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="min">The absolute minimum - exclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckGreaterThan<T>(T value, T min, string paramName, string? format, params object?[] args) where T : IComparable<T>
            => value.CompareTo(min) > 0 ? value : throw ExceptGreaterThan(paramName, min, format, args);

        /// <summary>
        /// Validates that the value is less than the max value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="max">The absolute maximum - exclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckLessThan<T>(T value, T max, string paramName) where T : IComparable<T>
            => CheckLessThan(value, max, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value is less than the max value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> is thrown on failure.</exception>
        /// <param name="value">The actual condition.</param>
        /// <param name="max">The absolute maximum - exclusive.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckLessThan<T>(T value, T max, string paramName, string? format, params object?[] args) where T : IComparable<T>
            => value.CompareTo(max) < 0 ? value : throw ExceptLessThan(paramName, max, format, args);

        /// <summary>
        /// Validates that all the strings in a specified collection are
        /// non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<string> CheckAllNonEmpty(IList<string?>? arguments, string paramName)
            => CheckAllNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the strings in a specified collection are
        /// non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> or any of its items is null.</exception>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IList<string> CheckAllNonEmpty(IList<string?>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);
            var size = items.Count;

            for (var i = 0; i < size; i++)
            {
                var item = items[i];

                if (item is null)
                    throw ExceptAllValues(paramName, format, args);

                if (item.Length == 0)
                    throw ExceptAllNonEmpty(paramName, format, args);
            }

            return items!;
        }

        /// <summary>
        /// Validates that all the strings in a specified collection are
        /// non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<string> CheckAllNonEmpty(IReadOnlyList<string?>? arguments, string paramName)
            => CheckAllNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the strings in a specified collection are
        /// non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> or any of its items is null.</exception>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IReadOnlyList<string> CheckAllNonEmpty(IReadOnlyList<string?>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);
            var size = items.Count;

            for (var i = 0; i < size; i++)
            {
                var item = items[i];

                if (item is null)
                    throw ExceptAllValues(paramName, format, args);

                if (item.Length == 0)
                    throw ExceptAllNonEmpty(paramName, format, args);
            }

            return items!;
        }

        /// <summary>
        /// Validates that all the strings in a specified collection are
        /// non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> or any of its items is null.</exception>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<string> CheckAllNonEmpty(ISet<string?>? arguments, string paramName)
            => CheckAllNonEmpty(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the strings in a specified collection are
        /// non-null and non-empty.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> or any of its items is null.</exception>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static ISet<string> CheckAllNonEmpty(ISet<string?>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            foreach (var argument in items)
            {
                if (argument == null)
                    throw ExceptAllNonEmpty(paramName, format, args);

                if (argument.Length == 0)
                    throw ExceptAllNonEmpty(paramName, format, args);
            }

            return items!;
        }

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> CheckAllValues<T>(IList<T>? arguments, string paramName)
            => CheckAllValues(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IList<T> CheckAllValues<T>(IList<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);
            var size = items.Count;

            for (var i = 0; i < size; i++)
            {
                if (items[i] is null)
                    throw ExceptAllValues(paramName, format, args);
            }

            return items;
        }

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> CheckAllValues<T>(IReadOnlyList<T>? arguments, string paramName)
            => CheckAllValues(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IReadOnlyList<T> CheckAllValues<T>(IReadOnlyList<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);
            var size = items.Count;

            for (var i = 0; i < size; i++)
            {
                if (items[i] is null)
                    throw ExceptAllValues(paramName, format, args);
            }

            return items;
        }

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICollection<T> CheckAllValues<T>(ICollection<T>? arguments, string paramName)
            => CheckAllValues(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static ICollection<T> CheckAllValues<T>(ICollection<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            foreach (var arg in items)
            {
                if (arg is null)
                    throw ExceptAllValues(paramName, format, args);
            }

            return items;
        }

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<T> CheckAllValues<T>(IReadOnlyCollection<T>? arguments, string paramName)
            => CheckAllValues(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IReadOnlyCollection<T> CheckAllValues<T>(IReadOnlyCollection<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            foreach (var item in items)
            {
                if (item is null)
                    throw ExceptAllValues(paramName, format, args);
            }

            return items;
        }

        /// <summary>
        /// Validates that all the items in a set are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The set being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISet<T> CheckAllValues<T>(ISet<T>? arguments, string paramName)
            => CheckAllValues(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the items in a set are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="arguments">The set being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static ISet<T> CheckAllValues<T>(ISet<T>? arguments, string paramName, string? format, params object?[] args)
        {
            var items = CheckValue(arguments, paramName, format, args);

            foreach (var item in items)
            {
                if (item is null)
                    throw ExceptAllValues(paramName, format, args);
            }

            return items;
        }

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="arguments"/> is null.</exception>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TKey, TValue> CheckAllValues<TKey, TValue>(IDictionary<TKey, TValue>? arguments, string paramName) where TKey : notnull
            => CheckAllValues(arguments, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that all the items in a collection are non-null.
        /// </summary>
        /// <exception cref="ArgumentNullException"> is thrown on failure.</exception>
        /// <param name="arguments">The collection being tested. It cannot be null but can be empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static IDictionary<TKey, TValue> CheckAllValues<TKey, TValue>(IDictionary<TKey, TValue>? arguments, string paramName, string? format, params object?[] args) where TKey : notnull
        {
            var items = CheckValue(arguments, paramName, format, args);

            foreach (var item in items.Values)
            {
                if (item is null)
                    throw ExceptAllValues(paramName, format, args);
            }

            return items;
        }

        /// <summary>
        /// Validates that the value of a parameter is of a given type or inherits/implements that type
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="val"/> is null.</exception>
        /// <typeparam name="TType">The type of the value to test.</typeparam>
        /// <param name="val">The value to test.</param>
        /// <param name="paramName">parameter name</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TType CheckIsOfType<TType>(object? val, string paramName) where TType : class
            => CheckIsOfType<TType>(val, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value of a parameter is of a given type or inherits/implements that type
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="val"/> is null.</exception>
        /// <typeparam name="TType">The type of the value to test.</typeparam>
        /// <param name="val">The value to test.</param>
        /// <param name="paramName">parameter name</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static TType CheckIsOfType<TType>(object? val, string paramName, string? format, params object?[] args) where TType : class
        {
            CheckValue(val, paramName, format, args);

            if (val is TType value)
                return value;

            throw ExceptParam(paramName, format, args);
        }

        /// <summary>
        /// Validates that the value of a parameter is not of a given type or inherits/implements that type
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="val"/> is null.</exception>
        /// <typeparam name="TType">The type of the value to test.</typeparam>
        /// <param name="val">The value to test.</param>
        /// <param name="paramName">parameter name</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object CheckIsNotOfType<TType>(object? val, string paramName) where TType : class
            => CheckIsNotOfType<TType>(val, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value of a parameter is not of a given type or inherits/implements that type
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="val"/> is null.</exception>
        /// <typeparam name="TType">The type of the value to test.</typeparam>
        /// <param name="val">The value to test.</param>
        /// <param name="paramName">parameter name</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static object CheckIsNotOfType<TType>(object? val, string paramName, string? format, params object?[] args) where TType : class
        {
            var value = CheckValue(val, paramName, format, args);

            if (value is TType)
                throw ExceptParam(paramName, format, args);

            return value;
        }

        /// <summary>
        /// Validates that the value of a parameter is of an exact type
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="val"/> is null.</exception>
        /// <typeparam name="TType">The type of the value to test.</typeparam>
        /// <param name="val">The value to test.</param>
        /// <param name="paramName">parameter name</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TType CheckIsExactType<TType>(object? val, string paramName) where TType : class
            => CheckIsExactType<TType>(val, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Validates that the value of a parameter is of an exact type.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <exception cref="ArgumentNullException"> is thrown is <paramref name="val"/> is null.</exception>
        /// <typeparam name="TType">The type of the value to test.</typeparam>
        /// <param name="val">The value to test.</param>
        /// <param name="paramName">parameter name</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static TType CheckIsExactType<TType>(object? val, string paramName, string? format, params object?[] args) where TType : class
        {
            CheckValue(val, paramName, format, args);
            CheckIsExactType<TType>(val, paramName, format, args);
            if (val!.GetType() != typeof(TType))
                throw ExceptParam(paramName, format, args);

            return (TType)val;
        }

        /// <summary>
        /// Used to check the equality of the value of the parameter with a comparand.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="expected">The comparand that <paramref name="val"/> is compared to.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckEqual<T>(T val, T expected, string paramName)
            => CheckEqual(val, expected, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Used to check the equality of the value of the parameter with a comparand.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="expected">The comparand that <paramref name="val"/> is compared to.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckEqual<T>(T val, T expected, string paramName, string? format, params object?[] args)
        {
            if (!Equals(val, expected))
                throw ExceptEqual(paramName, format, args);

            return val;
        }

        /// <summary>
        /// Used to check that the <paramref name="val"/> is not equal to <paramref name="notExpected"/>.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="notExpected">The comparand that <paramref name="val"/> is compared to.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CheckNotEqual<T>(T val, T notExpected, string paramName)
            => CheckNotEqual(val, notExpected, paramName, null, Array.Empty<object>());

        /// <summary>
        /// Used to check that the <paramref name="val"/> is not equal to <paramref name="notExpected"/>.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown on failure.</exception>
        /// <typeparam name="T">The type of the value being tested.</typeparam>
        /// <param name="val">The value being tested.</param>
        /// <param name="notExpected">The comparand that <paramref name="val"/> is compared to.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <param name="format">The exception message or message format.</param>
        /// <param name="args">The arguments for message format.</param>
        public static T CheckNotEqual<T>(T val, T notExpected, string paramName, string? format, params object?[] args)
        {
            if (Equals(val, notExpected))
                throw ExceptNotEqual(paramName, notExpected, format, args);

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentNullException ExceptValue(string paramName, string? format, object?[]? args)
        {
            return new ArgumentNullException(
                paramName,
                GetMessage($"{paramName} parameter cannot be null.", format, args));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentException ExceptNonValue(string paramName, string? format, object?[] args)
        {
            return new ArgumentException(
                GetMessage($"{paramName} parameter must be null but it is not.", format, args),
                paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentNullException ExceptAllValues(string paramName, string? format, object?[] args)
        {
            return new ArgumentNullException(
                paramName,
                GetMessage($"{paramName} parameter cannot have a null item.", format, args));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentOutOfRangeException ExceptRange<T>(string paramName, T min, T max, string? format, object?[] args)
        {
            return new ArgumentOutOfRangeException(
                paramName,
                GetMessage($"{paramName} parameter is out of the range of valid values. Range = [{min},{max}]", format, args));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentOutOfRangeException ExceptGreaterThanOrEqual<T>(string paramName, T min, string? format, object?[] args)
        {
            return new ArgumentOutOfRangeException(
                paramName,
                GetMessage($"{paramName} parameter must be greater than or equal to {min}.", format, args));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentOutOfRangeException ExceptLessThanOrEqual<T>(string paramName, T max, string? format, object?[] args)
        {
            return new ArgumentOutOfRangeException(
                paramName,
                GetMessage($"{paramName} parameter must be less than or equal to {max}.", format, args));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentOutOfRangeException ExceptGreaterThan<T>(string paramName, T min, string? format, object?[] args)
        {
            return new ArgumentOutOfRangeException(
                paramName,
                GetMessage($"{paramName} parameter must be greater than {min}.", format, args));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentOutOfRangeException ExceptLessThan<T>(string paramName, T max, string? format, object?[] args)
        {
            return new ArgumentOutOfRangeException(
                paramName,
                GetMessage($"{paramName} parameter must be less than {max}.", format, args));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentException ExceptNonEmpty(string paramName, string? format, object?[] args)
        {
            return new ArgumentException(
                GetMessage($"Value for {paramName} parameter should not be empty.", format, args),
                paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentException ExceptAllNonEmpty(string paramName, string? format, object?[] args)
        {
            return new ArgumentException(
                GetMessage($"{paramName} parameter cannot have empty item.", format, args),
                paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentException ExceptEqual(string paramName, string? format, object?[] args)
        {
            return new ArgumentException(
                GetMessage($"{paramName} does not have the expected value.", format, args),
                paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentException ExceptNotEqual<T>(string paramName, T notExpected, string? format, object?[] args)
        {
            return new ArgumentException(
                GetMessage($"Expected the value for {paramName} parameter NOT to be '{notExpected}', but it is!", format, args),
                paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Exception ExceptParam(string paramName, string? format, object?[] args)
        {
            return new ArgumentException(
                GetMessage($"Value for {paramName} parameter was unexpected.", format, args),
                paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Exception ExceptCheck(string format, object?[] args)
        {
            return new Exception(GetMessageSafe(format, args) ?? "A condition check failed.");
        }

        private static string GetMessage(string genericMessage, string? format, object?[]? args)
        {
            var message = GetMessageSafe(format, args);
            return string.IsNullOrEmpty(message) ? genericMessage : message! + Environment.NewLine + genericMessage;
        }

        private static string? GetMessageSafe(string? format, object?[]? args)
        {
            if (string.IsNullOrEmpty(format))
                return null;

            if (args.IsNullOrEmpty())
                return format;

            try
            {
                return StringUtil.FormatInvariant(format!, args!);
            }
            catch (FormatException)
            {
                return format + " arguments = " + string.Join(",", args);
            }
        }
    }
}
