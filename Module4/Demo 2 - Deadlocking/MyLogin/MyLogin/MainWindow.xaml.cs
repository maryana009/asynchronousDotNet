using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MyLogin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await LoginAsync();
            }
            catch (Exception)
            {
                // Raygun
            }
        }

        private async Task<string> LoginAsync()
        {
            await Task.Delay(2000);

            return "Login Successful!";
        }
    }
}
