using System;
using System.Net;
using System.Threading;

namespace ThreadPoolWPF {
    public class DownloadItem {
        private WebClient _wc;

        public ActiveProperty<Guid> Id { get; } = new ActiveProperty<Guid>();
        public ActiveProperty<Uri> Uri { get; } = new ActiveProperty<Uri>();
        public ActiveProperty<string> Name { get; } = new ActiveProperty<string>();
        public ActiveProperty<int> Percentage { get; } = new ActiveProperty<int>();

        public DownloadItem(Uri uri, string name) {
            Uri.Value = uri;
            Name.Value = name;
            _wc = new WebClient();
        }

        public void StartDownload() {
            var context = SynchronizationContext.Current;
            _wc.DownloadProgressChanged += (s, e) => {
                context.Post((skip) => {
                    Percentage.Value = e.ProgressPercentage;
                }, null);
            };
            _wc.DownloadFileAsync(Uri.Value, Name.Value);
        }
        public void CancelDownload() {
            _wc.CancelAsync();
        }
    }
}
