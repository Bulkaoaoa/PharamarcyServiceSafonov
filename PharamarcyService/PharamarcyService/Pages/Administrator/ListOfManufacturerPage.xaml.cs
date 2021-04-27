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
    /// Interaction logic for ListOfManufacturerPage.xaml
    /// </summary>
    public partial class ListOfManufacturerPage : Page
    {
        public ListOfManufacturerPage()
        {
            InitializeComponent();
            var currlist = AppData.Context.Country.ToList();
            currlist.Insert(0, new Country()
            {
                Name = "Все"
            });
            CmBoxCountry.ItemsSource = currlist;
        }

        private void UpdateManufacturer()
        {
            DataGrdManufacturer.ItemsSource = AppData.Context.Manufacturer.ToList();
        }
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var currList = AppData.Context.Manufacturer.ToList().Where(p => p.Name.ToLower().Trim().Contains(TxtBoxSearch.Text.ToLower().Trim())).ToList();
            if (CmBoxCountry.SelectedIndex > 0)
                currList = currList.Where(p => p.CountryId == (CmBoxCountry.SelectedItem as Country).Id).ToList();
            DataGrdManufacturer.ItemsSource = currList;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currManufacturer = (sender as Button).DataContext as Manufacturer;
            NavigationService.Navigate(new Pages.Administrator.AddEditManufacturerPage(currManufacturer));
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var currManifacturer = (sender as Button).DataContext as Manufacturer;
            var result = MessageBox.Show($"Вы уверены, что хотите удалить {currManifacturer.Name} и все его товары?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var item in AppData.Context.Product.ToList().Where(p => p.ManufacturerId == currManifacturer.Id).ToList())
                {
                    AppData.Context.Product.Remove(item);
                    AppData.Context.SaveChanges();
                }
                AppData.Context.Manufacturer.Remove(currManifacturer);
                AppData.Context.SaveChanges();
            }
        }
        private void BtnAddManufacturer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Administrator.AddEditManufacturerPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateManufacturer();
        }

    }
}
