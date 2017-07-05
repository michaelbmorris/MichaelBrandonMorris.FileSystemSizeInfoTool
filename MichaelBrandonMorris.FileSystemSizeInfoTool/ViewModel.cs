using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MichaelBrandonMorris.DynamicText;
using static System.IO.Path;

namespace MichaelBrandonMorris.FileSystemSizeInfoTool
{
    /// <summary>
    ///     Class ViewModel.
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    /// TODO Edit XML Comment Template for ViewModel
    internal class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     The help file
        /// </summary>
        /// TODO Edit XML Comment Template for HelpFile
        private static readonly string HelpFile =
            Combine(Combine("Resources", "Help"), "Help.chm");

        /// <summary>
        ///     The about window
        /// </summary>
        /// TODO Edit XML Comment Template for _aboutWindow
        private AboutWindow _aboutWindow;

        /// <summary>
        ///     The combined paths is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _combinedPathsIsChecked
        private bool _combinedPathsIsChecked;

        /// <summary>
        ///     The exclude extensions is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _excludeExtensionsIsChecked
        private bool _excludeExtensionsIsChecked;

        /// <summary>
        ///     The extensions selector index
        /// </summary>
        /// TODO Edit XML Comment Template for _extensionsSelectorIndex
        private int _extensionsSelectorIndex;

        /// <summary>
        ///     The is busy
        /// </summary>
        /// TODO Edit XML Comment Template for _isBusy
        private bool _isBusy;

        /// <summary>
        ///     The maximum file size
        /// </summary>
        /// TODO Edit XML Comment Template for _maxFileSize
        private int _maxFileSize;

        /// <summary>
        ///     The maximum file size is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _maxFileSizeIsChecked
        private bool _maxFileSizeIsChecked;

        /// <summary>
        ///     The maximum folder contents
        /// </summary>
        /// TODO Edit XML Comment Template for _maxFolderContents
        private int _maxFolderContents;

        /// <summary>
        ///     The maximum folder contents is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _maxFolderContentsIsChecked
        private bool _maxFolderContentsIsChecked;

        /// <summary>
        ///     The maximum folder size
        /// </summary>
        /// TODO Edit XML Comment Template for _maxFolderSize
        private int _maxFolderSize;

        /// <summary>
        ///     The maximum folder size is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _maxFolderSizeIsChecked
        private bool _maxFolderSizeIsChecked;

        /// <summary>
        ///     The message
        /// </summary>
        /// TODO Edit XML Comment Template for _message
        private string _message;

        /// <summary>
        ///     The message is visible
        /// </summary>
        /// TODO Edit XML Comment Template for _messageIsVisible
        private bool _messageIsVisible;

        /// <summary>
        ///     The message z index
        /// </summary>
        /// TODO Edit XML Comment Template for _messageZIndex
        private int _messageZIndex;

        /// <summary>
        ///     The minimum file size
        /// </summary>
        /// TODO Edit XML Comment Template for _minFileSize
        private int _minFileSize;

        /// <summary>
        ///     The minimum file size is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _minFileSizeIsChecked
        private bool _minFileSizeIsChecked;

        /// <summary>
        ///     The minimum folder contents
        /// </summary>
        /// TODO Edit XML Comment Template for _minFolderContents
        private int _minFolderContents;

        /// <summary>
        ///     The minimum folder contents is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _minFolderContentsIsChecked
        private bool _minFolderContentsIsChecked;

        /// <summary>
        ///     The minimum folder size
        /// </summary>
        /// TODO Edit XML Comment Template for _minFolderSize
        private int _minFolderSize;

        /// <summary>
        ///     The minimum folder size is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _minFolderSizeIsChecked
        private bool _minFolderSizeIsChecked;

        /// <summary>
        ///     The scope all children is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _scopeAllChildrenIsChecked
        private bool _scopeAllChildrenIsChecked;

        /// <summary>
        ///     The scope immediate children is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _scopeImmediateChildrenIsChecked
        private bool _scopeImmediateChildrenIsChecked;

