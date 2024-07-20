using DiamondShopSystem.Business;
using DiamondShopSystem.Data.Models;
using Microsoft.Win32;
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
    /// Interaction logic for wProduct.xaml
    /// </summary>
    public partial class wProduct : Window
    {
        private readonly ProductBusiness _business;
        private readonly CategoryBusiness _categoryBusiness;
        public wProduct()
        {
            InitializeComponent();
            _business = new ProductBusiness();
            _categoryBusiness = new CategoryBusiness();
            LoadGrdProduct();
        }

        private void ClearForm()
        {
            ProductId.Text = string.Empty;
            ProductName.Text = string.Empty;
            Brand.Text = string.Empty;
            Diamond.Text = string.Empty;
            Image.Text = string.Empty;
            Price.Text = string.Empty;
            CategoriesComboBox.SelectedIndex = 0;
            Quantity.Text = string.Empty;
            Description.Text = string.Empty;
            Status.Text = string.Empty;
            Size.Text = string.Empty;
        }

        private async void CategoriesComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            CategoriesComboBox.Items.Add(new Category
            {
                CategoryId = -1,
                CategoryName = "Chọn Category"
            });

            var categories = await _categoryBusiness.GetCategories();
            foreach ( var category in categories )
            {
                CategoriesComboBox.Items.Add(category);
            }
            CategoriesComboBox.SelectedIndex = 0;
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

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = -1;
                int.TryParse(ProductId.Text, out id);
                var item = await _business.GetById(id);
                if (item.Data == null)
                {
                    var product = new Product()
                    {
                        ProductName = ProductName.Text,
                        Brand = Brand.Text,
                        Diamond = Diamond.Text,
                        Image = Image.Text,
                        Price = double.Parse(Price.Text),
                        Quantity = int.Parse(Quantity.Text),
                        Description = Description.Text,
                        Status = bool.Parse(Status.Text),
                        Size = int.Parse(Size.Text),
                        CategoryId = int.TryParse(CategoriesComboBox.SelectedValue?.ToString(), out var categoryId) ? categoryId : -1
                    };
                    var result = await _business.Save(product);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var product = item.Data as Product;
                    product.ProductName = ProductName.Text;
                    product.Brand = Brand.Text;
                    product.Diamond = Diamond.Text;
                    product.Image = Image.Text;
                    product.Price = double.Parse(Price.Text);
                    product.Quantity = int.Parse(Quantity.Text);
                    product.Description = Description.Text;
                    product.Status = bool.Parse(Status.Text);
                    product.Size = int.Parse(Size.Text);
                    product.CategoryId = int.TryParse(CategoriesComboBox.SelectedValue?.ToString(), out var categoryId) ? categoryId : product.CategoryId;

                    var result = await _business.Update(product);
                    MessageBox.Show(result.Message, "Update");
                }
                ClearForm();
                LoadGrdProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                Image.Text = fileName;
            }
        }

        private async void grdProduct_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(productId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(productId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    LoadGrdProduct();
                }
            }
        }

        private async void grdProduct_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Product;
                    if (item != null)
                    {
                        var productResult = await _business.GetById(item.ProductId);

                        if (productResult.Status > 0 && productResult.Data != null)
                        {
                            item = productResult.Data as Product;
                            ProductId.Text = item.ProductId.ToString();
                            ProductName.Text = item.ProductName;
                            Brand.Text = item.Brand;
                            Diamond.Text = item.Diamond;
                            Image.Text = item.Image;
                            Price.Text = item.Price.ToString();
                            Quantity.Text = item.Quantity.ToString();
                            Description.Text = item.Description;
                            Status.Text = item.Status.ToString();
                            Size.Text = item.Size.ToString();
                            CategoriesComboBox.SelectedValue = item.CategoryId;
                        }
                    }
                }
            }
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            var sp = new wSearchProduct();
            sp.Owner = this;
            sp.Show();
        }
    }
}
