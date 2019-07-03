using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace ThreadPoolWPF {
    public class MainViewModel {
        public ActiveProperty<Uri> DownloadUri { get; } = new ActiveProperty<Uri>();
        public ObservableCollection<DownloadItem> Downloads { get; }
        public ICommand DownloadCommand { get; }
        public ICommand CancelCommand { get; }
        private IDialogService _dialogService;

        public MainViewModel(IDialogService dialogService) {
            _dialogService = dialogService;

            Downloads = new ObservableCollection<DownloadItem>();
            DownloadCommand = new RelayCommand(DownloadCommandExecute, DownloadCommandCanExecute);
            CancelCommand = new RelayCommand<DownloadItem>(CancelCommandExecute);
        }

        private void CancelCommandExecute(DownloadItem param) {
            if (_dialogService.Confirm("Are you sure you want to cancel download?", "Part of the ship, part of the crew")) {
                param.CancelDownload();
                Downloads.Remove(param);
            }
        }

        private void DownloadCommandExecute() {
            var item = new DownloadItem(
                DownloadUri.Value,
                Path.GetRandomFileName()
            );
            Downloads.Add(item);
            try {
                item.StartDownload();
            } catch (Exception ex) {
                _dialogService.Error($"Error: {ex.Message}", "Download error");
                Downloads.Remove(item);
            }

            DownloadUri.Value = null;
        }

        private bool DownloadCommandCanExecute() {
            return DownloadUri.Value?.IsAbsoluteUri ?? false;
        }
    }
}
