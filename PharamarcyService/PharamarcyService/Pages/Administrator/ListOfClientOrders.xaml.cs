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
    /// Interaction logic for ListOfClientOrders.xaml
    /// </summary>
    public partial class ListOfClientOrders : Page
    {
        public ListOfClientOrders()
        {
            InitializeComponent();
            var currList = AppData.Context.OrderStatus.ToList();
            currList.Insert(0, new OrderStatus()
            {
                Name = "Все"
            });
            CmbBoxStatus.ItemsSource = currList;
            UpdateDataGrd();
        }
        private void UpdateDataGrd()
        {
            DataGrdClientorders.ItemsSource = AppData.Context.Order.ToList().Where(p => p.IsForPharamarcy == false).ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrd();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Administrator.EditClientOrderPage((sender as Button).DataContext as Order));
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var currItem = (sender as Button).DataContext as Order;
            if (currItem.StatusId != 3)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите отменить заказ №{currItem.Id}?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    currItem.StatusId = 3;
                    AppData.Context.SaveChanges();
                }
            }
            else
                MessageBox.Show("Вы не можете отменить этот заказ", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var currList = AppData.Context.Order.ToList().Where(p => p.IsForPharamarcy == false && p.Id.ToString().Contains(TxtBoxSearch.Text.ToLower().Trim())).ToList();
            if (CmbBoxStatus.SelectedIndex > 0)
                currList = currList.Where(p => p.StatusId == (CmbBoxStatus.SelectedItem as OrderStatus).Id).ToList();
            DataGrdClientorders.ItemsSource = currList;

        }

        private void BtnAddUserOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Administrator.EditClientOrderPage());
        }
    }
}
