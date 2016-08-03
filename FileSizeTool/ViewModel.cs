using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MichaelBrandonMorris.DynamicText;

namespace MichaelBrandonMorris.FileSizeTool
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private bool _combinedPathsIsChecked;
        private bool _excludedExtensionsIsChecked;
        private bool _includedExtensionsIsChecked;
        private bool _isBusy;
        private int _maxFileSize;
        private bool _maxFileSizeIsChecked;
        private int _maxFolderContents;
        private bool _maxFolderContentsIsChecked;
        private int _maxFolderSize;
        private bool _maxFolderSizeIsChecked;
        private string _message;
        private bool _messageIsVisible;
        private int _messageZIndex;
        private int _minFileSize;
        private bool _minFileSizeIsChecked;
        private int _minFolderContents;
        private bool _minFolderContentsIsChecked;
        private int _minFolderSize;
        private bool _minFolderSizeIsChecked;
        private bool _scopeAllChildrenIsChecked;
        private bool _scopeImmediateChildrenIsChecked;
        private bool _scopeNoChildrenIsChecked;
        private bool _splitPathsIsChecked;

        public ICommand AddExcludedExtension => new RelayCommand(
            ExecuteAddExcludedExtension, CanAddExcludedExtension);

        public ICommand AddExcludedPath => new RelayCommand(
            ExecuteAddExcludedPath, CanAddExcludedPath);

        public ICommand AddSearchPath => new RelayCommand(
            ExecuteAddSearchPath, CanAddSearchPath);

        public ICommand Cancel => new RelayCommand(ExecuteCancel, CanCancel);

        public bool CombinedPathsIsChecked
        {
            get
            {
                return _combinedPathsIsChecked;
            }
            set
            {
                if (_combinedPathsIsChecked == value)
                {
                    return;
                }

                _combinedPathsIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public DynamicTextCollection ExcludedExtensions
        {
            get;
        } = new DynamicTextCollection();

        public bool ExcludedExtensionsIsChecked
        {
            get
            {
                return _excludedExtensionsIsChecked;
            }
            set
            {
                if (_excludedExtensionsIsChecked)
                {
                    return;
                }

                _excludedExtensionsIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<DynamicDirectoryPath> ExcludedPaths
        {
            get;
        } = new DynamicDirectoryPathCollection();

        public ICommand GetFileSizeInfo => new RelayCommand(
            ExecuteGetFileSizeInfo, CanGetFileSizeInfo);

        public DynamicTextCollection IncludedExtensions
        {
            get;
        } = new DynamicTextCollection();

        public bool IncludedExtensionsIsChecked
        {
            get
            {
                return _includedExtensionsIsChecked;
            }
            set
            {
                if (_includedExtensionsIsChecked == value)
                {
                    return;
                }

                _includedExtensionsIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy == value)
                {
                    return;
                }

                _isBusy = value;
                NotifyPropertyChanged();
            }
        }

        public int MaxFileSize
        {
            get
            {
                return _maxFileSize;
            }
            set
            {
                if (_maxFileSize == value)
                {
                    return;
                }

                _maxFileSize = value;
                NotifyPropertyChanged();
            }
        }

        public bool MaxFileSizeIsChecked
        {
            get
            {
                return _maxFileSizeIsChecked;
            }
            set
            {
                if (_maxFileSizeIsChecked == value)
                {
                    return;
                }

                _maxFileSizeIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public int MaxFolderContents
        {
            get
            {
                return _maxFolderContents;
            }
            set
            {
                if (_maxFolderContents == value)
                {
                    return;
                }

                _maxFolderContents = value;
                NotifyPropertyChanged();
            }
        }

        public bool MaxFolderContentsIsChecked
        {
            get
            {
                return _maxFolderContentsIsChecked;
            }
            set
            {
                if (_maxFolderContentsIsChecked == value)
                {
                    return;
                }

                _maxFolderContentsIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public int MaxFolderSize
        {
            get
            {
                return _maxFolderSize;
            }
            set
            {
                if (_maxFolderSize == value)
                {
                    return;
                }

                _maxFolderSize = value;
                NotifyPropertyChanged();
            }
        }

        public bool MaxFolderSizeIsChecked
        {
            get
            {
                return _maxFolderSizeIsChecked;
            }
            set
            {
                if (_maxFolderSizeIsChecked == value)
                {
                    return;
                }

                _maxFolderSizeIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message == value)
                {
                    return;
                }

                _message = value;
                NotifyPropertyChanged();
            }
        }

        public bool MessageIsVisible
        {
            get
            {
                return _messageIsVisible;
            }
            set
            {
                if (_messageIsVisible == value)
                {
                    return;
                }

                _messageIsVisible = value;
                NotifyPropertyChanged();
            }
        }

        public int MessageZIndex
        {
            get
            {
                return _messageZIndex;
            }
            set
            {
                if (_messageZIndex == value)
                {
                    return;
                }

                _messageZIndex = value;
                NotifyPropertyChanged();
            }
        }

        public int MinFileSize
        {
            get
            {
                return _minFileSize;
            }
            set
            {
                if (_minFileSize == value)
                {
                    return;
                }

                _minFileSize = value;
                NotifyPropertyChanged();
            }
        }

        public bool MinFileSizeIsChecked
        {
            get
            {
                return _minFileSizeIsChecked;
            }
            set
            {
                if (_minFileSizeIsChecked == value)
                {
                    return;
                }

                _minFileSizeIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public int MinFolderContents
        {
            get
            {
                return _minFolderContents;
            }
            set
            {
                if (_minFolderContents == value)
                {
                    return;
                }

                _minFolderContents = value;
                NotifyPropertyChanged();
            }
        }

        public bool MinFolderContentsIsChecked
        {
            get
            {
                return _minFolderContentsIsChecked;
            }
            set
            {
                if (_minFolderContentsIsChecked == value)
                {
                    return;
                }

                _minFolderContentsIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public int MinFolderSize
        {
            get
            {
                return _minFolderSize;
            }
            set
            {
                if (_minFolderSize == value)
                {
                    return;
                }

                _minFolderSize = value;
                NotifyPropertyChanged();
            }
        }

        public bool MinFolderSizeIsChecked
        {
            get
            {
                return _minFolderSizeIsChecked;
            }
            set
            {
                if (_minFolderSizeIsChecked == value)
                {
                    return;
                }

                _minFolderSizeIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand OpenAboutWindow => new RelayCommand(
            ExecuteOpenAboutWindow);

        public ICommand OpenUserGuide => new RelayCommand(
            ExecuteOpenUserGuide);

        public ICommand RemoveExcludedExtension =>
            new RelayCommand<DynamicText.DynamicText>(
                ExecuteRemoveExcludedExtension, CanRemoveExcludedExtension);

        public ICommand RemoveExcludedPath =>
            new RelayCommand<DynamicDirectoryPath>(
                ExecuteRemoveExcludedPath, CanRemoveExcludedPath);

        public ICommand RemoveSearchPath =>
            new RelayCommand<DynamicDirectoryPath>(
                ExecuteRemoveSearchPath, CanRemoveSearchPath);

        public bool ScopeAllChildrenIsChecked
        {
            get
            {
                return _scopeAllChildrenIsChecked;
            }
            set
            {
                if (_scopeAllChildrenIsChecked == value)
                {
                    return;
                }

                _scopeAllChildrenIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public bool ScopeImmediateChildrenIsChecked
        {
            get
            {
                return _scopeImmediateChildrenIsChecked;
            }
            set
            {
                if (_scopeImmediateChildrenIsChecked == value)
                {
                    return;
                }

                _scopeImmediateChildrenIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public bool ScopeNoChildrenIsChecked
        {
            get
            {
                return _scopeNoChildrenIsChecked;
            }
            set
            {
                if (_scopeNoChildrenIsChecked == value)
                {
                    return;
                }

                _scopeNoChildrenIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public DynamicDirectoryPathCollection SearchPaths
        {
            get;
        } = new DynamicDirectoryPathCollection();

        public bool SplitPathsIsChecked
        {
            get
            {
                return _splitPathsIsChecked;
            }
            set
            {
                if (_splitPathsIsChecked == value)
                {
                    return;
                }

                _splitPathsIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Scope GetSelectedScope()
        {
            if (ScopeAllChildrenIsChecked)
            {
                return Scope.AllChildren;
            }

            if (ScopeImmediateChildrenIsChecked)
            {
                return Scope.ImmediateChildren;
            }

            if (ScopeNoChildrenIsChecked)
            {
                return Scope.NoChildren;
            }

            throw new ArgumentOutOfRangeException();
        }

        private bool CanAddExcludedExtension()
        {
            return ExcludedExtensions.All(x => !x.IsNullOrWhiteSpace());
        }

        private bool CanAddExcludedPath()
        {
            return ExcludedPaths.All(x => !x.IsNullOrWhiteSpace());
        }

        private bool CanAddSearchPath()
        {
            return SearchPaths.All(x => !x.IsNullOrWhiteSpace());
        }

        private bool CanCancel()
        {
            return IsBusy;
        }

        // TODO
        private bool CanGetFileSizeInfo()
        {
            return SearchPaths.ContainsText() && PathDisplayOptionIsChecked();
        }

        private bool CanRemoveExcludedExtension(
            DynamicText.DynamicText excludedExtension)
        {
            return ExcludedExtensions.Count > 1 ||
                   ExcludedExtensions.All(x => !x.IsNullOrWhiteSpace());
        }

        private bool CanRemoveExcludedPath(
            DynamicDirectoryPath excludedPath = null)
        {
            return ExcludedPaths.Count > 1 ||
                   ExcludedPaths.All(x => !x.IsNullOrWhiteSpace());
        }

        private bool CanRemoveSearchPath(
            DynamicDirectoryPath searchPath = null)
        {
            return SearchPaths.Count > 1 ||
                   SearchPaths.All(x => !x.IsNullOrWhiteSpace());
        }

        private void ExecuteAddExcludedExtension()
        {
        }

        private void ExecuteAddExcludedPath()
        {
            ExcludedPaths.Add(new DynamicDirectoryPath());
        }

        private void ExecuteAddSearchPath()
        {
            SearchPaths.Add(new DynamicDirectoryPath());
        }

        private void ExecuteCancel()
        {
        }

        private async void ExecuteGetFileSizeInfo()
        {
            var searchPaths = from x in SearchPaths
                where !x.IsNullOrWhiteSpace()
                select x.Text;

            var excludedPaths = from x in ExcludedPaths
                where !x.IsNullOrWhiteSpace()
                select x.Text;

            var minFileSize = MinFileSizeIsChecked
                ? (int?) MinFileSize
                : null;

            var maxFileSize = MaxFileSizeIsChecked ? (int?) MaxFileSize : null;

            var minFolderSize = MinFolderSizeIsChecked
                ? (int?) MinFolderSize
                : null;

            var maxFolderSize = MaxFolderSizeIsChecked
                ? (int?) MaxFolderSize
                : null;

            var minFolderContents = MinFolderContentsIsChecked
                ? (int?) MinFolderContents
                : null;

            var maxFolderContents = MaxFolderContentsIsChecked
                ? (int?) MaxFolderContents
                : null;

            var fileSizeChecker = new FileSizeChecker(
                searchPaths,
                excludedPaths,
                GetSelectedScope(),
                SplitPathsIsChecked,
                minFileSize,
                maxFileSize,
                minFolderSize,
                maxFolderSize,
                minFolderContents,
                maxFolderContents);

            await fileSizeChecker.Execute();
        }

        // TODO
        private void ExecuteOpenAboutWindow()
        {
            throw new NotImplementedException();
        }

        // TODO
        private void ExecuteOpenUserGuide()
        {
            throw new NotImplementedException();
        }

        private void ExecuteRemoveExcludedExtension(
            DynamicText.DynamicText excludedExtension)
        {
        }

        private void ExecuteRemoveExcludedPath(
            DynamicDirectoryPath excludedPath)
        {
            if (ExcludedPaths.Count > 1)
            {
                ExcludedPaths.Remove(excludedPath);
            }
            else
            {
                ExcludedPaths.First().Text = string.Empty;
            }
        }

        private void ExecuteRemoveSearchPath(
            DynamicDirectoryPath searchPath)
        {
            if (SearchPaths.Count > 1)
            {
                SearchPaths.Remove(searchPath);
            }
            else
            {
                ExcludedPaths.First().Text = string.Empty;
            }
        }

        private void NotifyPropertyChanged(
            [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        private bool PathDisplayOptionIsChecked()
        {
            return CombinedPathsIsChecked || SplitPathsIsChecked;
        }
    }
}