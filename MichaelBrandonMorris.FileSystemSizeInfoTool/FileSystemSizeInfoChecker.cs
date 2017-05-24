using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    internal enum Scope
    {
        NoChildren,
        ImmediateChildren,
        AllChildren
    }

    internal class FileSystemSizeInfoChecker
    {
        private const string CsvExtension = ".csv";

        private static readonly string OutputPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "FileSizeTool");

        private const string ResultsFileName = "FileSizeInfo";

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

        private CancellationToken CancellationToken
        {
            get
            {
                return CancellationTokenSource.Token;
            }
        }

        private CancellationTokenSource CancellationTokenSource
        {
            get;
        } = new CancellationTokenSource();

        private FileSystemSizeInfoGetter FileSystemSizeInfoGetter
        {
            get;
        }

        private bool ShouldSplitPaths
        {
            get;
        }

        private string Results
        {
            get;
            set;
        }

        internal void Cancel()
        {
            CancellationTokenSource.Cancel();
        }

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

        private void WriteAndOpenResults(DateTime timeStamp)
        {
            var resultsFileName =
                $"{ResultsFileName} - "
                + $"{timeStamp:yyyyMMddTHHmmss}{CsvExtension}";

            var resultsFullName = Path.Combine(OutputPath, resultsFileName);
            Directory.CreateDirectory(OutputPath);
            File.WriteAllText(resultsFullName, Results);
            Process.Start(resultsFullName);
        }
    }
}