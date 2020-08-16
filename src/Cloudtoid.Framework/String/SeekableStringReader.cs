using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Cloudtoid.Contract;

namespace Cloudtoid
{
    /// <summary>
    /// This class implements a seekable text reader that reads from a string.
    /// </summary>
    public class SeekableStringReader : TextReader
    {
        private readonly int length;
        private int pos;
        private bool disposed;

        public SeekableStringReader(string value)
        {
            Value = CheckValue(value, nameof(value));
            length = value.Length;
        }

        public string Value { get; }

        public virtual int NextPosition
        {
            get
            {
                CheckDisposed();
                return pos;
            }
            set => pos = CheckRange(value, 0, length, nameof(NextPosition));
        }

        public override void Close()
            => Dispose(true);

        protected override void Dispose(bool disposing)
        {
            disposed = true;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Returns the next available character without actually reading it from
        /// the underlying string. The current position of the StringReader is not
        /// changed by this operation. The returned value is -1 if no further
        /// characters are available.
        /// </summary>
        public override int Peek()
        {
            CheckDisposed();
            return pos == length ? -1 : Value[pos];
        }

        /// <summary>
        /// Reads the next character from the underlying string. The returned value
        /// is -1 if no further characters are available.
        /// </summary>
        public override int Read()
        {
            CheckDisposed();
            return pos == length ? -1 : Value[pos++];
        }

        /// <summary>
        /// Reads a block of characters. This method will read up to count
        /// characters from this StringReader into the buffer character
        /// array starting at position index. Returns the actual number of
        /// characters read, or zero if the end of the string is reached.
        /// </summary>
        public override int Read(char[] buffer, int index, int count)
        {
            CheckValue(buffer, nameof(buffer));
            CheckNonNegative(index, nameof(index));
            CheckNonNegative(count, nameof(count));
            Check(buffer.Length - index >= count, "The length of the buffer is smaller than what is needed by index + count");
            CheckDisposed();

            int n = length - pos;
            if (n > 0)
            {
                if (n > count)
                    n = count;

                Value.CopyTo(pos, buffer, index, n);
                pos += n;
            }

            return n;
        }

        public override int Read(Span<char> buffer)
        {
            CheckDisposed();

            int n = length - pos;
            if (n > 0)
            {
                if (n > buffer.Length)
                    n = buffer.Length;

                Value.AsSpan(pos, n).CopyTo(buffer);
                pos += n;
            }

            return n;
        }

        public override int ReadBlock(Span<char> buffer)
            => Read(buffer);

        public override string ReadToEnd()
        {
            CheckDisposed();

            var s = pos == 0
                ? Value
                : Value[pos..length];

            pos = length;
            return s;
        }

        /// <summary>
        /// Reads a line. A line is defined as a sequence of characters followed by
        /// a carriage return ('\r'), a line feed ('\n'), or a carriage return
        /// immediately followed by a line feed. The resulting string does not
        /// contain the terminating carriage return and/or line feed. The returned
        /// value is null if the end of the underlying string has been reached.
        /// </summary>
        public override string? ReadLine()
        {
            CheckDisposed();

            int i = pos;
            while (i < length)
            {
                char ch = Value[i];
                if (ch == '\r' || ch == '\n')
                {
                    string result = Value[pos..i];
                    pos = i + 1;
                    if (ch == '\r' && pos < length && Value[pos] == '\n')
                    {
                        pos++;
                    }

                    return result;
                }

                i++;
            }

            if (i > pos)
            {
                string result = Value[pos..i];
                pos = i;
                return result;
            }

            return null;
        }

        public override Task<string?> ReadLineAsync()
            => Task.FromResult(ReadLine());

        public override Task<string> ReadToEndAsync()
            => Task.FromResult(ReadToEnd());

        public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            CheckValue(buffer, nameof(buffer));
            CheckNonNegative(index, nameof(index));
            CheckNonNegative(count, nameof(count));
            Check(buffer.Length - index >= count, "The length of the buffer is smaller than what is needed by index + count");

            return Task.FromResult(ReadBlock(buffer, index, count));
        }

        public override ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested
                ? new ValueTask<int>(Task.FromCanceled<int>(cancellationToken))
                : new ValueTask<int>(ReadBlock(buffer.Span));
        }

        public override Task<int> ReadAsync(char[] buffer, int index, int count)
            => Task.FromResult(Read(buffer, index, count));

        public override ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested
                ? new ValueTask<int>(Task.FromCanceled<int>(cancellationToken))
                : new ValueTask<int>(Read(buffer.Span));
        }

        private void CheckDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(SeekableStringReader));
        }
    }
}
