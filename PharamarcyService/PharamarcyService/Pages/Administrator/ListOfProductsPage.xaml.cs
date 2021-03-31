using PharamarcyService.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PharamarcyService.Pages.Administrator
{
    /// <summary>
    /// Interaction logic for ListOfProductsPage.xaml
    /// </summary>
    public partial class ListOfProductsPage : Page
    {
        public ListOfProductsPage()
        {
            InitializeComponent();
            UpdateDataGrd();
            CmbBoxManufacturer.ItemsSource = AppData.Context.Manufacturer.ToList();
        }

        private void UpdateDataGrd()
        {
            DataGrdProducts.ItemsSource = AppData.Context.Product.ToList().Where(p => p.IsArchived == false);

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var listOfProducts = AppData.Context.Product.ToList().Where(p => p.Name.ToLower().Trim().Contains(TxtBoxSearch.Text.ToLower().Trim())
                && p.IsArchived == false).ToList();
            if (CmbBoxManufacturer.SelectedIndex > 0)
                listOfProducts = listOfProducts.Where(p => p.ManufacturerId == (CmbBoxManufacturer.SelectedItem as Manufacturer).Id).ToList();
            DataGrdProducts.ItemsSource = listOfProducts;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Administrator.AddEditProductPage((sender as Button).DataContext as Product));
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите архивировать данный продукт?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var currProduct = (sender as Button).DataContext as Product;
                currProduct.IsArchived = true;
                AppData.Context.SaveChanges();
                UpdateDataGrd();
            }
        }

        private void BtnAddEditProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Administrator.AddEditProductPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrd();
        }
    }
}
