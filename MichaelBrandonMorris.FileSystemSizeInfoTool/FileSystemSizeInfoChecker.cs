using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    /// <summary>
    ///     Enum Scope
    /// </summary>
    /// TODO Edit XML Comment Template for Scope
    internal enum Scope
    {
        /// <summary>
        ///     The no children
        /// </summary>
        /// TODO Edit XML Comment Template for NoChildren
        NoChildren,

        /// <summary>
        ///     The immediate children
        /// </summary>
        /// TODO Edit XML Comment Template for ImmediateChildren
        ImmediateChildren,

        /// <summary>
        ///     All children
        /// </summary>
        /// TODO Edit XML Comment Template for AllChildren
        AllChildren
    }

    /// <summary>
    ///     Class FileSystemSizeInfoChecker.
    /// </summary>
    /// TODO Edit XML Comment Template for FileSystemSizeInfoChecker
    internal class FileSystemSizeInfoChecker
    {
        /// <summary>
        ///     The CSV extension
        /// </summary>
        /// TODO Edit XML Comment Template for CsvExtension
        private const string CsvExtension = ".csv";

        /// <summary>
        ///     The output path
        /// </summary>
        /// TODO Edit XML Comment Template for OutputPath
        private static readonly string OutputPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "FileSizeTool");

        /// <summary>
        ///     The results file name
        /// </summary>
        /// TODO Edit XML Comment Template for ResultsFileName
        private const string ResultsFileName = "FileSizeInfo";

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="FileSystemSizeInfoChecker" /> class.
        /// </summary>
        /// <param name="searchPaths">The search paths.</param>
        /// <param name="excludedPaths">The excluded paths.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="shouldSplitPaths">
        ///     if set to <c>true</c>
        ///     [should split paths].
        /// </param>
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
        internal FileSystemSizeInfoChecker(
            IEnumerable<string> searchPaths,
            IEnumerable<string> excludedPaths,
            Scope scope,
            bool shouldSplitPaths,
            bool shouldExcludeExtensions,
            IEnumerable<string> extensions = null,
            int? minFileSize = null,
            int? maxFileSize = null,
            int? minFolderSize = null,
            int? maxFolderSize = null,
            int? minFolderContents = null,
            int? maxFolderContents = null)
        {
            FileSystemSizeInfoGetter = new FileSystemSizeInfoGetter(
                CancellationToken,
                searchPaths,
                excludedPaths,
                scope,
                shouldExcludeExtensions,
                extensions,
                minFileSize,
                maxFileSize,
                minFolderSize,
                maxFolderSize,
                minFolderContents,
                maxFolderContents);

            ShouldSplitPaths = shouldSplitPaths;
        }

        /// <summary>
        ///     Gets the cancellation token.
        /// </summary>
        /// <value>The cancellation token.</value>
        /// TODO Edit XML Comment Template for CancellationToken
        private CancellationToken CancellationToken => CancellationTokenSource
            .Token;

        /// <summary>
        ///     Gets the cancellation token source.
        /// </summary>
        /// <value>The cancellation token source.</value>
        /// TODO Edit XML Comment Template for CancellationTokenSource
        private CancellationTokenSource CancellationTokenSource
        {
            get;
        } = new CancellationTokenSource();

        /// <summary>
        ///     Gets the file system size information getter.
        /// </summary>
        /// <value>The file system size information getter.</value>
        /// TODO Edit XML Comment Template for FileSystemSizeInfoGetter
        private FileSystemSizeInfoGetter FileSystemSizeInfoGetter
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
        ///     Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        /// TODO Edit XML Comment Template for Results
        private string Results
        {
            get;
            set;
        }

        /// <summary>
        ///     Cancels this instance.
        /// </summary>
        /// TODO Edit XML Comment Template for Cancel
        internal void Cancel()
        {
            CancellationTokenSource.Cancel();
        }

        /// <summary>
        ///     Executes this instance.
        /// </summary>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for Execute
        internal async Task Execute()
        {
            var task = Task.Run(
                () =>
                {
                    var fileSystemSizeInfos =
                        FileSystemSizeInfoGetter.GetFileSystemSizeInfos();

                    Results = new FileSystemSizeInfoFormatter(
                            CancellationTokenSource.Token,
                            fileSystemSizeInfos,
                            ShouldSplitPaths,
                            FileSystemSizeInfoGetter.MaxPathLevels)
                        .GetFormattedFileSystemSizeInfos();
                },
                CancellationToken);

            await task;

            var now = DateTime.Now;
            WriteAndOpenResults(now);
        }

        /// <summary>
        ///     Writes the and open results.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// TODO Edit XML Comment Template for WriteAndOpenResults
        private void WriteAndOpenResults(DateTime timeStamp)
        {
            var resultsFileName = $"{ResultsFileName} - "
                                  + $"{timeStamp:yyyyMMddTHHmmss}{CsvExtension}";

            var resultsFullName = Path.Combine(OutputPath, resultsFileName);
            Directory.CreateDirectory(OutputPath);
            File.WriteAllText(resultsFullName, Results);
            Process.Start(resultsFullName);
        }
    }
}