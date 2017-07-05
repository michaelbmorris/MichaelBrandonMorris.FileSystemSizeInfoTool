using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MichaelBrandonMorris.Extensions.CollectionExtensions;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    /// <summary>
    ///     Class FileSystemSizeInfoGetter.
    /// </summary>
    /// TODO Edit XML Comment Template for FileSystemSizeInfoGetter
    internal class FileSystemSizeInfoGetter
    {
        /// <summary>
        ///     The bytes in mega byte
        /// </summary>
        /// TODO Edit XML Comment Template for BytesInMegaByte
        private const int BytesInMegaByte = 1000000;

        /// <summary>
        ///     The root level
        /// </summary>
        /// TODO Edit XML Comment Template for RootLevel
        private const int RootLevel = 0;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="FileSystemSizeInfoGetter" /> class.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="searchPaths">The search paths.</param>
        /// <param name="excludedPaths">The excluded paths.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="shouldExcludeExtensions">
        ///     if set to <c>true</c>
        ///     [should exclude extensions].
        /// </param>
        /// <param name="extensions">The extensions.</param>
        /// <param name="minFileSize">Minimum size of the file.</param>
        /// <param name="maxFileSize">Maximum size of the file.</param>
        /// <param name="minFolderSize">Minimum size of the folder.</param>
        /// <param name="maxFolderSize">Maximum size of the folder.</param>
        /// <param name="minFolderContents">
        ///     The minimum folder
        ///     contents.
        /// </param>
        /// <param name="maxFolderContents">
        ///     The maximum folder
        ///     contents.
        /// </param>
        /// TODO Edit XML Comment Template for #ctor
        internal FileSystemSizeInfoGetter(
            CancellationToken cancellationToken,
            IEnumerable<string> searchPaths,
            IEnumerable<string> excludedPaths,
            Scope scope,
            bool shouldExcludeExtensions,
            IEnumerable<string> extensions = null,
            int? minFileSize = null,
            int? maxFileSize = null,
            int? minFolderSize = null,
            int? maxFolderSize = null,
            int? minFolderContents = null,
            int? maxFolderContents = null)
        {
            CancellationToken = cancellationToken;

            foreach (var searchPath in searchPaths)
            {
                CancellationToken.ThrowIfCancellationRequested();
                SearchDirectories.Add(new DirectoryInfo(searchPath));
            }

            ExcludedPaths = excludedPaths;
            Scope = scope;
            ShouldExcludeExtensions = shouldExcludeExtensions;
            Extensions = extensions;
            MinFileSize = minFileSize * BytesInMegaByte;
            MaxFileSize = maxFileSize * BytesInMegaByte;
            MinFolderSize = minFolderSize * BytesInMegaByte;
            MaxFolderSize = maxFolderSize * BytesInMegaByte;
            MinFolderContents = minFolderContents * BytesInMegaByte;
            MaxFolderContents = maxFolderContents * BytesInMegaByte;
        }

        /// <summary>
        ///     Gets or sets the file system size infos.
        /// </summary>
        /// <value>The file system size infos.</value>
        /// TODO Edit XML Comment Template for FileSystemSizeInfos
        internal HashSet<FileSystemSizeInfo> FileSystemSizeInfos
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the maximum path levels.
        /// </summary>
        /// <value>The maximum path levels.</value>
        /// TODO Edit XML Comment Template for MaxPathLevels
        internal int MaxPathLevels
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the cancellation token.
        /// </summary>
        /// <value>The cancellation token.</value>
        /// TODO Edit XML Comment Template for CancellationToken
        private CancellationToken CancellationToken
        {
            get;
        }

        /// <summary>
        ///     Gets the excluded paths.
        /// </summary>
        /// <value>The excluded paths.</value>
        /// TODO Edit XML Comment Template for ExcludedPaths
        private IEnumerable<string> ExcludedPaths
        {
            get;
        }

        /// <summary>
        ///     Gets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        /// TODO Edit XML Comment Template for Extensions
        private IEnumerable<string> Extensions
        {
            get;
        }

        /// <summary>
        ///     Gets the maximum size of the file.
        /// </summary>
        /// <value>The maximum size of the file.</value>
        /// TODO Edit XML Comment Template for MaxFileSize
        private long? MaxFileSize
        {
            get;
        }

        /// <summary>
        ///     Gets the maximum folder contents.
        /// </summary>
        /// <value>The maximum folder contents.</value>
        /// TODO Edit XML Comment Template for MaxFolderContents
        private long? MaxFolderContents
        {
            get;
        }

        /// <summary>
        ///     Gets the maximum size of the folder.
        /// </summary>
        /// <value>The maximum size of the folder.</value>
        /// TODO Edit XML Comment Template for MaxFolderSize
        private long? MaxFolderSize
        {
            get;
        }

        /// <summary>
        ///     Gets the minimum size of the file.
        /// </summary>
        /// <value>The minimum size of the file.</value>
        /// TODO Edit XML Comment Template for MinFileSize
        private long? MinFileSize
        {
            get;
        }

        /// <summary>
        ///     Gets the minimum folder contents.
        /// </summary>
        /// <value>The minimum folder contents.</value>
        /// TODO Edit XML Comment Template for MinFolderContents
        private long? MinFolderContents
        {
            get;
        }

        /// <summary>
        ///     Gets the minimum size of the folder.
        /// </summary>
        /// <value>The minimum size of the folder.</value>
        /// TODO Edit XML Comment Template for MinFolderSize
        private long? MinFolderSize
        {
            get;
        }

        /// <summary>
        ///     Gets the scope.
        /// </summary>
        /// <value>The scope.</value>
        /// TODO Edit XML Comment Template for Scope
        private Scope Scope
        {
            get;
        }

        /// <summary>
        ///     Gets the search directories.
        /// </summary>
        /// <value>The search directories.</value>
        /// TODO Edit XML Comment Template for SearchDirectories
        private HashSet<DirectoryInfo> SearchDirectories
        {
            get;
        } = new HashSet<DirectoryInfo>();

        /// <summary>
        ///     Gets a value indicating whether [should exclude
        ///     extensions].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [should exclude extensions];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ShouldExcludeExtensions
        private bool ShouldExcludeExtensions
        {
            get;
        }

        /// <summary>
        ///     Gets the file system size infos.
        /// </summary>
        /// <returns>HashSet&lt;FileSystemSizeInfo&gt;.</returns>
        /// TODO Edit XML Comment Template for GetFileSystemSizeInfos
        internal HashSet<FileSystemSizeInfo> GetFileSystemSizeInfos()
        {
            FileSystemSizeInfos = new HashSet<FileSystemSizeInfo>();

            foreach (var directory in SearchDirectories)
            {
                CancellationToken.ThrowIfCancellationRequested();

                try
                {
                    GetFileSystemInfos(directory, RootLevel);
                }
                catch (OperationCanceledException)
                {
                    FileSystemSizeInfos = null;
                    throw;
                }
            }

            return FileSystemSizeInfos;
        }

        /// <summary>
        ///     Gets the file system infos.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="currentLevel">The current level.</param>
        /// TODO Edit XML Comment Template for GetFileSystemInfos
        private void GetFileSystemInfos(
            DirectoryInfo directory,
            int currentLevel)
        {
            try
            {
                if (!ExcludedPaths.IsNullOrEmpty()
                    && ExcludedPaths.ContainsIgnoreCase(directory.FullName))
                {
                    return;
                }

                try
                {
                    var directorySizeInfo = new FileSystemSizeInfo(directory);

                    if (MinFolderSize != null
                        && directorySizeInfo.Size < MinFolderSize.Value
                        || MaxFolderSize != null
                        && directorySizeInfo.Size > MaxFolderSize.Value
                        || MinFolderContents != null
                        && directorySizeInfo.ContentsCount
                        < MinFolderContents.Value
                        || MaxFolderContents != null
                        && directorySizeInfo.ContentsCount
                        > MaxFolderContents.Value)
                    {
                        return;
                    }

                    if (directorySizeInfo.PathLevels > MaxPathLevels)
                    {
                        MaxPathLevels = directorySizeInfo.PathLevels;
                    }

                    FileSystemSizeInfos.Add(directorySizeInfo);
                }
                catch (UnauthorizedAccessException)
                {
                }

                foreach (var fileSystemInfo in directory.GetFileSystemInfos())
                {
                    CancellationToken.ThrowIfCancellationRequested();

                    var fileInfo = fileSystemInfo as FileInfo;

                    if (fileInfo != null)
                    {
                        if (MinFileSize != null
                            && fileInfo.Length < MinFileSize.Value
                            || MaxFileSize != null
                            && fileInfo.Length > MaxFileSize.Value
                            || ShouldExcludeExtensions
                            && Extensions.ContainsIgnoreCase(fileInfo.Extension)
                            || !ShouldExcludeExtensions
                            && !Extensions.ContainsIgnoreCase(
                                fileInfo.Extension))
                        {
                            continue;
                        }

                        var fileSizeInfo = new FileSystemSizeInfo(fileInfo);

                        if (fileSizeInfo.PathLevels > MaxPathLevels)
                        {
                            MaxPathLevels = fileSizeInfo.PathLevels;
                        }

                        FileSystemSizeInfos.Add(fileSizeInfo);
                    }

                    var directoryInfo = fileSystemInfo as DirectoryInfo;

                    if (directoryInfo == null
                        || Scope == Scope.NoChildren
                        || Scope == Scope.ImmediateChildren && currentLevel > 0)
                    {
                        continue;
                    }

                    GetFileSystemInfos(directoryInfo, currentLevel + 1);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
    }
}