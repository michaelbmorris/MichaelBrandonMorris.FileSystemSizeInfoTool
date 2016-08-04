using System.IO;
using System.Linq;
using System.Security.Principal;
using Extensions.PrimitiveExtensions;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    internal class FileSystemSizeInfo
    {
        internal int FilesCount
        {
            get;
            set;
        }

        internal int FoldersCount
        {
            get;
            set;
        }

        internal long Size
        {
            get;
            set;
        }

        internal int PathLevels => FullNameSplitPath.Length;

        internal int ContentsCount => FilesCount + FoldersCount;

        internal FileSystemInfo FileSystemInfo
        {
            get;
        }

        internal string[] FullNameSplitPath
        {
            get;
        }

        internal string FullName
        {
            get;
        }

        internal string Owner
        {
            get;
        }

        internal FileSystemSizeInfo(FileInfo file)
        {
            FilesCount = 0;
            FileSystemInfo = file;
            FoldersCount = 0;
            FullName = file.FullName;

            FullNameSplitPath = file.FullName.Split(
                Path.DirectorySeparatorChar).Where(
                    s => !s.IsNullOrWhiteSpace()).ToArray();

            try
            {
                Owner = file.GetAccessControl().GetOwner(
                    typeof(NTAccount)).ToString();
            }
            catch (IdentityNotMappedException)
            {
                Owner = string.Empty;
            }

            Size = file.Length;
        }

        internal FileSystemSizeInfo(DirectoryInfo directory)
        {
            GetContentsAndSize(directory);
            FileSystemInfo = directory;
            FullName = directory.FullName;
            FullNameSplitPath = directory.FullName.Split(
                Path.DirectorySeparatorChar).Where(
                    s => !s.IsNullOrWhiteSpace()).ToArray();

            try
            { 
                Owner = directory.GetAccessControl().GetOwner(
                    typeof(NTAccount)).ToString();
            }
            catch (IdentityNotMappedException)
            {
                Owner = string.Empty;
            }
        }

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
                Size += fileInfo.Length;
            }
        }
    }
}
