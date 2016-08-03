using System.Collections.Generic;
using System.Text;

namespace MichaelBrandonMorris.FileSizeTool
{
    internal class FileSystemSizeInfoFormatter
    {
        private const string ResultNumberHeader = "#";
        private const string LevelHeader = "Level";
        private const string FileExtensionHeader = "File Extension";
        private const string FolderContentsHeader = "# Folders";
        private const string FileContentsHeader = "# Files";
        private const string FolderSizeHeader = "Size";

        private IEnumerable<FileSystemSizeInfo> FileSystemSizeInfos
        {
            get;
        }

        private bool ShouldSplitPaths
        {
            get;
        }

        private int? MaxPathLevels
        {
            get;
        }

        internal FileSystemSizeInfoFormatter(
            IEnumerable<FileSystemSizeInfo> fileSystemSizeInfos,
            bool shouldSplitPaths,
            int? maxPathLevels)
        {
            FileSystemSizeInfos = fileSystemSizeInfos;
            ShouldSplitPaths = shouldSplitPaths;
            MaxPathLevels = maxPathLevels;
        }

        internal string GetFormattedFileSystemSizeInfos()
        {
            var formattedFileSystemSizeInfos = new StringBuilder();
            var header = new StringBuilder();
            header.Append(ResultNumberHeader);

            if (ShouldSplitPaths && MaxPathLevels != null)
            {
                for (var i = 0; i < MaxPathLevels; i++)
                {
                    header.Append($"{LevelHeader} {i}");
                }
            }

            header.Append(FileExtensionHeader);

            foreach (var fileSystemSizeInfo in FileSystemSizeInfos)
            {
                formattedFileSystemSizeInfos.AppendLine(
                    fileSystemSizeInfo.FileSystemInfo.FullName);
            }

            return formattedFileSystemSizeInfos.ToString();
        }
    }
}
