using System;
using System.Net;
using System.IO;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WebDownloader.MultiDownloader.Controller
{

    public class DownloadManager
    {
        private static int count = 1;
        private string url;
        private string path;
        private ListView listView;
        public ProgressBar progress;
        public WebClient client;

        public DownloadManager(string url, string path, ListView listView)
        {
            this.url = url;
            this.path = path;
            this.listView = listView;
        }

        private WebClient Download()
        {
            WebClient wc = new WebClient();
            Uri link = new Uri(url);

            string filename = Path.GetFileName(link.LocalPath);

            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClient_DownloadProgressChanged);
            wc.DownloadFileAsync(link, path + Path.DirectorySeparatorChar + filename);
            progress = new ProgressBar();
            Item item = new Item() { Count = count++, MyProgress = progress };
            listView.Items.Add(item);
            return wc;
        }

        public void Start()
        {
            client = Download();
        }

        public void CancelDownload()
        {
            client.CancelAsync();
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Dispatcher.Invoke(() =>
                    progress.Value = e.ProgressPercentage,
                    DispatcherPriority.Background
            );
        }
    }

    public class Item
    {
        public int Count { get; set; }
        public ProgressBar MyProgress { get; set; }
    }
}

// Big images for testing
// https://upload.wikimedia.org/wikipedia/commons/0/0a/Parc_de_Versailles%2C_Fontaine_du_Point_du_Jour_02.jpg
// https://upload.wikimedia.org/wikipedia/commons/e/e6/Clocktower_Panorama_20080622_20mb.jpg