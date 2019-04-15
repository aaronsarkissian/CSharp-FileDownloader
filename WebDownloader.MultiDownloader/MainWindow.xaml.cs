using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using WebDownloader.MultiDownloader.Controller;
using System.Windows.Data;

namespace WebDownloader.MultiDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path = string.Empty;
        //DownloadManager downloader; // Remove this

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Directory_button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                path = folderBrowser.SelectedPath;
                directory_button.Background = Brushes.Green;
            }
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            var url = file_url.Text;
            var dirPath = path;

            DownloadManager downloader = new DownloadManager(url, dirPath, fileList);
            downloader.Start();
        }

        // Fix this - it only cancels the last item!
    }
}
