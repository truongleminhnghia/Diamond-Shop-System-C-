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

namespace DiamondShopSystem.WPFApp.UI.Categories
{
    /// <summary>
    /// Interaction logic for wCategory.xaml
    /// </summary>
    public partial class wCategory : Window
    {
        private readonly CategoryBusiness _business;
        public wCategory()
        {
            InitializeComponent();
            _business = new CategoryBusiness();
            LoadGrdCategory();
        }

        public void ClearForm()
        {
            CategoryId.Text = string.Empty;
            CategoryName.Text = string.Empty;
        }

        public async void LoadGrdCategory()
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

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = -1;
                int.TryParse(CategoryId.Text, out id);
                var item = await _business.GetById(id);
                if(item.Data ==null)
                {
                    var category = new Category()
                    {
                        CategoryName = CategoryName.Text,
                    };
                    var result = await _business.Save(category);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var category = item.Data as Category;
                    category.CategoryName = CategoryName.Text;
                    var result = await _business.Update(category);
                    MessageBox.Show(result.Message, "Update");
                }
                ClearForm();
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
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Category;
                    if (item != null)
                    {
                        var categoryResult = await _business.GetById(item.CategoryId);

                        if (categoryResult.Status > 0 && categoryResult.Data != null)
                        {
                            item = categoryResult.Data as Category;
                            CategoryId.Text = item.CategoryId.ToString();
                            CategoryName.Text = item.CategoryName;
                        }
                    }
                }
            }
        }
    }
}
