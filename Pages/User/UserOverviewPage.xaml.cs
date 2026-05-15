using GreenSwap.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GreenSwap.Pages.User
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserOverviewPage : Page
    {
        public string PageTitle => "User Overview";

        private List<GreenSwap.Models.User> AllUsers = new();
        private List<Role> RoleList = new();
        public UserOverviewPage()
        {
            InitializeComponent();

            LoadUsers();
        }

        public void LoadUsers()
        {
            using (var db = new AppDbContext())
            {
                AllUsers = db.Users
                    .Include(u => u.Role)
                    .ToList();

                RoleList = db.Roles.ToList();
            }


            UserListView.ItemsSource = AllUsers;

            RoleFilterComboBox.ItemsSource = RoleList;
        }

        private void FilterUsers()
        {
            var filteredUsers = AllUsers.AsQueryable();

            // Name search
            string searchText = SearchBox.Text?.ToLower() ?? "";

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredUsers = filteredUsers
                    .Where(u => u.Name.ToLower().Contains(searchText));
            }

            // Role filter
            if (RoleFilterComboBox.SelectedItem is Role selectedRole)
            {
                if (selectedRole.Id != 4)
                {
                    filteredUsers = filteredUsers
                        .Where(u => u.RoleId == selectedRole.Id);
                }
            }

            UserListView.ItemsSource = filteredUsers.ToList();

        }

        

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterUsers();
        }

        private void RoleFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterUsers();
        }

        private void UserListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void UserCreationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
