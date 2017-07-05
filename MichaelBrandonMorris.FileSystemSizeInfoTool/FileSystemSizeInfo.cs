using System.IO;
using System.Linq;
using System.Security.Principal;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    /// <summary>
    ///     Class FileSystemSizeInfo.
    /// </summary>
    /// TODO Edit XML Comment Template for FileSystemSizeInfo
    internal class FileSystemSizeInfo
    {
        /// <summary>
        ///     The bytes in mega byte
        /// </summary>
        /// TODO Edit XML Comment Template for BytesInMegaByte
        private const double BytesInMegaByte = 1000000.0;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="FileSystemSizeInfo" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// TODO Edit XML Comment Template for #ctor
        internal FileSystemSizeInfo(FileInfo file)
        {
            Extension = file.Extension;
            FilesCount = 0;
            FileSystemInfo = file;
            FoldersCount = 0;
            FullName = file.FullName;

            FullNameSplitPath = file.FullName.Split(Path.DirectorySeparatorChar)
                .Where(s => !s.IsNullOrWhiteSpace())
                .ToArray();

            try
            {
                Owner = file.GetAccessControl()
                    .GetOwner(typeof(NTAccount))
                    .ToString();
            }
            catch (IdentityNotMappedException)
            {
                Owner = string.Empty;
            }

            SizeBytes = file.Length;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="FileSystemSizeInfo" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// TODO Edit XML Comment Template for #ctor
        internal FileSystemSizeInfo(DirectoryInfo directory)
        {
            Extension = string.Empty;
            FileSystemInfo = directory;
            FullName = directory.FullName;
            FullNameSplitPath = directory.FullName
                .Split(Path.DirectorySeparatorChar)
                .Where(s => !s.IsNullOrWhiteSpace())
                .ToArray();

            GetContentsAndSize(directory);

            try
            {
                Owner = directory.GetAccessControl()
                    .GetOwner(typeof(NTAccount))
                    .ToString();
            }
            catch (IdentityNotMappedException)
            {
                Owner = string.Empty;
            }
        }

        /// <summary>
        ///     Gets the size.
        /// </summary>
        /// <value>The size.</value>
        /// TODO Edit XML Comment Template for Size
        public double Size => SizeBytes / BytesInMegaByte;

        /// <summary>
        ///     Gets the contents count.
        /// </summary>
        /// <value>The contents count.</value>
        /// TODO Edit XML Comment Template for ContentsCount
        internal int ContentsCount => FilesCount + FoldersCount;

        /// <summary>
        ///     Gets the extension.
        /// </summary>
        /// <value>The extension.</value>
        /// TODO Edit XML Comment Template for Extension
        internal string Extension
        {
            get;
        }

        /// <summary>
        ///     Gets the file system information.
        /// </summary>
        /// <value>The file system information.</value>
        /// TODO Edit XML Comment Template for FileSystemInfo
        internal FileSystemInfo FileSystemInfo
        {
            get;
        }

        /// <summary>
        ///     Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        /// TODO Edit XML Comment Template for FullName
        internal string FullName
        {
            get;
        }

        /// <summary>
        ///     Gets the full name split path.
        /// </summary>
        /// <value>The full name split path.</value>
        /// TODO Edit XML Comment Template for FullNameSplitPath
        internal string[] FullNameSplitPath
        {
            get;
        }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>The owner.</value>
        /// TODO Edit XML Comment Template for Owner
        internal string Owner
        {
            get;
        }

        /// <summary>
        ///     Gets the path levels.
        /// </summary>
        /// <value>The path levels.</value>
        /// TODO Edit XML Comment Template for PathLevels
        internal int PathLevels => FullNameSplitPath.Length;

        /// <summary>
        ///     Gets or sets the files count.
        /// </summary>
        /// <value>The files count.</value>
        /// TODO Edit XML Comment Template for FilesCount
        internal int FilesCount
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the folders count.
        /// </summary>
        /// <value>The folders count.</value>
        /// TODO Edit XML Comment Template for FoldersCount
        internal int FoldersCount
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the size bytes.
        /// </summary>
        /// <value>The size bytes.</value>
        /// TODO Edit XML Comment Template for SizeBytes
        private long SizeBytes
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the size of the contents and.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// TODO Edit XML Comment Template for GetContentsAndSize
        private void GetContentsAndSize(DirectoryInfo directory)
        {
            foreach (var fileSystemInfo in directory.GetFileSystemInfos())
            {
                var directoryInfo = fileSystemInfo as DirectoryInfo;

                if (directoryInfo != null)
                {
                    FoldersCount++;
                    GetContentsAndSize(directoryInfo);
                    continue;
                }

                var fileInfo = fileSystemInfo as FileInfo;

                if (fileInfo == null)
                {
                    continue;
                }

                FilesCount++;
                SizeBytes += fileInfo.Length;
            }
        }
    }
}