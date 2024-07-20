using DiamondShopSystem.WPFApp.UI.Categories;
using DiamondShopSystem.WPFApp.UI.Companys;
using DiamondShopSystem.WPFApp.UI.Customers;
using DiamondShopSystem.WPFApp.UI.OrderDetails;
using DiamondShopSystem.WPFApp.UI.Orders;
using DiamondShopSystem.WPFApp.UI.Products;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiamondShopSystem.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_wProduct_Click(object sender, RoutedEventArgs e)
        {
            var p = new wProduct();
            p.Owner = this;
            p.Show();
        }

        private void Open_wSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            var p = new wSearchProduct();
            p.Owner = this;
            p.Show();
        }

        private void Open_wCompany_Click(object sender, RoutedEventArgs e)
        {
            var cp = new wCompany();
            cp.Owner = this;
            cp.Show();
        }

        private void Open_wSearchCompany_Click(object sender, RoutedEventArgs e)
        {
            var cp = new wSearchCompany();
            cp.Owner = this;
            cp.Show();
        }

        private void Open_wCustomer_Click(object sender, RoutedEventArgs e)
        {
            var c = new WCustomer();
            c.Owner = this;
            c.Show();
        }

        private void Open_wSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            var c = new wSearchCustomer();
            c.Owner = this;
            c.Show();
        }

        private void Open_wCategory_Click(object sender, RoutedEventArgs e)
        {
            var c = new wCategory();
            c.Owner = this;
            c.Show();
        }

        private void Open_wSearchCategory_Click(object sender, RoutedEventArgs e)
        {
            var c = new wSearchCategory();
            c.Owner = this;
            c.Show();
        }


        private void Open_wOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            var o = new WOrderDetail();
            o.Owner = this;
            o.Show();
        }

        private void Open_wSearchOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            var o = new WSearchOrderDetail();
            o.Owner = this;
            o.Show();
        }

        private void Open_wOrder_Click(object sender, RoutedEventArgs e)
        {
            var p = new WOrder();
            p.Owner = this;
            p.Show();
        }
    }
}