        /// <summary>
        ///     The scope no children is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _scopeNoChildrenIsChecked
        private bool _scopeNoChildrenIsChecked;

        /// <summary>
        ///     The split paths is checked
        /// </summary>
        /// TODO Edit XML Comment Template for _splitPathsIsChecked
        private bool _splitPathsIsChecked;

        /// <summary>
        ///     The user guide
        /// </summary>
        /// TODO Edit XML Comment Template for _userGuide
        private Process _userGuide;

        /// <summary>
        ///     Gets the add excluded path.
        /// </summary>
        /// <value>The add excluded path.</value>
        /// TODO Edit XML Comment Template for AddExcludedPath
        public ICommand AddExcludedPath => new RelayCommand(
            ExecuteAddExcludedPath,
            CanAddExcludedPath);

        /// <summary>
        ///     Gets the add extension.
        /// </summary>
        /// <value>The add extension.</value>
        /// TODO Edit XML Comment Template for AddExtension
        public ICommand AddExtension => new RelayCommand(
            ExecuteAddExtension,
            CanAddExtension);

        /// <summary>
        ///     Gets the add search path.
        /// </summary>
        /// <value>The add search path.</value>
        /// TODO Edit XML Comment Template for AddSearchPath
        public ICommand AddSearchPath => new RelayCommand(
            ExecuteAddSearchPath,
            CanAddSearchPath);

        /// <summary>
        ///     Gets the cancel.
        /// </summary>
        /// <value>The cancel.</value>
        /// TODO Edit XML Comment Template for Cancel
        public ICommand Cancel => new RelayCommand(ExecuteCancel, CanCancel);

        /// <summary>
        ///     Gets the excluded paths.
        /// </summary>
        /// <value>The excluded paths.</value>
        /// TODO Edit XML Comment Template for ExcludedPaths
        public ObservableCollection<DynamicDirectoryPath> ExcludedPaths
        {
            get;
        } = new DynamicDirectoryPathCollection();

        /// <summary>
        ///     Gets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        /// TODO Edit XML Comment Template for Extensions
        public DynamicTextCollection Extensions
        {
            get;
        } = new DynamicTextCollection();

        /// <summary>
        ///     Gets the get file size information.
        /// </summary>
        /// <value>The get file size information.</value>
        /// TODO Edit XML Comment Template for GetFileSizeInfo
        public ICommand GetFileSizeInfo => new RelayCommand(
            ExecuteGetFileSizeInfo,
            CanGetFileSizeInfo);

        /// <summary>
        ///     Gets the open about window.
        /// </summary>
        /// <value>The open about window.</value>
        /// TODO Edit XML Comment Template for OpenAboutWindow
        public ICommand OpenAboutWindow => new RelayCommand(
            ExecuteOpenAboutWindow);

        /// <summary>
        ///     Gets the open user guide.
        /// </summary>
        /// <value>The open user guide.</value>
        /// TODO Edit XML Comment Template for OpenUserGuide
        public ICommand OpenUserGuide => new RelayCommand(ExecuteOpenUserGuide);

        /// <summary>
        ///     Gets the remove excluded path.
        /// </summary>
        /// <value>The remove excluded path.</value>
        /// TODO Edit XML Comment Template for RemoveExcludedPath
        public ICommand RemoveExcludedPath => new
            RelayCommand<DynamicDirectoryPath>(
                ExecuteRemoveExcludedPath,
                CanRemoveExcludedPath);

        /// <summary>
        ///     Gets the remove extension.
        /// </summary>
        /// <value>The remove extension.</value>
        /// TODO Edit XML Comment Template for RemoveExtension
        public ICommand RemoveExtension => new
            RelayCommand<DynamicText.DynamicText>(
                ExecuteRemoveExtension,
                CanRemoveExtension);

