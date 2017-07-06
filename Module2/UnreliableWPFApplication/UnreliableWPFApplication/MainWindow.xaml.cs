using System.Net;
using System.Threading;
using System.Windows;

namespace UnreliableWPFApplication
{
    public partial class MainWindow : Window
    {
        private int count = 1;
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }
        
        private void RssButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();

            client.DownloadStringAsync(new System.Uri("http://www.filipekberg.se/rss/"));
            client.DownloadStringCompleted += Client_DownloadDataCompleted;

            //Thread.Sleep(10000);

        }

        private void Client_DownloadDataCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            RssText.Text = e.Result;
        }

        private void CounterButton_Click(object sender, RoutedEventArgs e)
        {
            CounterText.Text = $"Counter: {count++}";
        }
    }
}
