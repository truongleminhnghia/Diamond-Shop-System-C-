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
    /// Interaction logic for WDetailOrderDetail.xaml
    /// </summary>
    public partial class WDetailOrderDetail : Window
    {
        private readonly OrderDetailBusiness _business;
        private readonly int _id;
        public WDetailOrderDetail(int orderDetailId)
        {
            InitializeComponent();
            _business = new OrderDetailBusiness();
            _id = orderDetailId;
            LoadOrderDetail();
        }

        private async void LoadOrderDetail()
        {
            var result = await _business.GetById(_id);
            if (result.Status > 0 && result.Data != null)
            {
                var orderDetail = result.Data as OrderDetail;
                if (orderDetail != null)
                {
                    OrderDetailIdBlockText.Text = orderDetail.OrderDetailId.ToString();
                    OrderIdBlockText.Text = orderDetail.OrderId?.ToString();
                    ProductIdBlockText.Text = orderDetail.ProductId?.ToString();
                    QuantityBlockText.Text = orderDetail.Quantity?.ToString();
                    DiscountBlockText.Text = orderDetail.Discount;
                    UnitPriceBlockText.Text = orderDetail.UnitPrice;
                    CertificationBlockText.Text = orderDetail.Ceritficare;
                    WarrantyCardIdBlockText.Text = orderDetail.WarrantlyCardId;
                    DiscountAmountBlockText.Text = orderDetail.DiscountAmount?.ToString();
                    TotalAmountBlockText.Text = orderDetail.TotalAmount?.ToString();
                    AmountBlockText.Text = orderDetail.Amount?.ToString();
                }
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
