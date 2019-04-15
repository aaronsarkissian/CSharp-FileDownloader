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
            CreateDelete();
        }

        private WebClient Download()
        {
            WebClient wc = new WebClient();
            Uri link = new Uri(url);

            string filename = Path.GetFileName(link.LocalPath);

            wc.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
            wc.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");

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

        private void Cancel_button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CancelDownload();
        }

        private void CreateDelete()
        {
            //Button cancelButton = new Button();
            //cancelButton.Content = "Cancel";
            //cancelButton.Height = 25;
            //cancelButton.Width = 250;
            //cancelButton.Click += Cancel_button_Click;
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