using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Windows;
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

namespace GreenSwap.temp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Window
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Voeg hier je authenticatielogica toe
            ContentDialog loginDialog = new ContentDialog()
            {
                Title = "Login Result",
                CloseButtonText = "OK"
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
