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
    /// Interaction logic for wCompanyDetail.xaml
    /// </summary>
    public partial class wCompanyDetail : Window
    {
        private readonly CompanyBusiness _business;
        private readonly int _id;
        public wCompanyDetail(int companyId)
        {
            InitializeComponent();
            _business = new CompanyBusiness();
            _id = companyId;
            LoadCompanyDetail();
        }

        private async void LoadCompanyDetail()
        {
            var result = await _business.GetById(_id);
            if (result.Status > 0 && result.Data != null)
            {
                var company = result.Data as Company;
                if (company != null)
                {
                    CompanyIdBlockText.Text = company.CompanyId.ToString();
                    CompanyNameBlockText.Text = company.CompanyName;
                    AddressBlockText.Text = company.Address;
                    PhoneBlockText.Text = company.Phone;
                    EmailBlockText.Text = company.Email;
                    WebsiteBlockText.Text = company.Website;
                    IsActiveBlockText.Text = company.IsActive.ToString();
                    IndustryBlockText.Text = company.Industry;
                    FoundedDateBlockText.Text = company.FoundedDate.ToString();
                    DescriptionBlockText.Text = company.Description;
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