        /// <summary>
        ///     Gets the remove search path.
        /// </summary>
        /// <value>The remove search path.</value>
        /// TODO Edit XML Comment Template for RemoveSearchPath
        public ICommand RemoveSearchPath => new
            RelayCommand<DynamicDirectoryPath>(
                ExecuteRemoveSearchPath,
                CanRemoveSearchPath);

        /// <summary>
        ///     Gets the search paths.
        /// </summary>
        /// <value>The search paths.</value>
        /// TODO Edit XML Comment Template for SearchPaths
        public DynamicDirectoryPathCollection SearchPaths
        {
            get;
        } = new DynamicDirectoryPathCollection();

        /// <summary>
        ///     Gets the version.
        /// </summary>
        /// <value>The version.</value>
        /// TODO Edit XML Comment Template for Version
        public string Version
        {
            get
            {
                string version;

                try
                {
                    version = ApplicationDeployment.CurrentDeployment
                        .CurrentVersion.ToString();
                }
                catch (InvalidDeploymentException)
                {
                    version = "Dev";
                }

                return version;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [combined paths
        ///     is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [combined paths is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for CombinedPathsIsChecked
        public bool CombinedPathsIsChecked
        {
            get => _combinedPathsIsChecked;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [exclude
        ///     extensions is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [exclude extensions is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ExcludeExtensionsIsChecked
        public bool ExcludeExtensionsIsChecked
        {
            get => _excludeExtensionsIsChecked;
            set
            {
                if (_excludeExtensionsIsChecked)
                {
                    return;
                }

                _excludeExtensionsIsChecked = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the index of the extensions selector.
        /// </summary>
        /// <value>The index of the extensions selector.</value>
        /// TODO Edit XML Comment Template for ExtensionsSelectorIndex
        public int ExtensionsSelectorIndex
        {
            get => _extensionsSelectorIndex;
            set
            {
                if (_extensionsSelectorIndex == value)
                {
                    return;
                }

                _extensionsSelectorIndex = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance
        ///     is busy.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is busy; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for IsBusy
        public bool IsBusy
        {
            get => _isBusy;
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

        /// <summary>
        ///     Gets or sets the maximum size of the file.
        /// </summary>
        /// <value>The maximum size of the file.</value>
        /// TODO Edit XML Comment Template for MaxFileSize
        public int MaxFileSize
        {
            get => _maxFileSize;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [maximum file
        ///     size is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [maximum file size is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for MaxFileSizeIsChecked
        public bool MaxFileSizeIsChecked
        {
            get => _maxFileSizeIsChecked;
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

        /// <summary>
        ///     Gets or sets the maximum folder contents.
        /// </summary>
        /// <value>The maximum folder contents.</value>
        /// TODO Edit XML Comment Template for MaxFolderContents
        public int MaxFolderContents
        {
            get => _maxFolderContents;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [maximum folder
        ///     contents is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [maximum folder contents is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for MaxFolderContentsIsChecked
        public bool MaxFolderContentsIsChecked
        {
            get => _maxFolderContentsIsChecked;
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

        /// <summary>
        ///     Gets or sets the maximum size of the folder.
        /// </summary>
        /// <value>The maximum size of the folder.</value>
        /// TODO Edit XML Comment Template for MaxFolderSize
        public int MaxFolderSize
        {
            get => _maxFolderSize;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [maximum folder
        ///     size is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [maximum folder size is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for MaxFolderSizeIsChecked
        public bool MaxFolderSizeIsChecked
        {
            get => _maxFolderSizeIsChecked;
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

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        /// TODO Edit XML Comment Template for Message
        public string Message
        {
            get => _message;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [message is
        ///     visible].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [message is visible]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for MessageIsVisible
        public bool MessageIsVisible
        {
            get => _messageIsVisible;
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

        /// <summary>
        ///     Gets or sets the index of the message z.
        /// </summary>
        /// <value>The index of the message z.</value>
        /// TODO Edit XML Comment Template for MessageZIndex
        public int MessageZIndex
        {
            get => _messageZIndex;
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

        /// <summary>
        ///     Gets or sets the minimum size of the file.
        /// </summary>
        /// <value>The minimum size of the file.</value>
        /// TODO Edit XML Comment Template for MinFileSize
        public int MinFileSize
        {
            get => _minFileSize;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [minimum file
        ///     size is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [minimum file size is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for MinFileSizeIsChecked
        public bool MinFileSizeIsChecked
        {
            get => _minFileSizeIsChecked;
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

        /// <summary>
        ///     Gets or sets the minimum folder contents.
        /// </summary>
        /// <value>The minimum folder contents.</value>
        /// TODO Edit XML Comment Template for MinFolderContents
        public int MinFolderContents
        {
            get => _minFolderContents;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [minimum folder
        ///     contents is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [minimum folder contents is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for MinFolderContentsIsChecked
        public bool MinFolderContentsIsChecked
        {
            get => _minFolderContentsIsChecked;
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

        /// <summary>
        ///     Gets or sets the minimum size of the folder.
        /// </summary>
        /// <value>The minimum size of the folder.</value>
        /// TODO Edit XML Comment Template for MinFolderSize
        public int MinFolderSize
        {
            get => _minFolderSize;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [minimum folder
        ///     size is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [minimum folder size is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for MinFolderSizeIsChecked
        public bool MinFolderSizeIsChecked
        {
            get => _minFolderSizeIsChecked;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [scope all
        ///     children is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [scope all children is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ScopeAllChildrenIsChecked
        public bool ScopeAllChildrenIsChecked
        {
            get => _scopeAllChildrenIsChecked;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [scope
        ///     immediate children is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [scope immediate children is
        ///     checked]; otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ScopeImmediateChildrenIsChecked
        public bool ScopeImmediateChildrenIsChecked
        {
            get => _scopeImmediateChildrenIsChecked;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [scope no
        ///     children is checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [scope no children is checked];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for ScopeNoChildrenIsChecked
        public bool ScopeNoChildrenIsChecked
        {
            get => _scopeNoChildrenIsChecked;
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

        /// <summary>
        ///     Gets or sets a value indicating whether [split paths is
        ///     checked].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [split paths is checked]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for SplitPathsIsChecked
        public bool SplitPathsIsChecked
        {
            get => _splitPathsIsChecked;
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

        /// <summary>
        ///     Gets the about window.
        /// </summary>
        /// <value>The about window.</value>
        /// TODO Edit XML Comment Template for AboutWindow
        private AboutWindow AboutWindow
        {
            get
            {
                if (_aboutWindow == null
                    || !_aboutWindow.IsVisible)
                {
                    _aboutWindow = new AboutWindow();
                }

                return _aboutWindow;
            }
        }

        /// <summary>
        ///     Gets the user guide.
        /// </summary>
        /// <value>The user guide.</value>
        /// TODO Edit XML Comment Template for UserGuide
        private Process UserGuide
        {
            get
            {
                if (_userGuide != null
                    && !_userGuide.HasExited)
                {
                    _userGuide.Kill();
                }
                _userGuide = new Process
                {
                    StartInfo =
                    {
                        FileName = HelpFile
                    }
                };

                return _userGuide;
            }
        }

        /// <summary>
        ///     Gets or sets the file system size information checker.
        /// </summary>
        /// <value>The file system size information checker.</value>
        /// TODO Edit XML Comment Template for FileSystemSizeInfoChecker
        private FileSystemSizeInfoChecker FileSystemSizeInfoChecker
        {
            get;
            set;
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        /// TODO Edit XML Comment Template for PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets the selected scope.
        /// </summary>
        /// <returns>Scope.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// TODO Edit XML Comment Template for GetSelectedScope
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

        /// <summary>
        ///     Determines whether this instance [can add excluded
        ///     path].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance [can add excluded
        ///     path]; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for CanAddExcludedPath
        private bool CanAddExcludedPath()
        {
            return ExcludedPaths.All(x => !x.IsNullOrWhiteSpace());
        }

        /// <summary>
        ///     Determines whether this instance [can add extension].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance [can add extension];
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for CanAddExtension
        private bool CanAddExtension()
        {
            return Extensions.All(x => !x.IsNullOrWhiteSpace());
        }

        /// <summary>
        ///     Determines whether this instance [can add search path].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance [can add search
        ///     path]; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for CanAddSearchPath
        private bool CanAddSearchPath()
        {
            return SearchPaths.All(x => !x.IsNullOrWhiteSpace());
        }

        /// <summary>
        ///     Determines whether this instance can cancel.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance can cancel;
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for CanCancel
        private bool CanCancel()
        {
            return IsBusy;
        }

        /// <summary>
        ///     Determines whether this instance [can get file size
        ///     information].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance [can get file size
        ///     information]; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for CanGetFileSizeInfo
        private bool CanGetFileSizeInfo()
        {
            return SearchPaths.ContainsText()
                   && PathDisplayOptionIsChecked()
                   && ScopeIsSelected();
        }

        /// <summary>
        ///     Determines whether this instance [can remove excluded
        ///     path] the specified excluded path.
        /// </summary>
        /// <param name="excludedPath">The excluded path.</param>
        /// <returns>
        ///     <c>true</c> if this instance [can remove excluded path]
        ///     the specified excluded path; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for CanRemoveExcludedPath
        private bool CanRemoveExcludedPath(
            DynamicDirectoryPath excludedPath = null)
        {
            return ExcludedPaths.Count > 1
                   || ExcludedPaths.All(x => !x.IsNullOrWhiteSpace());
        }

        /// <summary>
        ///     Determines whether this instance [can remove extension]
        ///     the specified excluded extension.
        /// </summary>
        /// <param name="excludedExtension">The excluded extension.</param>
        /// <returns>
        ///     <c>true</c> if this instance [can remove extension] the
        ///     specified excluded extension; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for CanRemoveExtension
        private bool CanRemoveExtension(
            DynamicText.DynamicText excludedExtension)
        {
            return Extensions.Count > 1
                   || Extensions.All(x => !x.IsNullOrWhiteSpace());
        }

        /// <summary>
        ///     Determines whether this instance [can remove search
        ///     path] the specified search path.
        /// </summary>
        /// <param name="searchPath">The search path.</param>
        /// <returns>
        ///     <c>true</c> if this instance [can remove search path]
        ///     the specified search path; otherwise, <c>false</c>.
        /// </returns>
        /// TODO Edit XML Comment Template for CanRemoveSearchPath
        private bool CanRemoveSearchPath(DynamicDirectoryPath searchPath = null)
        {
            return SearchPaths.Count > 1
                   || SearchPaths.All(x => !x.IsNullOrWhiteSpace());
        }

        /// <summary>
        ///     Executes the add excluded path.
        /// </summary>
        /// TODO Edit XML Comment Template for ExecuteAddExcludedPath
        private void ExecuteAddExcludedPath()
        {
            ExcludedPaths.Add(new DynamicDirectoryPath());
        }

        /// <summary>
        ///     Executes the add extension.
        /// </summary>
        /// TODO Edit XML Comment Template for ExecuteAddExtension
        private void ExecuteAddExtension()
        {
            Extensions.Add(new DynamicText.DynamicText());
        }

        /// <summary>
        ///     Executes the add search path.
        /// </summary>
        /// TODO Edit XML Comment Template for ExecuteAddSearchPath
        private void ExecuteAddSearchPath()
        {
            SearchPaths.Add(new DynamicDirectoryPath());
        }

        /// <summary>
        ///     Executes the cancel.
        /// </summary>
        /// TODO Edit XML Comment Template for ExecuteCancel
        private void ExecuteCancel()
        {
            FileSystemSizeInfoChecker.Cancel();
        }

        /// <summary>
        ///     Executes the get file size information.
        /// </summary>
        /// TODO Edit XML Comment Template for ExecuteGetFileSizeInfo
        private async void ExecuteGetFileSizeInfo()
        {
            var searchPaths = from x in SearchPaths
                where !x.IsNullOrWhiteSpace()
                select x.Text;

            var excludedPaths = from x in ExcludedPaths
                where !x.IsNullOrWhiteSpace()
                select x.Text;

            var minFileSize = MinFileSizeIsChecked ? (int?) MinFileSize : null;

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

            var extensions = from x in Extensions
                where !x.IsNullOrWhiteSpace()
                select x.Text;

            var shouldExcludeExtensions = ExtensionsSelectorIndex == 0;

            FileSystemSizeInfoChecker = new FileSystemSizeInfoChecker(
                searchPaths,
                excludedPaths,
                GetSelectedScope(),
                SplitPathsIsChecked,
                shouldExcludeExtensions,
                extensions,
                minFileSize,
                maxFileSize,
                minFolderSize,
                maxFolderSize,
                minFolderContents,
                maxFolderContents);

            IsBusy = true;

            try
            {
                await FileSystemSizeInfoChecker.Execute();
            }
            catch (OperationCanceledException)
            {
                ShowMessage("Operation was cancelled.");
            }

            IsBusy = false;
        }

        /// <summary>
        ///     Executes the open about window.
        /// </summary>
        /// TODO Edit XML Comment Template for ExecuteOpenAboutWindow
        private void ExecuteOpenAboutWindow()
        {
            AboutWindow.Show();
            AboutWindow.Activate();
        }

        // TODO
        /// <summary>
        ///     Executes the open user guide.
        /// </summary>
        /// TODO Edit XML Comment Template for ExecuteOpenUserGuide
        private void ExecuteOpenUserGuide()
        {
            try
            {
                UserGuide.Start();
            }
            catch (Exception)
            {
                ShowMessage("User guide could not be opened.");
            }
        }

        /// <summary>
        ///     Executes the remove excluded path.
        /// </summary>
        /// <param name="excludedPath">The excluded path.</param>
        /// TODO Edit XML Comment Template for ExecuteRemoveExcludedPath
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

        /// <summary>
        ///     Executes the remove extension.
        /// </summary>
        /// <param name="excludedExtension">The excluded extension.</param>
        /// TODO Edit XML Comment Template for ExecuteRemoveExtension
        private void ExecuteRemoveExtension(
            DynamicText.DynamicText excludedExtension)
        {
            if (Extensions.Count > 1)
            {
                Extensions.Remove(excludedExtension);
            }
            else
            {
                Extensions.First().Text = string.Empty;
            }
        }

        /// <summary>
        ///     Executes the remove search path.
        /// </summary>
        /// <param name="searchPath">The search path.</param>
        /// TODO Edit XML Comment Template for ExecuteRemoveSearchPath
        private void ExecuteRemoveSearchPath(DynamicDirectoryPath searchPath)
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

        /// <summary>
        ///     Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// TODO Edit XML Comment Template for NotifyPropertyChanged
        private void NotifyPropertyChanged(
            [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Pathes the display option is checked.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// TODO Edit XML Comment Template for PathDisplayOptionIsChecked
        private bool PathDisplayOptionIsChecked()
        {
            return CombinedPathsIsChecked || SplitPathsIsChecked;
        }

        /// <summary>
        ///     Scopes the is selected.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// TODO Edit XML Comment Template for ScopeIsSelected
        private bool ScopeIsSelected()
        {
            return ScopeAllChildrenIsChecked
                   || ScopeImmediateChildrenIsChecked
                   || ScopeNoChildrenIsChecked;
        }

        /// <summary>
        ///     Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// TODO Edit XML Comment Template for ShowMessage
        private void ShowMessage(string message)
        {
            Message = message + "\n\nDouble-click to dismiss.";
            MessageIsVisible = true;
            MessageZIndex = 1;
        }
    }
}