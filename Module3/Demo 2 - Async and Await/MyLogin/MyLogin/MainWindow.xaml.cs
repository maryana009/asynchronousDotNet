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

        private async void LoginButton_Click1(object sender, RoutedEventArgs e)
        {
            //await grabs result and continuation happens on a calling thread, ie UI thread
            var result = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                return "Login successful";
            });

            LoginButton.Content = result;

        }


        //generally async should return Task, not be void, but event handler has this signature
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginButton.IsEnabled = false;
                BusyIndicator.Visibility = Visibility.Visible;

                //will do cross threading, continuation will no longer be executed on an UI thread
                //var result = await LoginAsync().ConfigureAwait(false);
                var result = await LoginAsync();

                LoginButton.Content = result;

                LoginButton.IsEnabled = true;
                BusyIndicator.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                LoginButton.Content = "Internal Error!";
            }
        }

        private async Task<string> LoginAsync()
        {
            try
            {
                var loginTask = Task.Run(() => {

                    Thread.Sleep(2000);

                    return "Login Successful!";
                });
                
                var logTask = Task.Delay(2000); // Log the login
                
                var purchaseTask = Task.Delay(1000); // Fetch purchases

                await Task.WhenAll(loginTask, logTask, purchaseTask);
                
                return loginTask.Result;
            }
            catch (Exception)
            {
                return "Login failed!";
            }
        }
    }
}
