using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using MichaelBrandonMorris.Extensions.OtherExtensions;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    /// <summary>
    ///     Class FileSystemSizeInfoFormatter.
    /// </summary>
    /// TODO Edit XML Comment Template for FileSystemSizeInfoFormatter
    internal class FileSystemSizeInfoFormatter
    {
        /// <summary>
        ///     The file contents header
        /// </summary>
        /// TODO Edit XML Comment Template for FileContentsHeader
        private const string FileContentsHeader = "# Files";

        /// <summary>
        ///     The file extension header
        /// </summary>
        /// TODO Edit XML Comment Template for FileExtensionHeader
        private const string FileExtensionHeader = "File Extension";

        /// <summary>
        ///     The folder contents header
        /// </summary>
        /// TODO Edit XML Comment Template for FolderContentsHeader
        private const string FolderContentsHeader = "# Folders";

        /// <summary>
        ///     The level header
        /// </summary>
        /// TODO Edit XML Comment Template for LevelHeader
        private const string LevelHeader = "Level";

        /// <summary>
        ///     The maximum results
        /// </summary>
        /// TODO Edit XML Comment Template for MaxResults
        private const int MaxResults = 1048574;

        /// <summary>
        ///     The owner header
        /// </summary>
        /// TODO Edit XML Comment Template for OwnerHeader
        private const string OwnerHeader = "Owner";

        /// <summary>
        ///     The path header
        /// </summary>
        /// TODO Edit XML Comment Template for PathHeader
        private const string PathHeader = "Path";

        /// <summary>
        ///     The result number header
        /// </summary>
        /// TODO Edit XML Comment Template for ResultNumberHeader
        private const string ResultNumberHeader = "#";

        /// <summary>
        ///     The size header
        /// </summary>
        /// TODO Edit XML Comment Template for SizeHeader
        private const string SizeHeader = "Size (MB)";

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="FileSystemSizeInfoFormatter" /> class.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="fileSystemSizeInfos">
        ///     The file system size
        ///     infos.
        /// </param>
        /// <param name="shouldSplitPaths">
        ///     if set to <c>true</c>
        ///     [should split paths].
        /// </param>
        /// <param name="maxPathLevels">The maximum path levels.</param>
        /// TODO Edit XML Comment Template for #ctor
        internal FileSystemSizeInfoFormatter(
            CancellationToken cancellationToken,
            IEnumerable<FileSystemSizeInfo> fileSystemSizeInfos,
            bool shouldSplitPaths,
            int? maxPathLevels)
        {
            CancellationToken = cancellationToken;
            FileSystemSizeInfos = fileSystemSizeInfos;
            ShouldSplitPaths = shouldSplitPaths;
            MaxPathLevels = maxPathLevels;
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
        ///     Gets the file system size infos.
        /// </summary>
        /// <value>The file system size infos.</value>
        /// TODO Edit XML Comment Template for FileSystemSizeInfos
        private IEnumerable<FileSystemSizeInfo> FileSystemSizeInfos
        {
            get;
        }

        /// <summary>
        ///     Gets the maximum path levels.
        /// </summary>
        /// <value>The maximum path levels.</value>
        /// TODO Edit XML Comment Template for MaxPathLevels
        private int? MaxPathLevels
        {
            get;
        }

        /// <summary>
        ///     Gets a value indicating whether [should split paths].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [should split paths]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ShouldSplitPaths
        private bool ShouldSplitPaths
        {
            get;
        }

        /// <summary>
        ///     Gets the formatted file system size infos.
        /// </summary>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetFormattedFileSystemSizeInfos
        internal string GetFormattedFileSystemSizeInfos()
        {
            var formattedFileSystemSizeInfos = new StringBuilder();
            var header = new StringBuilder();
            header.AppendCsv(ResultNumberHeader);

            if (ShouldSplitPaths && MaxPathLevels != null)
            {
                for (var i = 0; i < MaxPathLevels; i++)
                {
                    CancellationToken.ThrowIfCancellationRequested();
                    header.AppendCsv($"{LevelHeader} {i}");
                }
            }
            else
            {
                header.AppendCsv(PathHeader);
            }

            header.AppendCsv(FileExtensionHeader);
            header.AppendCsv(FolderContentsHeader);
            header.AppendCsv(FileContentsHeader);
            header.AppendCsv(SizeHeader);
            header.AppendCsv(OwnerHeader);

            formattedFileSystemSizeInfos.AppendLine(header.ToString());

            var resultsCount = 0;

            foreach (var fileSystemSizeInfo in FileSystemSizeInfos)
            {
                CancellationToken.ThrowIfCancellationRequested();

                if (resultsCount == MaxResults)
                {
                    break;
                }

                formattedFileSystemSizeInfos.AppendCsv(resultsCount.ToString());

                if (ShouldSplitPaths && MaxPathLevels != null)
                {
                    for (var i = 0; i < MaxPathLevels.Value; i++)
                    {
                        CancellationToken.ThrowIfCancellationRequested();

                        formattedFileSystemSizeInfos.AppendCsv(
                            i < fileSystemSizeInfo.PathLevels
                                ? fileSystemSizeInfo.FullNameSplitPath[i]
                                : string.Empty);
                    }
                }
                else
                {
                    formattedFileSystemSizeInfos.AppendCsv(
                        fileSystemSizeInfo.FullName);
                }

                formattedFileSystemSizeInfos.AppendCsv(
                    fileSystemSizeInfo.Extension);

                formattedFileSystemSizeInfos.AppendCsv(
                    fileSystemSizeInfo.FoldersCount.ToString());

                formattedFileSystemSizeInfos.AppendCsv(
                    fileSystemSizeInfo.FilesCount.ToString());

                formattedFileSystemSizeInfos.AppendCsv(
                    fileSystemSizeInfo.Size.ToString(
                        CultureInfo.InvariantCulture));

                formattedFileSystemSizeInfos
                    .AppendCsv(fileSystemSizeInfo.Owner);

                formattedFileSystemSizeInfos.AppendLine();
                resultsCount++;
            }

            return formattedFileSystemSizeInfos.ToString();
        }
    }
}