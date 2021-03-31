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

namespace PharamarcyService.Pages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            var currUser = AppData.Context.User.ToList().Where(p => p.Login == TxtBoxLogin.Text && p.Password == PassBoxPassword.Password).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(TxtBoxLogin.Text)) errors += "Вы не ввели логин \r\n";
            if (string.IsNullOrWhiteSpace(PassBoxPassword.Password)) errors += "Вы не ввели пароль \r\n";
            if (currUser == null) errors += "Такого пользователя не существует";

            if (errors.Length == 0)
            {
                AppData.CurrUser = currUser;
                switch (currUser.RoleId)
                {
                    case 1:
                        NavigationService.Navigate(new Pages.Client.MainClientPage());
                        break;
                    case 2:
                        NavigationService.Navigate(new Pages.Supplier.MainSupplierPage());
                        break;
                    case 3:
                        NavigationService.Navigate(new Pages.Administrator.MainAdminPage());
                        break;
                }
            }
            else
                MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.RegistrationPage());
        }
    }
}
