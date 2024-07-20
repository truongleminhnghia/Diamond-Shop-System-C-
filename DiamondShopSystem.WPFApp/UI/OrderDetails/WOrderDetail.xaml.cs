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
    /// Interaction logic for WOrderDetail.xaml
    /// </summary>
    public partial class WOrderDetail : Window
    {
        private readonly OrderDetailBusiness _business;
        public WOrderDetail()
        {
            InitializeComponent();
            _business = new OrderDetailBusiness();
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
        private void ClearForm()
        {
            OrderDetailId.Text = string.Empty;
            OrderId.Text = string.Empty;
            ProductId.Text = string.Empty;
            Quantity.Text = string.Empty;
            Discount.Text = string.Empty;
            UnitPrice.Text = string.Empty;
            Ceritficare.Text = string.Empty;
            WarrantlyCardId.Text = string.Empty;
            DiscountAmount.Text = string.Empty;
            TotalAmount.Text = string.Empty;
            Amount.Text = string.Empty;
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                int id = -1;
                int.TryParse(OrderDetailId.Text, out id);
                var item = await _business.GetById(id);
                if (item.Data == null)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = int.Parse(OrderId.Text),
                        ProductId = int.Parse(ProductId.Text),
                        Quantity = int.Parse(Quantity.Text),
                        Discount = Discount.Text,
                        UnitPrice = UnitPrice.Text,
                        Ceritficare = Ceritficare.Text,
                        WarrantlyCardId = WarrantlyCardId.Text,
                        DiscountAmount = double.Parse(DiscountAmount.Text),
                        TotalAmount = double.Parse(TotalAmount.Text),
                        Amount = double.Parse(Amount.Text)
                    };
                    var result = await _business.Save(orderDetail);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var orderDetail = item.Data as OrderDetail;
                    orderDetail.OrderId = int.Parse(OrderId.Text);
                    orderDetail.ProductId = int.Parse(ProductId.Text);
                    orderDetail.Quantity = int.Parse(Quantity.Text);
                    orderDetail.Discount = Discount.Text;
                    orderDetail.UnitPrice = UnitPrice.Text;
                    orderDetail.Ceritficare = Ceritficare.Text;
                    orderDetail.WarrantlyCardId = WarrantlyCardId.Text;
                    orderDetail.DiscountAmount = double.Parse(DiscountAmount.Text);
                    orderDetail.TotalAmount = double.Parse(TotalAmount.Text);
                    orderDetail.Amount = double.Parse(Amount.Text);

                    var result = await _business.Update(orderDetail);
                    MessageBox.Show(result.Message, "Update");
                }
                ClearForm();
                LoadGrdOrderDetail();
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

        private async void grdOrderDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string orderDetailId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(orderDetailId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(orderDetailId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    LoadGrdOrderDetail();
                }
            }
        }

        private async void grdOrderDetail_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as OrderDetail;
                    if (item != null)
                    {
                        var orderDetailResult = await _business.GetById(item.OrderDetailId);

                        if (orderDetailResult.Status > 0 && orderDetailResult.Data != null)
                        {
                            item = orderDetailResult.Data as OrderDetail;
                            OrderDetailId.Text = item.OrderDetailId.ToString();
                            OrderId.Text = item.OrderId.ToString();
                            ProductId.Text = item.ProductId.ToString();
                            Quantity.Text = item.Quantity.ToString();
                            Discount.Text = item.Discount;
                            UnitPrice.Text = item.UnitPrice;
                            Ceritficare.Text = item.Ceritficare;
                            WarrantlyCardId.Text = item.WarrantlyCardId;
                            DiscountAmount.Text = item.DiscountAmount.ToString();
                            TotalAmount.Text = item.TotalAmount.ToString();
                            Amount.Text = item.Amount.ToString();
                        }
                    }
                }
            }
        }
    }
}
