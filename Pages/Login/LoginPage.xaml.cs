using GreenSwap.Data;
using GreenSwap.Data.Models;
using GreenSwap.Pages.Plant;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.System;
using System.Linq;


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

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));

        }

        private void DevInlogButton_Click(object sender, RoutedEventArgs e)
        {
            var username = "admin@greenswap.nl";
            var password = "admin123";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Een van de gegevens zijn niet ingevuld!");
                return;
            }

            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u =>
            u.Name.ToLower() == username.ToLower() || u.Email.ToLower() == username.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ShowError("⚠ ACCESS DENIED: Incorrect Wachtwoord!");

            }
            else
            {
                GreenSwap.Data.Models.User.LoggedInUser = user; 
                Frame.Navigate(typeof(OverviewPage));
            }
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
            string username = NameEmailTextBox.Text?.Trim();
            string password = PasswordTextBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await ShowError("Vul zowel username/email als wachtwoord in.");
                return;
            }

            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u =>
                u.Email.ToLower() == username.ToLower() ||
                u.Name.ToLower() == username.ToLower());

            if (user == null)
            {
                await ShowError("Gebruiker niet gevonden.");
                return;
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!passwordValid)
            {
                await ShowError("Onjuist wachtwoord.");
                return;
            }

            // ✅ Succesvol ingelogd
            GreenSwap.Data.Models.User.LoggedInUser = user;

            var dialog = new ContentDialog
            {
                Title = "Succes",
                Content = "Je bent ingelogd!",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();

            Frame.Navigate(typeof(OverviewPage));
        }
        private async System.Threading.Tasks.Task ShowError(string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Fout",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }

    }
}