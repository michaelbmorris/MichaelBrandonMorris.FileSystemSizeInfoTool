using System;
using System.Collections.Generic;
using System.IO;
using Extensions.CollectionExtensions;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    internal class FileSystemSizeInfoGetter
    {
        private const int RootLevel = 0;
        private const int BytesInMegaByte = 1000000;

        private HashSet<DirectoryInfo> SearchDirectories
        {
            get;
        } = new HashSet<DirectoryInfo>();

        private IEnumerable<string> ExcludedPaths
        {
            get;
        }

        internal HashSet<FileSystemSizeInfo> FileSystemSizeInfos
        {
            get;
            set;
        }

        private long? MinFileSize
        {
            get;
        }

        private long? MaxFileSize
        {
            get;
        }

        private long? MinFolderSize
        {
            get;
        }

        private long? MaxFolderSize
        {
            get;
        }

        private long? MinFolderContents
        {
            get;
        }

        private long? MaxFolderContents
        {
            get;
        }

        private Scope Scope
        {
            get;
        }

        internal int MaxPathLevels
        {
            get;
            set;
        }

        internal FileSystemSizeInfoGetter(
            IEnumerable<string> searchPaths,
            IEnumerable<string> excludedPaths,
            Scope scope,
            int? minFileSize = null,
            int? maxFileSize = null,
            int? minFolderSize = null,
            int? maxFolderSize = null,
            int? minFolderContents = null,
            int? maxFolderContents = null)
        {
            foreach (var searchPath in searchPaths)
            {
                SearchDirectories.Add(new DirectoryInfo(searchPath));
            }

            ExcludedPaths = excludedPaths;
            Scope = scope;
            MinFileSize = minFileSize * BytesInMegaByte;
            MaxFileSize = maxFileSize * BytesInMegaByte;
            MinFolderSize = minFolderSize * BytesInMegaByte;
            MaxFolderSize = maxFolderSize * BytesInMegaByte;
            MinFolderContents = minFolderContents * BytesInMegaByte;
            MaxFolderContents = maxFolderContents * BytesInMegaByte;
        }

        internal HashSet<FileSystemSizeInfo> GetFileSystemSizeInfos()
        {
            FileSystemSizeInfos = new HashSet<FileSystemSizeInfo>();

            foreach (var directory in SearchDirectories)
            {
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

                foreach (var fileSystemInfo in directory.GetFileSystemInfos())
                {
                    var fileInfo = fileSystemInfo as FileInfo;

                    if (fileInfo != null)
                    {
                        if ((MinFileSize != null &&
                             fileInfo.Length < MinFileSize.Value) ||
                             (MaxFileSize != null && 
                             fileInfo.Length > MaxFileSize.Value))
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
