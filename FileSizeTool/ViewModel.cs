using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FileSizeTool
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public ICommand OpenAboutWindow => new RelayCommand(
            ExecuteOpenAboutWindow);

        public ICommand OpenUserGuide => new RelayCommand(
            ExecuteOpenUserGuide);

        private bool _isBusy;

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

        public event PropertyChangedEventHandler PropertyChanged;

        // TODO
        private void ExecuteOpenUserGuide()
        {
            throw new NotImplementedException();
        }

        // TODO
        private void ExecuteOpenAboutWindow()
        {
            throw new NotImplementedException();
        }

        private void NotifyPropertyChanged(
            [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
