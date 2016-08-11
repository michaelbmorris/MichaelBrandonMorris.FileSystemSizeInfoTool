using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MichaelBrandonMorris.Extensions.CollectionExtensions;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    internal class FileSystemSizeInfoGetter
    {
        private const int BytesInMegaByte = 1000000;
        private const int RootLevel = 0;

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
            MinFileSize = minFileSize*BytesInMegaByte;
            MaxFileSize = maxFileSize*BytesInMegaByte;
            MinFolderSize = minFolderSize*BytesInMegaByte;
            MaxFolderSize = maxFolderSize*BytesInMegaByte;
            MinFolderContents = minFolderContents*BytesInMegaByte;
            MaxFolderContents = maxFolderContents*BytesInMegaByte;
        }

        private CancellationToken CancellationToken
        {
            get;
        }

        internal HashSet<FileSystemSizeInfo> FileSystemSizeInfos
        {
            get;
            set;
        }

        internal int MaxPathLevels
        {
            get;
            set;
        }

        private IEnumerable<string> ExcludedPaths
        {
            get;
        }

        private IEnumerable<string> Extensions
        {
            get;
        }

        private long? MaxFileSize
        {
            get;
        }

        private long? MaxFolderContents
        {
            get;
        }

        private long? MaxFolderSize
        {
            get;
        }

        private long? MinFileSize
        {
            get;
        }

        private long? MinFolderContents
        {
            get;
        }

        private long? MinFolderSize
        {
            get;
        }

        private Scope Scope
        {
            get;
        }

        private HashSet<DirectoryInfo> SearchDirectories
        {
            get;
        } = new HashSet<DirectoryInfo>();

        private bool ShouldExcludeExtensions
        {
            get;
        }

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

        private void GetFileSystemInfos(
            DirectoryInfo directory, int currentLevel)
        {
            try
            {
                if (!ExcludedPaths.IsNullOrEmpty() &&
                    ExcludedPaths.ContainsIgnoreCase(directory.FullName))
                {
                    return;
                }

                try
                {
                    var directorySizeInfo = new FileSystemSizeInfo(directory);

                    if ((MinFolderSize != null &&
                     directorySizeInfo.Size < MinFolderSize.Value) ||
                    (MaxFolderSize != null &&
                     directorySizeInfo.Size > MaxFolderSize.Value) ||
                    (MinFolderContents != null &&
                     directorySizeInfo.ContentsCount < MinFolderContents.Value) ||
                    (MaxFolderContents != null &&
                     directorySizeInfo.ContentsCount > MaxFolderContents.Value))
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
                        if ((MinFileSize != null &&
                             fileInfo.Length < MinFileSize.Value) ||
                            (MaxFileSize != null &&
                             fileInfo.Length > MaxFileSize.Value) ||
                            (ShouldExcludeExtensions &&
                             Extensions.ContainsIgnoreCase(
                                 fileInfo.Extension)) ||
                            (!ShouldExcludeExtensions &&
                             !Extensions.ContainsIgnoreCase(
                                 fileInfo.Extension)))
                        {
                            continue;
                        }

                        var fileSizeInfo = new FileSystemSizeInfo(fileInfo);

                        if (fileSizeInfo.PathLevels > MaxPathLevels)
                        {
                            MaxPathLevels = fileSizeInfo.PathLevels;
                        }

                        FileSystemSizeInfos.Add(
                            fileSizeInfo);
                    }

                    var directoryInfo = fileSystemInfo as DirectoryInfo;

                    if (directoryInfo == null ||
                        Scope == Scope.NoChildren ||
                        (Scope == Scope.ImmediateChildren &&
                         currentLevel > 0))
                    {
                        continue;
                    }

                    GetFileSystemInfos(directoryInfo, currentLevel + 1);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}