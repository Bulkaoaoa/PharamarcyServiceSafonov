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

namespace PharamarcyService.Pages.Client
{
    /// <summary>
    /// Interaction logic for ListOfProductsPage.xaml
    /// </summary>
    public partial class ListOfProductsPage : Page
    {
        private Order _order;
        public ListOfProductsPage()
        {
            InitializeComponent();
        }

        private void CreateOrder()
        {
            _order = new Order
            {
                StatusId = 1,
                IsForPharamarcy = false,
                UserOrderProduct = new List<UserOrderProduct>(),
                User = AppData.CurrUser,
                UserId = AppData.CurrUser.Id,
            };
        }

        private void UpdateDataGrd()
        {
            DataGrdProducts.ItemsSource = AppData.Context.Product.ToList().Where(p => p.IsArchived == false).ToList();
        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            BdrCart.Visibility = Visibility.Visible;
        }

        private void BtnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as Product;
            var userOrderProduct = _order.UserOrderProduct.ToList().FirstOrDefault(i => i.Product == product);
            if (userOrderProduct == null)
                _order.UserOrderProduct.Add(new UserOrderProduct
                {
                    Product = product,
                    ProductId = product.Id,
                    Count = 1,
                });
            else
                userOrderProduct.Count++;
            UpdateDataContext();
        }

        private void UpdateDataContext()
        {
            DataContext = null;
            DataContext = _order;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var listOfProducts = AppData.Context.Product.ToList().Where(p => p.Name.ToLower().Trim().Contains(TxtBoxSearch.Text.ToLower().Trim())
               && p.IsArchived == false).ToList();
            if (CmbBoxManufacturer.SelectedIndex > 0)
                listOfProducts = listOfProducts.Where(p => p.ManufacturerId == (CmbBoxManufacturer.SelectedItem as Manufacturer).Id).ToList();
            DataGrdProducts.ItemsSource = listOfProducts;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var currList = AppData.Context.Manufacturer.ToList();
            currList.Insert(0, new Manufacturer()
            {
                Name = "Все",
            });
            CmbBoxManufacturer.ItemsSource = currList;
            CreateOrder();
            UpdateDataGrd();
            UpdateDataContext();
            BdrCart.Visibility = Visibility.Hidden;
        }

        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BdrCart.Visibility = Visibility.Hidden;
        }

        private void BtnUpper_Click(object sender, RoutedEventArgs e)
        {
            var userOrderProduct = (sender as Button).DataContext as UserOrderProduct;
            userOrderProduct.Count++;
            UpdateDataContext();
        }

        private void BtnLower_Click(object sender, RoutedEventArgs e)
        {
            var userOrderProduct = (sender as Button).DataContext as UserOrderProduct;
            if (userOrderProduct.Count > 1)
                userOrderProduct.Count--;
            UpdateDataContext();
        }

        private void BtnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_order.TotalCount == 0)
            {
                MessageBox.Show("Выберите товары", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AppData.Context.Order.Add(_order);
                AppData.Context.SaveChanges();
                NavigationService.Navigate(new Pages.Client.OrderInfoPage(_order));
            }
        }
    }
}
