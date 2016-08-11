using System.IO;
using System.Linq;
using System.Security.Principal;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    internal class FileSystemSizeInfo
    {
        private const double BytesInMegaByte = 1000000.0;

        internal FileSystemSizeInfo(FileInfo file)
        {
            Extension = file.Extension;
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

            SizeBytes = file.Length;
        }

        internal FileSystemSizeInfo(DirectoryInfo directory)
        {
            Extension = string.Empty;
            FileSystemInfo = directory;
            FullName = directory.FullName;
            FullNameSplitPath = directory.FullName.Split(
                Path.DirectorySeparatorChar).Where(
                    s => !s.IsNullOrWhiteSpace()).ToArray();

            GetContentsAndSize(directory);

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

        public double Size => SizeBytes/BytesInMegaByte;

        internal int ContentsCount => FilesCount + FoldersCount;

        internal string Extension
        {
            get;
        }

        internal int FilesCount
        {
            get;
            set;
        }

        internal FileSystemInfo FileSystemInfo
        {
            get;
        }

        internal int FoldersCount
        {
            get;
            set;
        }

        internal string FullName
        {
            get;
        }

        internal string[] FullNameSplitPath
        {
            get;
        }

        internal string Owner
        {
            get;
        }

        internal int PathLevels => FullNameSplitPath.Length;

        private long SizeBytes
        {
            get;
            set;
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
                SizeBytes += fileInfo.Length;
            }
        }
    }
}