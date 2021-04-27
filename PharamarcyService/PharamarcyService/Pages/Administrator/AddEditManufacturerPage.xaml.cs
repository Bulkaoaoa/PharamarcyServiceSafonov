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
    /// Interaction logic for AddEditManufacturerPage.xaml
    /// </summary>
    public partial class AddEditManufacturerPage : Page
    {
        Manufacturer _currManufacturer  = null;
        public AddEditManufacturerPage()
        {
            InitializeComponent();
            CmBoxCountry.ItemsSource = AppData.Context.Country.ToList();
        }
        public AddEditManufacturerPage(Manufacturer currManufacturer)
        {
            InitializeComponent();
            _currManufacturer = currManufacturer;
            this.DataContext = currManufacturer;
            CmBoxCountry.ItemsSource = AppData.Context.Country.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(TxtBoxName.Text)) errors += "Вы не ввели название \r\n";
            if (CmBoxCountry.SelectedIndex == -1) errors += "Вы не выбрали страну производителя\r\n";

            if (errors.Length == 0)
            {
                if (_currManufacturer == null)
                {
                    var newManufacturer = new Manufacturer()
                    {
                        Name = TxtBoxName.Text.Trim(),
                        CountryId = (CmBoxCountry.SelectedItem as Country).Id,
                    };
                    AppData.Context.Manufacturer.Add(newManufacturer);
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Вы успешно добавили нового производителя", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    _currManufacturer.CountryId = (CmBoxCountry.SelectedItem as Country).Id;
                    _currManufacturer.Name = TxtBoxName.Text.Trim();
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Вы успешно изменили производителя", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
            else
                MessageBox.Show(errors, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
