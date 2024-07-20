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

namespace DiamondShopSystem.WPFApp.UI.Orders
{
    /// <summary>
    /// Interaction logic for WViewOrder.xaml
    /// </summary>
    public partial class WViewOrder : Window
    {
        private readonly OrderBusiness _business;
        private readonly int _id;
        public WViewOrder(int id)
        {
            InitializeComponent();
            _business ??= new OrderBusiness();
            _id = id;
            LoadOrderDetails();
        }

        private async void LoadOrderDetails()
        {
            var result = await _business.GetById(_id);

            if (result.Status > 0 && result.Data != null)
            {
                var order = result.Data as Order;
                OrderId.Text = order.OrderId.ToString();
                CustomerId.Text = order.CustomerId.ToString();
                OrderDay.Text = order.OrderDay.ToString();
                OrderStatus.Text = order.OrderStatus;
                PaymentMethod.Text = order.PaymentMethod;
                PaymentStatus.Text = order.PaymentStatus;
                ShippingAddress.Text = order.ShippingAddress;
                Discount.Text = order.Discount;
                TotalPrice.Text = order.TotalPrice.ToString();
                Note.Text = order.Note;

            }
            else
            {
                MessageBox.Show(result.Message, "Error");
            }
        }
        private async void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var item = await _business.GetById(_id);
            if (item.Status > 0 && item.Data != null)
            {
                var order = item.Data as Order;

                order.OrderId = int.Parse(OrderId.Text);
                order.CustomerId = int.Parse(CustomerId.Text);
                order.OrderDay = DateTime.Parse(OrderDay.Text);
                order.OrderStatus = OrderStatus.Text;
                order.PaymentMethod = PaymentMethod.Text;
                order.PaymentStatus = PaymentStatus.Text;
                order.ShippingAddress = ShippingAddress.Text;
                order.Discount = Discount.Text;
                order.TotalPrice = int.Parse(TotalPrice.Text);
                order.Note = Note.Text;

                var result = await _business.Update(order);
                MessageBox.Show(result.Message, "Update");
            }
        }

    }
}
