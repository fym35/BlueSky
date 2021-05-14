using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace BlueSky
{
    /// <summary>
    /// Interaction logic for MCDownloader.xaml
    /// </summary>
    public partial class MCDownloader : Window
    {
        public MCDownloader()
        {
            InitializeComponent();
            try
            {
                Directory.CreateDirectory("C:\\BlueSky\\");
                WebClient Client = new WebClient();
                Client.DownloadFile("https://bluesky-webserver.000webhostapp.com/BLUESKY-VER-CHECK.bluesky", @"C:\BlueSky\blueskymcd.bluesky");
                string lateststable = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(2).Take(1).First();
                string latestbeta = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(5).Take(6).First();
                string lateststableinfo = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(1).Take(1).First();
                string latestbetainfo = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(4).Take(1).First();
                lv1.Content = lateststableinfo;
                lv2.Content = latestbetainfo;
                progr.Content = "Ready to download!";
            }
            catch
            {
                stable.IsEnabled = false;
                beta.IsEnabled = false;
                lv1.Content = "";
                lv2.Content = "";
                progr.Content = "Please check your internet connection.";
            }
        }

        WebClient client;
        WebClient client1;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stable.IsEnabled = false;
            beta.IsEnabled = false;
            string lateststable = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(2).Take(1).First();
            string latestbeta = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(5).Take(6).First();
            string url = lateststable;
            progr.Content = "Downloading stable...";
            Uri uri = new Uri(url);
            client.DownloadFileAsync(uri, "C:\\BlueSky\\stable.appx");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client1 = new WebClient();
            client1.DownloadProgressChanged += Client1_DownloadProgressChanged;
            client1.DownloadFileCompleted += Client1_DownloadFileCompleted;
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progr.Content = "Download completed!";
            stable.IsEnabled = true;
            beta.IsEnabled = true;
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Dispatcher.Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Minimum = 0;
                double receive = double.Parse(e.BytesReceived.ToString());
                double total = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = receive / total * 100;
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            }));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stable.IsEnabled = false;
            beta.IsEnabled = false;
            string lateststable = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(2).Take(1).First();
            string latestbeta = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(5).Take(6).First();
            string url = latestbeta;
            progr.Content = "Downloading beta...";
            Uri uri = new Uri(url);
            client1.DownloadFileAsync(uri, "C:\\BlueSky\\beta.appx");
        }

        private void Client1_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progr.Content = "Download completed!";
            stable.IsEnabled = true;
            beta.IsEnabled = true;
        }

        private void Client1_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Dispatcher.Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Minimum = 0;
                double receive = double.Parse(e.BytesReceived.ToString());
                double total = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = receive / total * 100;
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            }));
        }
    }
}



