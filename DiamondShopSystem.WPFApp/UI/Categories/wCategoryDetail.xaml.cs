using DiamondShopSystem.Business;
using DiamondShopSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for wCategoryDetail.xaml
    /// </summary>
    public partial class wCategoryDetail : Window
    {
        private readonly CategoryBusiness _business;
        private readonly int _id;
        public wCategoryDetail(int categoryId)
        {
            InitializeComponent();
            _business = new CategoryBusiness();
            _id = categoryId;
            LoadProductDetail();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void LoadProductDetail()
        {
            var result = await _business.GetById(_id);
            if (result.Status > 0 && result.Data != null)
            {
                var category = result.Data as Category;
                if (category != null)
                {
                    CategoryIdBlockText.Text = category.CategoryId.ToString();
                    CategoryNameBlockText.Text = category.CategoryName;
                }
            }
        }
    }
}
