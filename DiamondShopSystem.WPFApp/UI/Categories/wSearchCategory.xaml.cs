using DiamondShopSystem.Business;
using DiamondShopSystem.Data.Models;
using DiamondShopSystem.WPFApp.UI.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiamondShopSystem.WPFApp.UI.Categories
{
    /// <summary>
    /// Interaction logic for wSearchCategory.xaml
    /// </summary>
    public partial class wSearchCategory : Window
    {
        private readonly CategoryBusiness _business;
        public wSearchCategory()
        {
            InitializeComponent();
            _business = new CategoryBusiness();
            DataContext = this;
            LoadGrdCategory();
        }

        private async Task LoadGrdCategory()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCategory.ItemsSource = result.Data as List<Category>;
            }
            else
            {
                grdCategory.ItemsSource = new List<Category>();
            }
        }

        private void ButtonCloess_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = CategoryName.Text.ToLower();

            var searchResults = await _business.GetByName(name);
            if (searchResults.Status > 0 && searchResults.Data != null)
            {
                grdCategory.ItemsSource = searchResults.Data as List<Category>;
            }
            else
            {
                MessageBox.Show("No category found.", "Search");
                await LoadGrdCategory();
            }
        }

        private async void grdCategory_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string categoryId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(categoryId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(categoryId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    LoadGrdCategory();
                }
            }
        }

        private async void grdCategory_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            if(grdCategory.SelectedItem is Category selectedCustomer)
            {
                var detailWindow = new wCategoryDetail(selectedCustomer.CategoryId);
                detailWindow.ShowDialog();
            }
        }
    }
}
