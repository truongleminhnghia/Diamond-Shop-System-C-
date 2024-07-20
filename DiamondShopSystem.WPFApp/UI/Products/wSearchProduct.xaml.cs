using DiamondShopSystem.Business;
using DiamondShopSystem.Data.Models;
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

namespace DiamondShopSystem.WPFApp.UI.Products
{
    /// <summary>
    /// Interaction logic for wSearchProduct.xaml
    /// </summary>
    public partial class wSearchProduct : Window
    {
        private readonly ProductBusiness _business;
        private readonly CategoryBusiness _categoryBusiness;
        public wSearchProduct()
        {
            InitializeComponent(); 
            this._business ??= new ProductBusiness();
            _categoryBusiness ??= new CategoryBusiness();
            DataContext = this;
            LoadGrdProduct();
        }

        private async void LoadGrdProduct()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                var products = result.Data as List<Product>;
                foreach (var product in products)
                {
                    if (product.CategoryId.HasValue)
                    {
                        var cateResult = await _categoryBusiness.GetById(product.CategoryId.Value);
                        if (cateResult.Status > 0 && cateResult.Data != null)
                        {
                            var category = cateResult.Data as Category;
                            product.Category = category;
                        }
                    }
                }
                grdProduct.ItemsSource = products;
            }
            else
            {
                grdProduct.ItemsSource = new List<Product>();
            }
        }

        private void ButtonCloess_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*private async void grdProduct_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            if (grdProduct.SelectedItem is Product selectedCustomer)
            {
                var detailWindow = new wProductDetail(selectedCustomer.ProductId);
                detailWindow.ShowDialog();
            }
        }*/

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductName.Text.ToLower();
            string brand = Brand.Text.ToLower();
            string diamond = Diamond.Text.ToLower();
            string price = Price.Text;
            string size = Size.Text;
            bool? status = Status.IsChecked;

            var searchResults = await _business.search(name, brand, diamond, price, size, status);
            if (searchResults.Status > 0 && searchResults.Data != null)
            {
                grdProduct.ItemsSource = searchResults.Data as List<Product>;
            }
            else
            {
                MessageBox.Show("No products found.", "Search");
                LoadGrdProduct();
            }
        }

        private async void grdProduct_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            if (grdProduct.SelectedItem is Product selectedCustomer)
            {
                var detailWindow = new wProductDetail(selectedCustomer.ProductId);
                detailWindow.ShowDialog();
            }
        }

        private async void Open_wProductNew_Click(object sender, RoutedEventArgs e)
        {
            var cp = new wProduct();
            cp.Owner = this;
            cp.Show();
        }
    }
}
