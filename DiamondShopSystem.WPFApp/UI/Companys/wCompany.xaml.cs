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
    /// Interaction logic for wCompany.xaml
    /// </summary>
    public partial class wCompany : Window
    {
        private readonly CompanyBusiness _business;
        public wCompany()
        {
            InitializeComponent();
            _business = new CompanyBusiness();
            LoadGrdCompany();
        }
        private async void LoadGrdCompany()
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

        private void ClearForm()
        {
            CompanyId.Text = string.Empty;
            CompanyName.Text = string.Empty;
            Address.Text = string.Empty;
            Phone.Text = string.Empty;
            Email.Text = string.Empty;
            Description.Text = string.Empty;
            FoundedDate.Text = string.Empty;
            Website.Text = string.Empty;
            IsActive.Text = string.Empty;
            Industry.Text = string.Empty;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = -1;
                int.TryParse(CompanyId.Text, out id);
                var item = await _business.GetById(id);

                DateTime foundedDate;
                bool isActive;

                // Validate and parse FoundedDate
                if (!DateTime.TryParse(FoundedDate.Text, out foundedDate))
                {
                    MessageBox.Show("Invalid Founded Date", "Error");
                    return;
                }

                // Validate and parse IsActive
                if (!bool.TryParse(IsActive.Text, out isActive))
                {
                    MessageBox.Show("Invalid IsActive value", "Error");
                    return;
                }

                if (item.Data == null)
                {
                    var company = new Company()
                    {
                        CompanyName = CompanyName.Text,
                        Address = Address.Text,
                        Phone = Phone.Text,
                        Email = Email.Text,
                        Website = Website.Text,
                        Industry = Industry.Text,
                        Description = Description.Text,
                        IsActive = isActive,
                        FoundedDate = foundedDate
                    };

                    var result = await _business.Save(company);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var company = item.Data as Company;

                    company.CompanyName = CompanyName.Text;
                    company.Address = Address.Text;
                    company.Phone = Phone.Text;
                    company.Email = Email.Text;
                    company.Website = Website.Text;
                    company.Industry = Industry.Text;
                    company.Description = Description.Text;
                    company.IsActive = isActive;
                    company.FoundedDate = foundedDate;

                    var result = await _business.Update(company);
                    MessageBox.Show(result.Message, "Update");
                }

                ClearForm();
                LoadGrdCompany();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void grdCompany_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string companyId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(companyId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse(companyId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    LoadGrdCompany();
                }
            }
        }

        private async void grdCompany_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Company;
                    if (item != null)
                    {
                        var companyResult = await _business.GetById(item.CompanyId);
                        if (companyResult.Status > 0 && companyResult.Data != null)
                        {
                            item = companyResult.Data as Company;
                            CompanyId.Text = item.CompanyId.ToString();
                            CompanyName.Text = item.CompanyName;
                            Address.Text = item.Address;
                            Phone.Text = item.Phone;
                            Email.Text = item.Email;
                            Website.Text = item.Website;
                            Industry.Text = item.Industry;
                            Description.Text = item.Description;
                            FoundedDate.Text = item.FoundedDate.ToString();
                            IsActive.Text = item.FoundedDate.ToString();
                        }
                    }
                }
            }
        }
    }
}
