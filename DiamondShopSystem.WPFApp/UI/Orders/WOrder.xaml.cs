using DiamondShopSystem.Business;
using DiamondShopSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for WOrder.xaml
    /// </summary>
    public partial class WOrder : Window
    {
        private readonly OrderBusiness _business;
        public WOrder()
        {
            InitializeComponent();
            _business ??= new OrderBusiness();
            this.LoadGrdOrder();
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            this.LoadGrdOrder();
        }

        private async void grdOrder_ButtonView_Click(object sender, RoutedEventArgs e)
        {

            if (grdOrder.SelectedItem is Order selectedOrder)
            {
                var OrderDetail = new WViewOrder(selectedOrder.OrderId);
                OrderDetail.ShowDialog();

            }
            else
            {
                MessageBox.Show("No order selected to view details.", "Error");
            }
        }


        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            DateTime day = DateTime.Parse(orderDay.Text);
            string orderStatus = OrderStatus.Text;
            string paymentMethod = PaymentMethod.Text;
            string paymentStatus = PaymentStatus.Text.ToLower();
            string shippingAddress = ShippingAddress.Text.ToLower();
            string discount = Discount.Text.ToLower();
            string note = Note.Text.ToLower();

            var searchResults = await _business.SearchOrder(day, orderStatus, paymentMethod, paymentStatus, shippingAddress, discount, note);

            if (searchResults.Status > 0 && searchResults.Data != null)
            {
                grdOrder.ItemsSource = searchResults.Data as List<Order>;
            }
            else
            {

                grdOrder.ItemsSource = new List<Order>();
            }


        }
        private async void grdOrder_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string OrderId = btn.CommandParameter.ToString();

            //MessageBox.Show(OrderId);

            if (!string.IsNullOrEmpty(OrderId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(OrderId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdOrder();
                }
            }
        }





        private async void LoadGrdOrder()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdOrder.ItemsSource = result.Data as List<Order>;
            }
            else
            {
                grdOrder.ItemsSource = new List<Order>();
            }
        }
        private async void LoadGrdOrder(int orderId)
        {
            var orderResult = await _business.GetById(orderId);
            if (orderResult.Status > 0 && orderResult.Data != null)
            {
                this.LoadGrdOrder();
                var order = orderResult.Data as Order;

                OrderId.Text = order.OrderId.ToString();
                CustomerId.Text = order.CustomerId.ToString();
                orderDay.SelectedDate = order.OrderDay;
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
                MessageBox.Show("null");
            }
        }

        private async void grdOrder_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Order;
                    if (item != null)
                    {
                        var orderResult = await _business.GetById(item.OrderId);

                        if (orderResult.Status > 0 && orderResult.Data != null)
                        {
                            item = orderResult.Data as Order;
                            OrderId.Text = item.OrderId.ToString();
                            CustomerId.Text = item.CustomerId.ToString();
                            orderDay.SelectedDate = item.OrderDay;
                            OrderStatus.Text = item.OrderStatus;
                            PaymentMethod.Text = item.PaymentMethod;
                            PaymentStatus.Text = item.PaymentStatus;
                            ShippingAddress.Text = item.ShippingAddress;
                            Discount.Text = item.Discount;
                            TotalPrice.Text = item.TotalPrice.ToString();
                            Note.Text = item.Note;

                        }
                    }
                }
            }
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idd = -1;
                int.TryParse(OrderId.Text, out idd);
                var item = await _business.GetById(idd);

                if (item.Data == null)
                {
                    var order = new Order()
                    {
                        CustomerId = int.Parse(CustomerId.Text),
                        OrderDay = orderDay.SelectedDate,
                        OrderStatus = OrderStatus.Text,
                        PaymentMethod = PaymentMethod.Text,
                        PaymentStatus = PaymentStatus.Text,
                        ShippingAddress = ShippingAddress.Text,
                        Discount = Discount.Text,
                        TotalPrice = int.Parse(TotalPrice.Text),
                        Note = Note.Text,

                    };

                    var result = await _business.Save(order);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var order = item.Data as Order;
                    //customer.CustomerId = int.Parse(CustomerId.Text);
                    order.CustomerId = int.Parse(CustomerId.Text);
                    order.OrderDay = orderDay.SelectedDate;
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

                ClearForm();
                this.LoadGrdOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }



        private void ClearForm()
        {
            OrderId.Text = string.Empty;
            CustomerId.Text = string.Empty;
            orderDay.SelectedDate = null;
            OrderStatus.Text = string.Empty;
            PaymentMethod.Text = string.Empty;
            PaymentStatus.Text = string.Empty;
            ShippingAddress.Text = string.Empty;
            Discount.Text = null;
            TotalPrice.Text = string.Empty;
            Note.Text = string.Empty;

        }
    }
}
