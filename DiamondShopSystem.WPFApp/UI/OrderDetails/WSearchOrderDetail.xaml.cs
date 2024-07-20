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

namespace DiamondShopSystem.WPFApp.UI.OrderDetails
{
    /// <summary>
    /// Interaction logic for WSearchOrderDetail.xaml
    /// </summary>
    public partial class WSearchOrderDetail : Window
    {
        private readonly OrderDetailBusiness _business;
        public WSearchOrderDetail()
        {
            InitializeComponent();
            _business = new OrderDetailBusiness();
            DataContext = this;
            LoadGrdOrderDetail();
        }

        private async void LoadGrdOrderDetail()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdOrderDetail.ItemsSource = result.Data as List<OrderDetail>;
            }
            else
            {
                grdOrderDetail.ItemsSource = new List<OrderDetail>();
            }
        }
        private void ButtonCloess_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void grdProduct_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            if (grdOrderDetail.SelectedItem is OrderDetail selectedOrderDetail)
            {
                var detailWindow = new WDetailOrderDetail(selectedOrderDetail.OrderDetailId);
                detailWindow.ShowDialog();
            }
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string productid = ProductId.Text;
            string orderId = OrderId.Text;
            string quantity = Quantity.Text;

            var searchResults = await _business.SearchOrderDetail(productid, orderId, quantity);
                if (searchResults.Status > 0 && searchResults.Data != null)
                {
                    grdOrderDetail.ItemsSource = searchResults.Data as List<OrderDetail>;
                }
                else
                {
                    MessageBox.Show("No data found.", "Search");
                }
            
        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var a = new WOrderDetail();
            a.Owner = this;
            a.Show();
        }
    }
}
