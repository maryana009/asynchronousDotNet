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
                ToggleLoginBusyIndicator();

                var result = await LoginAsync();

                await Task.Delay(1);
                LoginButton.Content = "Task 1 done";
                await Task.Delay(1);
                LoginButton.Content = "Task 2 done";
                await Task.Delay(1);
                LoginButton.Content = "Task 3 done";
                await Task.Delay(1);
                LoginButton.Content = "Task 4 done";
                await Task.Delay(1);

                LoginButton.Content = result;

                ToggleLoginBusyIndicator();
            }
            catch (Exception)
            {
                LoginButton.Content = "Internal Error!";
            }
        }

        private Task<string> LoginAsync()
        {
            try
            {
                var loginTask = Task.Run(async () => {

                    await Task.Delay(2000);

                    return "Login Successful!";
                });

                return loginTask;
            }
            catch (Exception)
            {
                return Task.FromResult("Login failed!");
            }
        }

        private void ToggleLoginBusyIndicator()
        {
            LoginButton.IsEnabled = !LoginButton.IsEnabled;

            BusyIndicator.Visibility = BusyIndicator.Visibility  == Visibility.Hidden ? 
                Visibility.Visible : Visibility.Hidden;
        }
    }
}
