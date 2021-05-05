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
    /// Interaction logic for ListOfSuppliersProductPage.xaml
    /// </summary>
    public partial class ListOfSuppliersProductPage : Page
    {
        private List<Product> _orderProducts = new List<Product>();
        public ListOfSuppliersProductPage()
        {
            InitializeComponent();
            CmbBoxSupplier.ItemsSource = AppData.Context.Supplier.ToList();
            DataGrdSuppliersProducts.ItemsSource = AppData.Context.Product.ToList().Where(p => p.SupplierId == (CmbBoxSupplier.SelectedItem as Entities.Supplier).Id).ToList();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Очищать лист с продкутами
            _orderProducts = new List<Product>();
            DataGrdSuppliersProducts.ItemsSource = AppData.Context.Product.ToList().Where(p => p.SupplierId == (CmbBoxSupplier.SelectedItem as Entities.Supplier).Id
            && p.Name.ToLower().Trim().Contains(TxtBoxSearch.Text.Trim().ToLower())).ToList();
        }

        private void BtnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currTextBox = sender as TextBox;
            int currValue = -1;
            var currProduct = currTextBox.DataContext as Product;
            try
            {
                currValue = int.Parse(currTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Вы ввели неправильное значение для количества товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (currValue != -1)
            {

                var currItem = _orderProducts.Where(p => p.Id == currProduct.Id).FirstOrDefault();
                if (currItem != null)
                {
                    //Удлаять если 0, редактировать, если число другое
                }
                else
                {
                    //Только добавление в лист
                    currProduct.CountInProgram = currValue;
                }
            }
            else

                MessageBox.Show("An error was occured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
