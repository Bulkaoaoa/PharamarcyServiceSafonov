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
    /// Interaction logic for MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        public MainAdminPage()
        {
            InitializeComponent();
        }

        private void BtnListOfProducts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Administrator.ListOfProductsPage());
        }

        private void BtnListOfSupplier_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Administrator.ListOfManufacturerPage());
        }

        private void BtnListOfOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
