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
    /// Interaction logic for EditClientOrderPage.xaml
    /// </summary>
    public partial class EditClientOrderPage : Page
    {
        private Order _currOrder;
        private List<UserOrderProduct> _listOfProductOfOrder = new List<UserOrderProduct>();
        private List<Product> _listOfProducts = new List<Product>();
        public EditClientOrderPage()
        {
            InitializeComponent();
            //Проверить на всякий может будет ломать все
            _listOfProducts = AppData.Context.Product.ToList();
            _listOfProducts.ForEach(p => p.CountInProgram = 0);
            var listOfManufacturers = AppData.Context.Manufacturer.ToList();
            listOfManufacturers.Insert(0, new Manufacturer()
            {
                Name = "Все"
            });
            CmbBoxManufacturer.ItemsSource = listOfManufacturers;
            _listOfProducts = AppData.Context.Product.ToList();

        }
        public EditClientOrderPage(Order currOrder)
        {
            InitializeComponent();

            var listOfManufacturers = AppData.Context.Manufacturer.ToList();
            listOfManufacturers.Insert(0, new Manufacturer()
            {
                Name = "Все"
            });
            CmbBoxManufacturer.ItemsSource = listOfManufacturers;

            _currOrder = currOrder;
            _listOfProducts = AppData.Context.Product.ToList();
            _listOfProducts.ForEach(p => p.CountInProgram = 0);
            var listofUserOrderOfProduct = AppData.Context.UserOrderProduct.ToList().Where(p => p.OrderId == currOrder.Id).ToList();
            foreach (var item in listofUserOrderOfProduct.ToList())
            {
                if (_listOfProducts.Contains(item.Product))
                {
                    var curritem = _listOfProducts.Where(p => p.Id == item.Product.Id).FirstOrDefault();
                    curritem.CountInProgram = item.Count;
                }
            }
            DataGrdProduct.ItemsSource = _listOfProducts;
        }

        private void BtnSaveOrder_Click(object sender, RoutedEventArgs e)
        {


            if (_listOfProductOfOrder.Count != 0)
            {
                if (_currOrder == null)
                {
                    foreach (var item in _listOfProductOfOrder.ToList())
                    {
                        AppData.Context.UserOrderProduct.Add(item);
                    }
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Вы успешно добавили заказ!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var listToRemove = AppData.Context.UserOrderProduct.ToList().Where(p => p.OrderId == _currOrder.Id).ToList();
                    foreach (var item in listToRemove.ToList())
                    {
                        AppData.Context.UserOrderProduct.Remove(item);
                    }
                    AppData.Context.SaveChanges();
                    foreach (var item in _listOfProductOfOrder.ToList())
                    {
                        AppData.Context.UserOrderProduct.Add(item);
                    }
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Вы успешно обновили заказ!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
                MessageBox.Show("Вы не выбрали ни одного товара!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void TxtBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsWrongValue = false;
            var currTxtBox = sender as TextBox;
            foreach (var item in currTxtBox.Text)
            {
                if (!char.IsDigit(item))
                {
                    IsWrongValue = true;
                    break;
                }
            }

            if (!IsWrongValue)
            {
                int currValue = int.Parse(currTxtBox.Text);
                var currProduct = currTxtBox.DataContext as Product;

                if (_listOfProductOfOrder.Where(p => p.ProductId == currProduct.Id).FirstOrDefault() == null)
                {
                    //Тут мы только добавляем
                    if (currValue > 0)
                    {
                        var newItemOfOrder = new UserOrderProduct()
                        {
                            OrderId = _currOrder.Id,
                            ProductId = currProduct.Id,
                            Count = currValue
                        };
                        _listOfProductOfOrder.Add(newItemOfOrder);
                    }
                }
                else
                {
                    var currItemOfOrder = _listOfProductOfOrder.Where(p => p.ProductId == currProduct.Id).FirstOrDefault();
                    if (currValue > 0)
                        currItemOfOrder.Count = currValue;
                    else
                        _listOfProductOfOrder.Remove(currItemOfOrder);
                }
            }
            else
            {
                MessageBox.Show("Вы ввели неправильное значение для количества!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var buffList = _listOfProducts;
            if (!string.IsNullOrWhiteSpace(TxtBoxSearch.Text))
                buffList = buffList.Where(p => p.Name.ToLower().Trim().Contains(TxtBoxSearch.Text.ToLower().Trim())).ToList();

            if (CmbBoxManufacturer.SelectedIndex > 0)
                buffList = buffList.Where(p => p.ManufacturerId == (CmbBoxManufacturer.SelectedItem as Manufacturer).Id).ToList();
            DataGrdProduct.ItemsSource = buffList;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _listOfProducts = AppData.Context.Product.ToList();
            _listOfProducts.ForEach(p => p.CountInProgram = 0);
        }
    }
}
