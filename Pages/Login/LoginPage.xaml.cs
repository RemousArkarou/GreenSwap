using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.System;

namespace GreenSwap.Pages.Login
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void InlogButton_Click(object sender, RoutedEventArgs e)
        {
            await DoLogin();
        }

        private async void DevInlogButton_Click(object sender, RoutedEventArgs e)
        {
            NameEmailTextBox.Text = "admin";
            PasswordTextBox.Password = "password";

            await DoLogin();
        }

        private async void PasswordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await DoLogin();
            }
        }

        private async System.Threading.Tasks.Task DoLogin()
        {
            string username = NameEmailTextBox.Text;
            string password = PasswordTextBox.Password;

            ContentDialog loginDialog = new ContentDialog()
            {
                Title = "Login Result",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            if (username == "admin" && password == "password")
            {
                loginDialog.Content = "Login successful!";
            }
            else
            {
                loginDialog.Content = "Invalid username or password.";
            }

            await loginDialog.ShowAsync();
        }
    }
}