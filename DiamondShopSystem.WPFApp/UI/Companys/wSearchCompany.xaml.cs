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

namespace DiamondShopSystem.WPFApp.UI.Companys
{
    /// <summary>
    /// Interaction logic for wSearchCompany.xaml
    /// </summary>
    public partial class wSearchCompany : Window
    {
        private readonly CompanyBusiness _business;
        public wSearchCompany()
        {
            InitializeComponent();
            this._business ??= new CompanyBusiness();
            DataContext = this;
            LoadGrdProduct();
        }

        private async void LoadGrdProduct()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCompany.ItemsSource = result.Data as List<Company>;
            }
            else
            {
                grdCompany.ItemsSource = new List<Company>();
            }
        }

        private void ButtonCloess_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grdCompany_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            if (grdCompany.SelectedItem is Company selectedCustomer)
            {
                var detailWindow = new wCompanyDetail(selectedCustomer.CompanyId);
                detailWindow.ShowDialog();
            }
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = CompanyName.Text.ToLower();
            string address = Address.Text.ToLower();
            string website = Website.Text.ToLower();
            string industry = Industry.Text.ToLower();
            bool? isActivel = IsActive.IsChecked;
            var searchResults = await _business.Search(name, address, website, industry, isActivel);
            if (searchResults.Status > 0 && searchResults.Data != null)
            {
                grdCompany.ItemsSource = searchResults.Data as List<Company>;
            }
            else
            {
                MessageBox.Show(searchResults.Message, "Search");
                grdCompany.ItemsSource = new List<Company>();
            }
        }
    }
}
