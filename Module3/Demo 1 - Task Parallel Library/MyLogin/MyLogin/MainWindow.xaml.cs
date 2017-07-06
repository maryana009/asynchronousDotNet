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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            var task = Task.Run(() => {

                Thread.Sleep(2000);

                return "Login Successful!";
            });

            //task.Result blocks application, so it freezes until the task end
            //LoginButton.Content = task.Result;
            
            task.ContinueWith((t) => {

                //task swallows excetion, so we should check if it wasn't failed
                if (t.IsFaulted)
                {
                    //Executes specified action synchronously on the thread System.Windows.Threading.Dispatcher is associated with
                    Dispatcher.Invoke(() =>
                    {
                        LoginButton.Content = "Login failed!";
                        LoginButton.IsEnabled = true;
                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        LoginButton.Content = t.Result;
                        LoginButton.IsEnabled = true;
                    });
                }
            });
        }

        private void LoginButton_Click2(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            var task = Task.Run(() => {

                Thread.Sleep(2000);
                return "Login Successful!";
            });

            //ConfigureAwait((true) marchalling continuation back on to UI thread
            task.ConfigureAwait(true)
                .GetAwaiter()
                .OnCompleted (() =>
                    {
                        LoginButton.Content = "Login failed!";
                        LoginButton.IsEnabled = true;
                    });
        }
            

        private void LoginButton_Click_Wrong(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            var task = Task.Run(() => {

                Thread.Sleep(2000);
            });

            //We are trying to invoke the UI from a different thread
            task.ContinueWith((t) => {
                //Ex The calling thread cannot access this object because a different thread owns it.
                LoginButton.IsEnabled = true;
            });
        }
    }
}
