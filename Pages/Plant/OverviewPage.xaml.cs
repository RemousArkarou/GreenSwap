using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using GreenSwap.Data;
using System.Linq;

namespace GreenSwap.Pages.Plant
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            this.InitializeComponent();
            LoadPlantsFromDatabase();
        }

        private void LoadPlantsFromDatabase()
        {
            using (var db = new AppDbContext())
            {
                // Haal de data op uit je database seeds
                var plants = db.Plants.ToList();
                ItemsListView.ItemsSource = plants;
            }
        }

        // --- HIER ZIJN DE MISSENDE METHODES ---

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            // Haal het specifieke plant object op uit de geklikte knop
            var button = sender as Button;
            var selectedPlant = button?.DataContext as Data.Models.Plant;

            if (selectedPlant != null)
            {
                // Navigeer bijvoorbeeld naar een detailpagina (als je die hebt)
                // this.Frame.Navigate(typeof(PlantDetailPage), selectedPlant.Id);
            }
        }

        private void TradeButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedPlant = button?.DataContext as Data.Models.Plant;

            // Hier kun je je trade logica starten
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = NameFilterBox.Text.ToLower();
            var selectedCategory = (CategoryFilterBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            using (var db = new AppDbContext())
            {
                var query = db.Plants.AsQueryable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(p => p.Name.ToLower().Contains(searchText));
                }

                if (selectedCategory != "All" && selectedCategory != null)
                {
                    query = query.Where(p => p.PlantType == selectedCategory);
                }

                ItemsListView.ItemsSource = query.ToList();
            }
        }
    }
}