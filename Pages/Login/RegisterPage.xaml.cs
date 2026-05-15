using GreenSwap.Data;
using GreenSwap.Data.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GreenSwap.Pages.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordTextBox.Password;
            string confirmPassword = ConfirmPasswordTextBox.Password;

            ErrorMessage.Text = "";

            // Validatie
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage.Text = "Vul alle velden in.";
                return;
            }

            if (password != confirmPassword)
            {
                ErrorMessage.Text = "Wachtwoorden komen niet overeen.";
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    // Controleer of email al bestaat
                    var existingUser = db.Users
                        .FirstOrDefault(u => u.Email == email);

                    if (existingUser != null)
                    {
                        ErrorMessage.Text = "E-mailadres bestaat al.";
                        return;
                    }

                    // Nieuwe gebruiker maken

                    GreenSwap.Data.Models.User newUser = new GreenSwap.Data.Models.User
                    {
                        Name = name,
                        Email = email,
                        Password = BCrypt.Net.BCrypt.HashPassword(password),
                        RoleId = 1,
                        GreenCredit = 0,
                        RegistrationDate = DateTime.Now
                    };

                    db.Users.Add(newUser);

                    await db.SaveChangesAsync();

                    ContentDialog successDialog = new ContentDialog
                    {
                        Title = "Succes",
                        Content = "Account succesvol aangemaakt!",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await successDialog.ShowAsync();

                    // Navigeer terug naar login
                    Frame.Navigate(typeof(LoginPage));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }
    }
}
