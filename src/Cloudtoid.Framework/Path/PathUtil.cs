using System;
using System.Diagnostics;
using System.IO;

namespace Cloudtoid.Framework
{
    [DebuggerStepThrough]
    public static class PathUtil
    {
        /// <summary>
        /// Converts a path to its absolute form. If it is a relative path, it assumes that it is relative to
        /// <see cref="Environment.CurrentDirectory"/>.
        /// </summary>
        public static string GetAbsolutePath(string path)
            => Path.IsPathRooted(path) ? path : Path.Combine(Environment.CurrentDirectory, path);

        /// <summary>
        /// Deletes a file or returns <see langword="false"/> if it fails to delete the file.
        /// No exceptions are thrown.
        /// </summary>
        public static bool TryDeleteFile(string file)
        {
            try
            {
                File.Delete(file);
                return true;
            }
            catch (Exception ex) when (!ex.IsFatal())
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a unique file name in the <paramref name="path"/> ensuring that
        /// such a file name does not exist. The path returned is the shorter of
        /// absolute and relative paths to this new unique file. The relative path
        /// is assumed to be relative to <see cref="Environment.CurrentDirectory"/>.
        /// </summary>
        public static string CreateShortUniqueFileName(
            string path,
            string fileName,
            string fileExtension)
        {
            path = ShortenPath(path);
            string filePath;
            do
            {
                var index = DateTime.Now.Ticks % 0xFFFFF;
                var name = fileName + index.ToStringInvariant("X5") + fileExtension;
                filePath = Path.Combine(path, name);
            }
            while (File.Exists(filePath));

            return filePath;
        }

        // shortens a file path by choosing the shorter of absolute and relative paths
        private static string ShortenPath(string path)
        {
            var relativePath = Path.GetRelativePath(Environment.CurrentDirectory, path);
            return relativePath.Length < path.Length ? relativePath : path;
        }
    }
}
