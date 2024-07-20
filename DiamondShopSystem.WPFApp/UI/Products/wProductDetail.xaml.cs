using DiamondShopSystem.Business;
using DiamondShopSystem.Data.Models;
using DiamondShopSystem.WPFApp.UI.Categories;
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
    /// Interaction logic for wProductDetail.xaml
    /// </summary>
    public partial class wProductDetail : Window
    {
        private readonly ProductBusiness _business;
        private readonly CategoryBusiness _categoryBusiness;
        private readonly int _id;
        public wProductDetail(int productId)
        {
            InitializeComponent();
            _business = new ProductBusiness();
            _categoryBusiness = new CategoryBusiness();
            _id = productId;
            LoadProductDetail();
        }

        private async void LoadProductDetail()
        {
            try
            {
                var result = await _business.GetById(_id);

                if (result != null && result.Status > 0 && result.Data != null)
                {
                    var product = result.Data as Product;
                    if (product != null)
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

                        ProductIdBlockText.Text = product.ProductId.ToString();
                        ProductNameBlockText.Text = product.ProductName ?? string.Empty;
                        BrandBlockText.Text = product.Brand ?? string.Empty;
                        DiamondBlockText.Text = product.Diamond ?? string.Empty;
                        CategoryBlockText.Text = product.Category?.CategoryName ?? "Unknown";
                        SizeBlockText.Text = product.Size.ToString();
                        QuantityBlockText.Text = product.Quantity.ToString();
                        PriceBlockText.Text = product.Price.ToString();
                        StatusBlockText.Text = product.Status.ToString();
                        DescriptionBlockText.Text = product.Description ?? string.Empty;

                        if (!string.IsNullOrEmpty(product.Image))
                        {
                            var uri = new Uri(product.Image, UriKind.RelativeOrAbsolute);
                            ProductImage.Source = new BitmapImage(uri);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Product not found or error in fetching product details.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
